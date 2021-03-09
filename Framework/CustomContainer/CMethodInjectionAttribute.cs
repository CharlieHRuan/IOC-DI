using System;

namespace Framework.CustomContainer
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CMethodInjectionAttribute : Attribute
    {
    }
}