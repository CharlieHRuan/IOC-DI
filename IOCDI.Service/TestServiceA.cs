using System;
using Framework.CustomContainer;
using IOCDI.Interface;

namespace IOCDI.Service
{
    public class TestServiceA : ITestServiceA
    {
        private readonly string _sIndex;
        private readonly ITestServiceB _iTestServiceB;
        private readonly int _index;

        public TestServiceA([CParameterInjection] string sIndex, ITestServiceB iTestServiceB,
            [CParameterInjection] int index)
        {
            _sIndex = sIndex;
            _iTestServiceB = iTestServiceB;
            _index = index;
            Console.WriteLine($"{GetType().Name} 被构造..{sIndex} {index}");
        }

        public void Show()
        {
            Console.WriteLine("A123456");
        }
    }
}