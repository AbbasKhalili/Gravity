using Gravity.Core;
using Gravity.Domain;

namespace Geotechnic.Domain.OrderConcrete
{
    public class OrderId : ValueObjectBase<OrderId>
    {
        public long DbId { get; private set; }

        protected OrderId() { }
        public OrderId(long dbId)
        {
            DbId = dbId;
        }

        public override bool SameValueAs(OrderId valueObject)
        {
            return this.DbId == valueObject?.DbId;
        }

        public override int HashCode()
        {
            return new HashCodeBuilder().Append(this.DbId).ToHashCode();
        }
    }
}
