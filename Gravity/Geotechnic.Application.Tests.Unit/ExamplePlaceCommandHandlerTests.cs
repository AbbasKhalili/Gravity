using FluentAssertions;
using Geotechnic.Application.CommandHandlers;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Gravity.Application;
using NSubstitute;
using Xunit;

namespace Geotechnic.Application.Tests.Unit
{
    public class ExamplePlaceCommandHandlerTests
    {
        private readonly EntityIdBuilder<ExamplePlaceId> _examplePlaceBuilder;

        public ExamplePlaceCommandHandlerTests()
        {
            _examplePlaceBuilder = new EntityIdBuilder<ExamplePlaceId>();
        }


        private const int BranchId = 100;
        private const int Id = 1;

        [Fact]
        public void HandleCreate_should_add_ExamplePlace_to_repository()
        {
            var character = "C";
            var title = "Column";
            var command = new ExamplePlaceCreate { BranchId = BranchId, Character = character, Title=title};
            var id = _examplePlaceBuilder.WithId(Id).Build();
            var expectedExamplePlace = new ExamplePlace(BranchId, id, character, title);
            var repository = Substitute.For<IExamplePlaceRepository>();
            var commandHandler = new ExamplePlaceCommandHandler(repository);

            commandHandler.Handle(command);

            repository.Received(1)
                .Create(Verify.That<ExamplePlace>(
                    a => a.Should().BeEquivalentTo(expectedExamplePlace,
                        z => z.Excluding(b => b.Id).ComparingByMembers<ExamplePlace>())));
        }
    }
}
