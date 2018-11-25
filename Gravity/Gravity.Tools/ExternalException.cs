namespace Gravity.Tools
{
    public class ExternalException : BusinessException
    {
        protected ExternalException(int exceptionCode, string message)
        {
            base.Code = exceptionCode;
            base.ExceptionMessage = message;
        }
    }
}