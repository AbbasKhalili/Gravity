using Geotechnic.Application.CommandHandlers;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.Order.Commands;
using Geotechnic.Facade.Contracts.Order.Services;
using Geotechnic.Persistence.Mappings;
using Geotechnic.Persistence.Repositories;
using Gravitest.NHibernate;
using Gravity.Application;
using Gravity.NHibernate;

namespace Geotechnic.Facade.Services.Tests.Unit.OrderFacade
{
    public class InMemoryOrderFacadeService : InMemoryDatabase
    {
        public IOrderRepository Repository;
        public IOrderFacadeService FacadeService;

        public InMemoryOrderFacadeService() : base(typeof(OrderMapping).Assembly)
        {
            ISequenceHelper sequenceHelper = new FakeSequenceHelper();
            Repository = new OrderRepository(Session, sequenceHelper);
            ICommandHandler<OrderCreate> commandHandler = new OrderCommandHandler(Repository);
            ICommandBus bus = new CommandBusFake<OrderCreate>(commandHandler, Session);
            FacadeService = new OrderFacadeService(bus);
        }
    }
}