using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Gravity.Application;

namespace Geotechnic.Application.CommandHandlers
{
    public class ExamplePlaceCommandHandler : ICommandHandler<ExamplePlaceCreate>
    {
        private readonly IExamplePlaceRepository _examplePlaceRepository;
        private readonly IEntityIdBuilder<ExamplePlaceId> _idBuilder;

        public ExamplePlaceCommandHandler(IExamplePlaceRepository examplePlaceRepository)
        {
            _examplePlaceRepository = examplePlaceRepository;
            _idBuilder = new EntityIdBuilder<ExamplePlaceId>();
        }

        public void Handle(ExamplePlaceCreate command)
        {
            var nextId = _examplePlaceRepository.GetNextId();
            var id = _idBuilder.WithId(nextId).Build();

            var examplePlace = new ExamplePlace(command.BranchId,id,command.Character,command.Title);

            _examplePlaceRepository.Create(examplePlace);
        }

    }
}
