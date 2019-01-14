using Geotechnic.Application.CommandHandlers;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.BreakTemplate.Commands;
using Geotechnic.Facade.Contracts.BreakTemplate.Services;
using Geotechnic.Persistence.Mappings;
using Geotechnic.Persistence.Repositories;
using Gravitest.NHibernate;
using Gravity.Application;
using Gravity.NHibernate;

namespace Geotechnic.Facade.Services.Tests.Unit.BreakTemplateFacade
{
    public class InMemoryBreakTemplateFacadeService : InMemoryDatabase
    {
        public IBreakTemplateRepository Repository;
        public IBreakTemplateFacadeService FacadeService;

        public InMemoryBreakTemplateFacadeService() : base(typeof(BreakTemplateMapping).Assembly)
        {
            ISequenceHelper sequenceHelper = new FakeSequenceHelper();
            Repository = new BreakTemplateRepository(Session, sequenceHelper);
            IOrderRepository orderRepository = new OrderRepository(Session, sequenceHelper);
            ICommandHandler<BreakTemplateCreate> commandHandler = new BreakTemplateCommandHandler(Repository, orderRepository);
            ICommandBus bus = new CommandBusFake<BreakTemplateCreate>(commandHandler, Session);
            FacadeService = new BreakTemplateFacadeService(bus);
        }
    }
}