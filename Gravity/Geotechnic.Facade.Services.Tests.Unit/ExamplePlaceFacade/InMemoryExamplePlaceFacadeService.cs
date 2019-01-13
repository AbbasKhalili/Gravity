using Geotechnic.Application.CommandHandlers;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Geotechnic.Persistence.Mappings;
using Geotechnic.Persistence.Repositories;
using Gravitest.NHibernate;
using Gravity.Application;
using Gravity.NHibernate;

namespace Geotechnic.Facade.Services.Tests.Unit.ExamplePlaceFacade
{
    public class InMemoryExamplePlaceFacadeService : InMemoryDatabase
    {
        public IExamplePlaceRepository Repository;
        public ICommandBus Bus;

        public InMemoryExamplePlaceFacadeService() : base(typeof(ExamplePlaceMapping).Assembly)
        {
            ISequenceHelper sequenceHelper = new FakeSequenceHelper();
            Repository = new ExamplePlaceRepository(sequenceHelper, Session);
            IOrderRepository orderRepository = new OrderRepository(Session, sequenceHelper);
            ICommandHandler<ExamplePlaceCreate> commandHandler = new ExamplePlaceCommandHandler(Repository, orderRepository);
            Bus = new CommandBusFake<ExamplePlaceCreate>(commandHandler, Session);
        }
    }
}