namespace Gravity.Tools
{
    public class InternalException : BusinessException
    {
        public InternalException(int exceptionCode)
        {
            base.Code = exceptionCode;
        }
    }
}