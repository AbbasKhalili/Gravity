using FluentAssertions;
using Geotechnic.Application.CommandHandlers;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Gravity.Application;
using NSubstitute;
using NSubstitute.Core;
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
        private const string Character = "C";
        private const string Title = "Column";

        [Fact]
        public void HandleCreate_should_add_ExamplePlace_to_repository()
        {
            var command = new ExamplePlaceCreate { BranchId = BranchId, Character = Character, Title=Title};
            var id = _examplePlaceBuilder.WithId(Id).Build();
            var expectedExamplePlace = new ExamplePlace(BranchId, id, Character, Title);
            var repository = Substitute.For<IExamplePlaceRepository>();
            var commandHandler = new ExamplePlaceCommandHandler(repository);

            commandHandler.Handle(command);

            repository.Received(1)
                .Create(Verify.That<ExamplePlace>(
                    a => a.Should().BeEquivalentTo(expectedExamplePlace,
                        z => z.Excluding(b => b.Id).ComparingByMembers<ExamplePlace>())));
        }

        [Fact]
        public void HandleUpdate_should_modify_ExamplePlace_in_repository()
        {
            var id = _examplePlaceBuilder.WithId(Id).Build();
            var returnValue = new ExamplePlace(BranchId, id, Character, Title);
            var repository = Substitute.For<IExamplePlaceRepository>();
            repository.GetByIdAndBranchId(id, BranchId).Returns(returnValue);
            
            var character = "W";
            var title = "Wall";
            var command = new ExamplePlaceUpdate { BranchId = BranchId, Id = Id, Character = character, Title = title };
            var commandHandler = new ExamplePlaceCommandHandler(repository);
            commandHandler.Handle(command);

            returnValue.Character.Should().Be(character);
            returnValue.Title.Should().Be(title);
        }
    }
}
