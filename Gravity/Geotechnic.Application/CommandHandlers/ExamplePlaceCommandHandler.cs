using System.Linq;
using Geotechnic.Application.Exceptions;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Gravity.Application;
using Gravity.Tools;

namespace Geotechnic.Application.CommandHandlers
{
    public class ExamplePlaceCommandHandler : ICommandHandler<ExamplePlaceCreate>,
                                              ICommandHandler<ExamplePlaceUpdate>,
                                              ICommandHandler<ExamplePlaceDelete>
    {

        private readonly IExamplePlaceRepository _examplePlaceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEntityIdBuilder<ExamplePlaceId> _idBuilder;

        public ExamplePlaceCommandHandler(IExamplePlaceRepository examplePlaceRepository, IOrderRepository orderRepository)
        {
            _examplePlaceRepository = examplePlaceRepository;
            _orderRepository = orderRepository;
            _idBuilder = new EntityIdBuilder<ExamplePlaceId>();
        }

        public void Handle(ExamplePlaceCreate command)
        {
            var nextId = _examplePlaceRepository.GetNextId();
            var id = _idBuilder.WithId(nextId).Build();

            var examplePlace = new ExamplePlace(command.BranchId,id,command.Character,command.Title);

            _examplePlaceRepository.Create(examplePlace);
        }

        public void Handle(ExamplePlaceUpdate command)
        {
            var id = _idBuilder.WithId(command.Id).Build();
            var model = _examplePlaceRepository.Get(id);

            Guard<ExamplePlaceNotFoundException>.AgainstNull(model);

            model.Update(command.Character, command.Title);
        }
        
        public void Handle(ExamplePlaceDelete command)
        {
            var order = _orderRepository.GetAll().FirstOrDefault(x => x.BranchId == command.BranchId && x.ExamplePlace.DbId == command.Id);
            Guard<ExamplePlaceUsedException>.AgainstNotNull(order);

            var id = _idBuilder.WithId(command.Id).Build();
            var model = _examplePlaceRepository.Get(id);

            Guard<ExamplePlaceNotFoundException>.AgainstNull(model);

            _examplePlaceRepository.Delete(model);
        }
    }
}
