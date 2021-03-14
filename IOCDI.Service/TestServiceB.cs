using System;
using Framework.CustomAOP;
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
            Console.WriteLine($"方法{nameof(Show)}被调用");
        }

        public void Show_1()
        {
            Console.WriteLine($"方法{nameof(Show_1)}被调用");
        }
    }
}
