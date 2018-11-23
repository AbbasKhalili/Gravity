namespace Gravity.Domain
{
    public interface IValueObject<T> where T : class
    {
        bool SameValueAs(T valueObject);
        int HashCode();
    }
}
