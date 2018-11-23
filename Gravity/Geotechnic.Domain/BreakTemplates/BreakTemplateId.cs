using Gravity.Core;
using Gravity.Domain;

namespace Geotechnic.Domain.BreakTemplates
{
    public class BreakTemplateId : ValueObjectBase<BreakTemplateId>
    {
        public long DbId { get; private set; }

        protected BreakTemplateId() { }
        public BreakTemplateId(long dbId)
        {
            DbId = dbId;
        }

        public override bool SameValueAs(BreakTemplateId valueObject)
        {
            return this.DbId == valueObject?.DbId;
        }

        public override int HashCode()
        {
            return new HashCodeBuilder().Append(this.DbId).ToHashCode();
        }
    }
}