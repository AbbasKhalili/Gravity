using Gravity.Core;
using NHibernate;

namespace Gravity.NHibernate
{
    public class NhUnitOfWork : IUnitofwork
    {
        private readonly ISession _session;
        public NhUnitOfWork(ISession session)
        {
            _session = session;
        }

        public void Begin()
        {
            _session.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
        }
    }
}
