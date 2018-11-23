using NHibernate;

namespace Gravity.NHibernate
{
    public class SequenceHelper : ISequenceHelper
    {
        private readonly ISession _session;

        public SequenceHelper(ISession session)
        {
            this._session = session;
        }

        public long Next(string sequenceName)
        {
            return this._session.CreateSQLQuery("SELECT NEXT VALUE FOR " + sequenceName).UniqueResult<long>();
        }
    }
}
