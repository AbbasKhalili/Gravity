using System.Linq;
using Geotechnic.Application.Exceptions;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.BreakTemplate.Commands;
using Gravity.Application;
using Gravity.Tools;

namespace Geotechnic.Application.CommandHandlers
{
    public class BreakTemplateCommandHandler : ICommandHandler<BreakTemplateCreate>,
        ICommandHandler<BreakTemplateUpdate>, ICommandHandler<BreakTemplateDelete>
    {
        private readonly IBreakTemplateRepository _repository;
        private readonly IEntityIdBuilder<BreakTemplateId> _idBuilder;
        private readonly IOrderRepository _orderRepository;

        public BreakTemplateCommandHandler(IBreakTemplateRepository repository, IOrderRepository orderRepository)
        {
            _repository = repository;
            _orderRepository = orderRepository;
            _idBuilder = new EntityIdBuilder<BreakTemplateId>();
        }

        public void Handle(BreakTemplateCreate command)
        {
            var nextId = _repository.GetNextId();
            var id = _idBuilder.WithId(nextId).Build();

            var moldList = command.MoldList.Select(a => new BreakTemplateMolds()
                {Age = a.Age, Count = a.Count, BreakTemplateId = id}).ToList();
             var examplePlace = new BreakTemplate(command.BranchId, id, command.Title,command.MoldCount, moldList);

            _repository.Create(examplePlace);
        }

        public void Handle(BreakTemplateUpdate command)
        {
            var id = _idBuilder.WithId(command.Id).Build();
            var model = _repository.Get(id);
            var moldList = command.MoldList.Select(a => new BreakTemplateMolds()
                {Age = a.Age, Count = a.Count, BreakTemplateId = id}).ToList();
            Guard<BreakTemplateNotFoundException>.AgainstNull(model);

            model.Update(command.Title,command.MoldCount,moldList);
        }

        public void Handle(BreakTemplateDelete command)
        {
            var order = _orderRepository.GetAll().FirstOrDefault(x => x.BranchId == command.BranchId && x.ExamplePlace.DbId == command.Id);
            Guard<BreakTemplateUsedException>.AgainstNotNull(order);

            var id = _idBuilder.WithId(command.Id).Build();
            var model = _repository.Get(id);

            Guard<BreakTemplateNotFoundException>.AgainstNull(model);

            _repository.Delete(model);
        }
    }
}
