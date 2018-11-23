namespace Gravity.Core
{
    public interface IEventHandler<T> where T : IDomainEvent
    {
        void Handle(T eventToHandle);
    }
}
