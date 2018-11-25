using System;

namespace Gravity.Tools
{
    public class BusinessException : Exception
    {
        public int Code { get; protected set; }
        public string ExceptionMessage { get; protected set; }
    }
}
