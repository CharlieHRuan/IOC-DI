using System;
using IOCDI.Interface;

namespace IOCDI.Service
{
    public class TestServiceC : ITestServiceC
    {

        public TestServiceC()
        {
            Console.WriteLine($"{this.GetType().Name} 被构造");
        }
        public void Show()
        {
            Console.WriteLine("C123456");
        }
    }
}
