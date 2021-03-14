using System;

namespace Framework.CustomContainer
{
    public class CContainerRegisterModel
    {
        /// <summary>
        /// 注册函数的类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 生命周期
        /// </summary>
        public LifeTime LifeTime { get; set; }

        /// <summary>
        /// 单例模式下，存入此
        /// </summary>
        public object SingletonInstance { get; set; }
    }

    public enum LifeTime
    {
        /// <summary>
        /// 瞬时
        /// </summary>
        Transient,

        /// <summary>
        /// 单例
        /// </summary>
        Singleton,

        /// <summary>
        /// 作用域单例
        /// </summary>
        Scoped,
        /// <summary>
        /// 线程单例
        /// </summary>
        PerThread, //
        
        //外部可释放单例
    }
}