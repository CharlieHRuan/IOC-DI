using System;

namespace Framework.CustomContainer
{
    //限制只有属性可以使用
    [AttributeUsage(AttributeTargets.Property)]
    public class CPropertyInjectionAttribute : Attribute
    {
        
    }
}