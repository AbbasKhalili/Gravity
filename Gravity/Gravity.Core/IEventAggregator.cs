namespace Gravity.Core
{
    public interface IEventAggregator
    {
        void Publish<T>(T eventToPublish) where T : IDomainEvent;
        void Subscribe<T>(IEventHandler<T> handler) where T : IDomainEvent;
    }
}
