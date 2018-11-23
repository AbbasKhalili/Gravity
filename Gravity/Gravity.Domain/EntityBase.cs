using Gravity.Core;

namespace Gravity.Domain
{
    public abstract class EntityBase<T>
    {
        public long BranchId { get; protected set; }

        public T Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var entity = obj as EntityBase<T>;
            if (entity == null) return false;

            return this.Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(this.Id).ToHashCode();
        }

    }
}
