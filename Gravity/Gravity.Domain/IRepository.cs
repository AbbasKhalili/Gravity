namespace Gravity.Domain
{
    public interface IRepository
    {
    }

    public interface IRepository<T, in TKey> : IRepository where T : class where TKey : class
    {
        long GetNextId();
        void Create(T entity);
        T Get(TKey id);
        T GetByIdAndBranchId(TKey idKey, long branchId);
        void Delete(T entity);
    }
}
