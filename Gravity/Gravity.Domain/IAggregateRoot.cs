using Gravity.Core;

namespace Gravity.Domain
{
    public interface IAggregateRoot
    {
        IEventAggregator EventAggregator { get; set; }
    }
}
