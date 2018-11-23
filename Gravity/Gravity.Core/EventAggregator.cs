using System.Collections.Generic;
using System.Linq;

namespace Gravity.Core
{
    public class EventAggregator : IEventAggregator
    {
        List<object> _subscribers = new List<object>();


        public void Publish<T>(T eventToPublish) where T : IDomainEvent
        {
            var handlers = _subscribers.OfType<IEventHandler<T>>().ToList();
            handlers.ForEach(a =>
            {
                a.Handle(eventToPublish);
            });
        }

        public void Subscribe<T>(IEventHandler<T> handler) where T : IDomainEvent
        {
            _subscribers.Add(handler);
        }
    }
}
