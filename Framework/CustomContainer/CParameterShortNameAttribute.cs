using System;

namespace Framework.CustomContainer
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class CParameterShortNameAttribute : Attribute
    {
        public string _shortName { get; private set; }

        public CParameterShortNameAttribute(string shortName)
        {
            _shortName = shortName;
        }
    }
}