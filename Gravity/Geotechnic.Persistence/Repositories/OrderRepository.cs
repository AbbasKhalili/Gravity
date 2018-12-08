using System.Linq;
using Geotechnic.Domain.OrderConcrete;
using Gravity.NHibernate;
using NHibernate;

namespace Geotechnic.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ISession _session;
        private readonly ISequenceHelper _sequenceHelper;

        public OrderRepository(ISession session, ISequenceHelper sequenceHelper)
        {
            _session = session;
            _sequenceHelper = sequenceHelper;
        }

        public long GetNextId()
        {
            return _sequenceHelper.Next("OrderConcreteSeq");
        }

        public void Create(Order entity)
        {
            _session.Save(entity);
        }

        public Order Get(OrderId id)
        {
            return _session.Get<Order>(id);
        }

        public Order GetByIdAndBranchId(OrderId idKey, long branchId)
        {
            return _session.Query<Order>().FirstOrDefault(x => x.Id == idKey && x.BranchId == branchId);
        }

        public void Delete(Order entity)
        {
            _session.Delete(entity);
        }

        public long GetExampleNumber(long companyId)
        {
            return _session.CreateSQLQuery("SELECT MAX(ExampleNumber) FROM OrderConcretes WHERE CompanyId=" + companyId).UniqueResult<long>();
        }

        public IQueryable<Order> GetAll()
        {
            return _session.Query<Order>();
        }
    }
}