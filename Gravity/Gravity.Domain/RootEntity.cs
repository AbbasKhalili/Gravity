using Gravity.Core;

namespace Gravity.Domain
{
    public abstract class RootEntity<T>
    {
        public T Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var entity = obj as RootEntity<T>;
            if (entity == null) return false;

            return this.Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return new HashCodeBuilder().Append(this.Id).ToHashCode();
        }
    }
}
