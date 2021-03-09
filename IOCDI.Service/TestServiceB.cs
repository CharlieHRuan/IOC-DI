using System;
using IOCDI.Interface;

namespace IOCDI.Service
{
    public class TestServiceB : ITestServiceB
    {

        public TestServiceB()
        {
            Console.WriteLine($"{this.GetType().Name} 被构造");
        }
        public void Show()
        {
            Console.WriteLine("B123456");
        }
    }
}
