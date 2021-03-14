using System;
using System.Net.Http.Headers;
using Castle.DynamicProxy;

namespace Framework.CustomAOP
{
    public class CustomAOPTest
    {
        public static void Show()
        {
            //动态代理对象
            ProxyGenerator generator = new ProxyGenerator();
            //注入对象
            CustomInterceptor interceptor = new CustomInterceptor();
            //原始对象
            CustomClass test = generator.CreateClassProxy<CustomClass>(interceptor);

            Console.WriteLine($"当前类型：{test.GetType()}，父类型：{test.GetType().BaseType}");
            Console.WriteLine();
            test.MethodInterceptor();
            Console.WriteLine();
            test.MethodNoInterceptor();
            Console.WriteLine();
        }
    }
}