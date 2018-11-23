using Gravity.Core;
using Gravity.Domain;

namespace Geotechnic.Domain.Additives
{
    public class AdditiveId : ValueObjectBase<AdditiveId>
    {
        public long DbId { get; private set; }

        protected AdditiveId() { }
        public AdditiveId(long dbId)
        {
            DbId = dbId;
        }

        public override bool SameValueAs(AdditiveId valueObject)
        {
            return this.DbId == valueObject?.DbId;
        }

        public override int HashCode()
        {
            return new HashCodeBuilder().Append(this.DbId).ToHashCode();
        }
    }
}
