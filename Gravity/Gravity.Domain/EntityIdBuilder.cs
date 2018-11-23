using System;

namespace Gravity.Domain
{
    public class EntityIdBuilder<T>
    {
        public long DbId { get; private set; }

        public T Build()
        {
            var instance = Activator.CreateInstance(typeof(T), DbId);
            return (T)instance;
        }

        public EntityIdBuilder<T> WithId(long id)
        {
            DbId = id;
            return this;
        }
    }
}