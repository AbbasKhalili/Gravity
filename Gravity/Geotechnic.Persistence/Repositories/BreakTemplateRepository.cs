using System.Linq;
using Geotechnic.Domain.BreakTemplates;
using Gravity.NHibernate;
using NHibernate;

namespace Geotechnic.Persistence.Repositories
{
    public class BreakTemplateRepository : IBreakTemplateRepository
    {
        private readonly ISession _session;
        private readonly ISequenceHelper _sequenceHelper;

        public BreakTemplateRepository(ISession session, ISequenceHelper sequenceHelper)
        {
            _session = session;
            _sequenceHelper = sequenceHelper;
        }

        public long GetNextId()
        {
            return _sequenceHelper.Next("BreakTemplateSeq");
        }

        public void Create(BreakTemplate entity)
        {
            _session.Save(entity);
        }

        public BreakTemplate Get(BreakTemplateId id)
        {
            return _session.Get<BreakTemplate>(id);
        }

        public BreakTemplate GetByIdAndBranchId(BreakTemplateId idKey, long branchId)
        {
            return _session.Query<BreakTemplate>().FirstOrDefault(x => x.Id == idKey && x.BranchId == branchId);
        }

        public void Delete(BreakTemplate entity)
        {
            _session.Delete(entity);
        }
    }
}
