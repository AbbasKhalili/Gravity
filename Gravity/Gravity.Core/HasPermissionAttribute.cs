using System;

namespace Gravity.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HasPermissionAttribute : Attribute
    {
        public long PermissionCode { get; private set; }
        public HasPermissionAttribute(object enumValue)
        {
            PermissionCode = (long)enumValue;
        }
    }
}
