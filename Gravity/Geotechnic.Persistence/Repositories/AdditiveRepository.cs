using Geotechnic.Domain.Additives;
using Gravity.NHibernate;
using NHibernate;
using System.Linq;

namespace Geotechnic.Persistence.Repositories
{
    public class AdditiveRepository : IAdditiveRepository
    {
        private readonly ISession _session;
        private readonly ISequenceHelper _sequenceHelper;

        public AdditiveRepository(ISession session, ISequenceHelper sequenceHelper)
        {
            _session = session;
            _sequenceHelper = sequenceHelper;
        }

        public long GetNextId()
        {
            return _sequenceHelper.Next("AdditiveSeq");
        }

        public void Create(Additive entity)
        {
            _session.Save(entity);
        }

        public Additive Get(AdditiveId id)
        {
            return _session.Get<Additive>(id);
        }

        public Additive GetByIdAndBranchId(AdditiveId idKey, long branchId)
        {
            return _session.Query<Additive>().FirstOrDefault(x => x.Id == idKey && x.BranchId == branchId);
        }

        public void Delete(Additive entity)
        {
            _session.Delete(entity);
        }
    }
}
