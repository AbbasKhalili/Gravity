using Geotechnic.Application.CommandHandlers;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.BreakTemplate.Commands;
using Geotechnic.Persistence.Mappings;
using Geotechnic.Persistence.Repositories;
using Gravitest.NHibernate;
using Gravity.Application;
using Gravity.NHibernate;

namespace Geotechnic.Facade.Services.Tests.Unit
{
    public class InMemoryBreakTemplateFacadeService : InMemoryDatabase
    {
        public IBreakTemplateRepository Repository;
        public ICommandBus Bus;

        public InMemoryBreakTemplateFacadeService() : base(typeof(BreakTemplateMapping).Assembly)
        {
            ISequenceHelper sequenceHelper = new FakeSequenceHelper();
            Repository = new BreakTemplateRepository(Session, sequenceHelper);
            IOrderRepository orderRepository = new OrderRepository(Session, sequenceHelper);
            ICommandHandler<BreakTemplateCreate> commandHandler = new BreakTemplateCommandHandler(Repository, orderRepository);
            Bus = new CommandBusFake<BreakTemplateCreate>(commandHandler, Session);
        }
    }
}