using FluentAssertions;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Gravity.Application;
using Xunit;

namespace Geotechnic.Facade.Services.Tests.Unit.ExamplePlaceFacade
{
    public class ExamplePlaceFacadeServiceTests : InMemoryExamplePlaceFacadeService
    {
        private readonly EntityIdBuilder<ExamplePlaceId> _idBuilder;

        public ExamplePlaceFacadeServiceTests()
        {
            _idBuilder = new EntityIdBuilder<ExamplePlaceId>();
        }

        private const long BranchId = 100;
        private const long Id = 1;
        private const string Character = "C";
        private const string Title = "Column";
        private ExamplePlaceId ExamplePlaceId => _idBuilder.WithId(Id).Build();

        [Fact]
        public void Create_should_add_ExamplePlace_to_repository_by_commandBus()
        {
            DispatchAnExamplePlaceCreate();
            
            var expectedResult = Repository.Get(ExamplePlaceId);

            expectedResult.Id.Should().BeEquivalentTo(ExamplePlaceId);
            expectedResult.BranchId.Should().Be(BranchId);
            expectedResult.Title.Should().Be(Title);
            expectedResult.Character.Should().Be(Character);
        }

        [Fact]
        public void Update_should_modify_ExamplePlace_in_repository_by_commandBus()
        {
            DispatchAnExamplePlaceCreate();

            var title = "Wall";
            var character = "W";
            var updateCommand = new ExamplePlaceUpdate()
                {BranchId = BranchId, Id = Id, Title = title, Character = character };
            FacadeService.Modify(updateCommand);

            var expectedResult = Repository.Get(ExamplePlaceId);

            expectedResult.Id.Should().BeEquivalentTo(ExamplePlaceId);
            expectedResult.BranchId.Should().Be(BranchId);
            expectedResult.Title.Should().Be(title);
            expectedResult.Character.Should().Be(character);
        }

        [Fact]
        public void Delete_should_remove_ExamplePlace_from_repository_by_commandBus()
        {
            DispatchAnExamplePlaceCreate();

           var deleteCommand = new ExamplePlaceDelete()
                { BranchId = BranchId, Id = Id };
            FacadeService.Delete(deleteCommand);

            var expectedResult = Repository.Get(ExamplePlaceId);

            expectedResult.Should().BeNull();
        }

        private void DispatchAnExamplePlaceCreate()
        {
            var command = new ExamplePlaceCreate()
            {
                BranchId = BranchId,
                Title = Title,
                Character = Character
            };
            FacadeService.Create(command);
        }
    }
}
