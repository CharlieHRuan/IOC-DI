using System;
using Framework.CustomContainer;
using IOCDI.Interface;

namespace IOCDI.Service
{
    public class TestServiceD : ITestServiceD
    {
        /// <summary>
        /// 在创建TestServiceD的同时，创建一个E的属性
        /// </summary>
        [CPropertyInjection]
        public ITestServiceE TestServiceE { get; set; }


        public TestServiceD( /*int n*/)
        {
            Console.WriteLine($"{this.GetType().Name} 被构造");
        }

        // //顺便标记一个特性，传入参数写死
        // [XXXXAttribute]
        // public TestServiceD() : this(3)
        // {
        //
        // }

        public void Show()
        {
            Console.WriteLine("D123456");
        }
    }
}