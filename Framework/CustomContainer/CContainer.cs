using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Framework.CustomAOP;

namespace Framework.CustomContainer
{
    /// <summary>
    /// 用来生成对象
    /// 第三方  业务无关性
    /// </summary>
    public class CContainer : ICContainer
    {
        private Dictionary<string, CContainerRegisterModel> _containerDictionary =
            new Dictionary<string, CContainerRegisterModel>();

        private Dictionary<string, object[]> _containerParameterDictionary = new Dictionary<string, object[]>();

        private Dictionary<string, object> _containerScopeDictionary = new Dictionary<string, object>();

        private string GetKey(string fullName, string shortName) => $"{fullName}___{shortName}";

        public ICContainer CreateChildContainer()
        {
            return new CContainer(this._containerDictionary, this._containerParameterDictionary,
                new Dictionary<string, object>());
        }

        public CContainer()
        {
        }

        private CContainer(Dictionary<string, CContainerRegisterModel> containerDictionary,
            Dictionary<string, object[]> containerParameterDictionary,
            Dictionary<string, object> containerScopeDictionary)
        {
            this._containerDictionary = containerDictionary;
            this._containerParameterDictionary = containerParameterDictionary;
            this._containerScopeDictionary = containerScopeDictionary;
        }


        /// <summary>
        /// 注册
        /// TTo一定是TFrom的子类
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="shortName"></param>
        /// <param name="paramList"></param>
        /// <param name="lifeTime">生命周期</param>
        public void Register<TFrom, TTo>(string shortName = null, object[] paramList = null,
            LifeTime lifeTime = LifeTime.Transient) where TTo : TFrom
        {
            // this._containerDictionary.Add(typeof(TFrom).FullName, typeof(TTo));
            // this._containerDictionary.Add(this.GetKey(typeof(TFrom).FullName, shortName), typeof(TTo));
            this._containerDictionary.Add(this.GetKey(typeof(TFrom).FullName, shortName), new CContainerRegisterModel()
            {
                LifeTime = lifeTime,
                Type = typeof(TTo)
            });
            if (paramList != null && paramList.Length > 0)
                this._containerParameterDictionary.Add(this.GetKey(typeof(TFrom).FullName, shortName), paramList);
        }

        public TFrom Resolve<TFrom>(string shortName = null)
        {
            #region 注释，改写多层级构造函数注入

            // string key = typeof(TFrom).FullName;
            // Type type = this._containerDictionary[key];
            //
            // #region 可能构造函数需要传递额外参数
            //
            // List<object> paramList = new List<object>();
            //
            // //拿到当前类的所有构造函数
            // foreach (var ctor in type.GetConstructors())
            // {
            //     //拿到当前构造函数的参数列表
            //     foreach (var param in ctor.GetParameters())
            //     {
            //         Type paramType = param.ParameterType; //获取其中参数类型
            //         string paramKey = paramType.FullName; //获取完整名称
            //         Type paramTargetType = this._containerDictionary[paramKey]; //从字典中获取对应参数类型
            //         paramList.Add(Activator.CreateInstance(paramTargetType)); //创建并添加到参数类型集合，方便后续使用
            //     }
            // }
            //
            // #endregion
            //
            // //将一系列构造函数参数传入
            // object oInstance = Activator.CreateInstance(type, paramList.ToArray());
            //
            // return (TFrom)oInstance; 

            #endregion

            return (TFrom) this.ResolveObject(typeof(TFrom), shortName);
        }

        private object _ITestServiceB = null;

        private object ResolveObject(Type originType, string shortName)
        {
            string key = this.GetKey(originType.FullName, shortName);
            // Type type = this._containerDictionary[key];

            var registerModel = this._containerDictionary[key];
            Type type = registerModel.Type;

