using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Framework.CustomAOP
{
    public static class ContainerAOPExtends
    {
        public static object AOP(this object t, Type interfaceType)
        {
            var generator = new ProxyGenerator();
            var interceptor = new IocInterceptor();
            t = generator.CreateInterfaceProxyWithTarget(interfaceType, t, interceptor);
            return t;
        }
    }

    #region 使用特性来实现接口AOP注入时的一些缺陷

    public abstract class BaseInterceptorAttribute : Attribute
    {
        public abstract Action Do(IInvocation invocation, Action action);
    }

    public class LogBeforeAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"Invoke {nameof(LogBeforeAttribute)} 1");
                // Console.WriteLine($"Invoke {nameof(invocation.Method)}" +
                //                   $" before DT:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}");
                action.Invoke();
                Console.WriteLine($"Invoke {nameof(LogBeforeAttribute)} 2");
            };
        }
    }

    public class LogAfterAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"Invoke {nameof(LogAfterAttribute)} 1");
                action.Invoke();
                // Console.WriteLine($"Invoke {nameof(invocation.Method)}" +
                // $" after  DT:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}");
                Console.WriteLine($"Invoke {nameof(LogAfterAttribute)} 2");
            };
        }
    }

    public class LoginAfterAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"Invoke {nameof(LoginAfterAttribute)} 1");
                // Console.WriteLine($"Invoke Login use {nameof(invocation.Method)}");
                action.Invoke();
                Console.WriteLine($"Invoke {nameof(LoginAfterAttribute)} 2");
            };
        }
    }

    /// <summary>
    /// 监视方法执行花费时长
    /// </summary>
    public class MonitorAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"Invoke {nameof(MonitorAttribute)} 1");
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                //执行的方法
                action.Invoke();
                stopwatch.Stop();
                Console.WriteLine($"本次执行{invocation.Method.Name},共花费{stopwatch.ElapsedMilliseconds}ms");
                Console.WriteLine($"Invoke {nameof(MonitorAttribute)} 2");
            };
        }
    }

    #endregion

    public class IocInterceptor : StandardInterceptor
    {
        /// <summary>
        /// 调用前的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            // Console.WriteLine($"调用前的拦截器，方法名是：{invocation.Method.Name}");
        }

        /// <summary>
        /// 拦截的方法返回时调用的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            var method = invocation.Method;
            //将待调用方法转成一个委托，
            Action action = () => { base.PerformProceed(invocation); };
            if (method.IsDefined(typeof(LogBeforeAttribute), true))
                foreach (var attribute in method.GetCustomAttributes<BaseInterceptorAttribute>().ToArray().Reverse())
                    //传入到特性里面,当有其他委托，继续将整个委托变为参数，传入，就呈现了一种调用链的关系
                    action = attribute.Do(invocation, action);

            //最后执行委托
            action.Invoke();
        }

        /// <summary>
        /// 调用后的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            // Console.WriteLine($"调用后的拦截器，方法名是：{invocation.Method.Name}");
        }
    }
}