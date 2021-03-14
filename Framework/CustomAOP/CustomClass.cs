using System;

namespace Framework.CustomAOP
{
    public class CustomClass
    {
        public virtual void MethodInterceptor()
        {
            Console.WriteLine($"This's Interceptor");
        }

        public void MethodNoInterceptor()
        {
            Console.WriteLine($"This's without Interceptor");
        }
    }
}