using System;

namespace Framework.CustomContainer
{
    /// <summary>
    /// 限制传参可以使用
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CParameterInjectionAttribute: Attribute
    {
        
    }
}