            switch (registerModel.LifeTime)
            {
                case LifeTime.Transient:
                    Console.WriteLine("Transient Do Nothing Before...");
                    break;
                case LifeTime.Singleton:
                    if (registerModel.SingletonInstance == null)
                        break;
                    else
                        return registerModel.SingletonInstance;
                case LifeTime.Scoped:
                    if (this._containerScopeDictionary.ContainsKey(key))
                    {
                        return this._containerScopeDictionary[key];
                    }

                    break;
                default:
                    break;
            }

            List<object> paramList = new List<object>();

            // //先默认用第一个构造函数
            // var ctor = type.GetConstructors()[0];

            ConstructorInfo ctor = null;
            //优先以特性标记为准
            ctor = type.GetConstructors()
                .FirstOrDefault(c => c.IsDefined(typeof(CConstructorAttribute), true));
            if (ctor == null)
            {
                //否则采用个数最多的构造函数
                ctor = type.GetConstructors()
                    .OrderByDescending(c => c.GetParameters().Length).First();
            }

            //先获取参数中是否有定义特性的
            object[] parList = this._containerParameterDictionary.ContainsKey(key)
                ? this._containerParameterDictionary[key]
                : null;
            int _index = 0;

            //拿到当前构造函数的参数列表
            foreach (var param in ctor.GetParameters())
            {
                if (param.IsDefined(typeof(CParameterInjectionAttribute), true))
                {
                    paramList.Add(parList[_index]);
                    _index++;
                }
                else
                {
                    Type paramType = param.ParameterType; //获取其中参数类型
                    string paramShowName = GetShortName(param); //获取层级传入参数
                    object paramInstance = this.ResolveObject(paramType, paramShowName);
                    // string paramKey = paramType.FullName; //获取完整名称
                    // Type paramTargetType = this._containerDictionary[paramKey]; //从字典中获取对应参数类型
                    // var paramInstance = Activator.CreateInstance(paramTargetType);
                    paramList.Add(paramInstance); //创建并添加到参数类型集合，方便后续使用
                }
            }


            object oInstance = Activator.CreateInstance(type, paramList.ToArray());

            //所有的构造函数注入完成，在开始执行属性注入
            foreach (var prop in type.GetProperties()
                .Where(p => p.IsDefined(typeof(CPropertyInjectionAttribute), true)))
            {
                Type propType = prop.PropertyType; //获取属性类型

                string propShortName = GetShortName(prop);
                var propInstance = this.ResolveObject(propType, propShortName); //获取属性实例
                prop.SetValue(oInstance, propInstance); //将属性实例赋值给构造函数实例
            }

            //方法注入
            foreach (var methodInfo in type.GetMethods()
                .Where(m => m.IsDefined(typeof(CMethodInjectionAttribute), true)))
            {
                List<object> methodParamList = new List<object>();
                var methodTypeInfos = methodInfo.GetParameters();
                foreach (var methodTypeInfo in methodTypeInfos)
                {
                    var methodParamType = methodTypeInfo.ParameterType;
                    string methodParamShortName = GetShortName(methodTypeInfo);
                    var methodParamInstance = this.ResolveObject(methodParamType, methodParamShortName);
                    methodParamList.Add(methodParamInstance);
                }

                methodInfo.Invoke(oInstance, methodParamList.ToArray());
            }

            switch (registerModel.LifeTime)
            {
                case LifeTime.Transient:
                    Console.WriteLine("Transient Do Nothing After...");
                    break;
                case LifeTime.Singleton:
                    registerModel.SingletonInstance = oInstance;
                    break;
                case LifeTime.Scoped:
                    this._containerScopeDictionary[key] = oInstance;
                    break;
                default:
                    break;
            }

            //实现AOP
            return oInstance.AOP(originType);
            // return oInstance;
        }

        private string GetShortName(ICustomAttributeProvider provider)
        {
            if (provider.IsDefined(typeof(CParameterShortNameAttribute), true))
            {
                var attribute =
                    (CParameterShortNameAttribute) provider.GetCustomAttributes(typeof(CParameterShortNameAttribute),
                        true)[0];
                return attribute._shortName;
            }
            else
            {
                return null;
            }
        }
    }
}