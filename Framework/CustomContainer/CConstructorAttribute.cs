using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.CustomContainer
{
    //只允许构造函数使用
    [AttributeUsage(AttributeTargets.Constructor)]
    public class CConstructorAttribute : Attribute
    {
    }
}