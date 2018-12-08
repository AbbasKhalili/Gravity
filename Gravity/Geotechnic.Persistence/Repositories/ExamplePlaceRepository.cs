using System.Linq;
using Geotechnic.Domain.ExamplePlaces;
using Gravity.NHibernate;
using NHibernate;

namespace Geotechnic.Persistence.Repositories
{
    public class ExamplePlaceRepository : IExamplePlaceRepository
    {
        private readonly ISession _session;
        private readonly ISequenceHelper _sequenceHelper;

        public ExamplePlaceRepository(ISequenceHelper sequenceHelper, ISession session)
        {
            _sequenceHelper = sequenceHelper;
            _session = session;
        }

        public long GetNextId()
        {
            return _sequenceHelper.Next("ExamplePlaceSeq");
        }

        public void Create(ExamplePlace entity)
        {
            _session.Save(entity);
        }

        public ExamplePlace Get(ExamplePlaceId id)
        {
            return _session.Get<ExamplePlace>(id);
        }

        public ExamplePlace GetByIdAndBranchId(ExamplePlaceId idKey, long branchId)
        {
            return _session.Query<ExamplePlace>().FirstOrDefault(x => x.Id == idKey && x.BranchId == branchId);
        }

        public void Delete(ExamplePlace entity)
        {
            _session.Delete(entity);
        }
    }
}
