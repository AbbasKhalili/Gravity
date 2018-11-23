namespace Gravity.Core
{
    public interface IUnitofwork
    {
        void Begin();
        void Commit();
        void Rollback();
    }
}
