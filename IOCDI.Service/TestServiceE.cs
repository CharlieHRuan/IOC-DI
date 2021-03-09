using System;
using Framework.CustomContainer;
using IOCDI.Interface;

namespace IOCDI.Service
{
    public class TestServiceE : ITestServiceE
    {
        /// <summary>
        /// 方法注入TestServiceC
        /// </summary>
        private ITestServiceC _iTestServiceC = null;

        /// <summary>
        /// 方法注入TestServiceC
        /// </summary>
        /// <param name="iTestServiceC"></param>
        [CMethodInjection]
        public void GetInstanceTestServiceC(ITestServiceC iTestServiceC)
        {
            this._iTestServiceC = iTestServiceC;
        }


        public TestServiceE()
        {
            Console.WriteLine($"{this.GetType().Name} 被构造");
        }

        public void Show()
        {
            Console.WriteLine("E123456");
        }
    }
}