using Gravity.NHibernate;

namespace Gravitest.NHibernate
{
    public class FakeSequenceHelper : ISequenceHelper
    {
        public long Next(string sequenceName)
        {
            return 1;
        }
    }
}
