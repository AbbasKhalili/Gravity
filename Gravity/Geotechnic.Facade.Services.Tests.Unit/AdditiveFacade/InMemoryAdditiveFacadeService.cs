using Geotechnic.Application.CommandHandlers;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.Additives.Commands;
using Geotechnic.Facade.Contracts.Additives.Services;
using Geotechnic.Persistence.Mappings;
using Geotechnic.Persistence.Repositories;
using Gravitest.NHibernate;
using Gravity.Application;
using Gravity.NHibernate;

namespace Geotechnic.Facade.Services.Tests.Unit.AdditiveFacade
{
    public class InMemoryAdditiveFacadeService : InMemoryDatabase
    {
        public IAdditiveRepository Repository;
        public IAdditivesFacadeService FacadeService;

        public InMemoryAdditiveFacadeService() : base(typeof(AdditiveMapping).Assembly)
        {
            ISequenceHelper sequenceHelper = new FakeSequenceHelper();
            Repository = new AdditiveRepository(Session, sequenceHelper);
            IOrderRepository orderRepository = new OrderRepository(Session, sequenceHelper);
            ICommandHandler<AdditiveCreate> commandHandler = new AdditiveCommandHandler(Repository, orderRepository);
            ICommandBus bus = new CommandBusFake<AdditiveCreate>(commandHandler, Session);
            FacadeService = new AdditivesFacadeService(bus);
        }
    }
}