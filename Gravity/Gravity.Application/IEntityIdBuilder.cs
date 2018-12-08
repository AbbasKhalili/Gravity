namespace Gravity.Application
{
    public interface IEntityIdBuilder<T>
    {
        T Build();
        EntityIdBuilder<T> WithId(long id);
    }
}