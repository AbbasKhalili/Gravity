namespace Gravity.NHibernate
{
    public interface ISequenceHelper
    {
        long Next(string sequenceName);
    }
}
