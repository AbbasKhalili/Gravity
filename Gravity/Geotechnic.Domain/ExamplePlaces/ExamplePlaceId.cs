using Gravity.Core;
using Gravity.Domain;

namespace Geotechnic.Domain.ExamplePlaces
{
    public class ExamplePlaceId : ValueObjectBase<ExamplePlaceId>
    {
        public long DbId { get; private set; }

        protected ExamplePlaceId() { }
        public ExamplePlaceId(long dbId)
        {
            DbId = dbId;
        }

        public override bool SameValueAs(ExamplePlaceId valueObject)
        {
            return this.DbId == valueObject?.DbId;
        }

        public override int HashCode()
        {
            return new HashCodeBuilder().Append(this.DbId).ToHashCode();
        }
    }
}
