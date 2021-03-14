using System;
using Framework.CustomAOP;

namespace IOCDI.Interface
{
    public interface ITestServiceB
    {
        [LogBefore]
        [LogAfter]
        [LoginAfter]
        [Monitor]
        void Show();

        void Show_1();
    }
}
