using FluentAssertions;
using Geotechnic.Domain.Additives;
using Geotechnic.Facade.Contracts.Additives.Commands;
using Gravity.Application;
using Xunit;

namespace Geotechnic.Facade.Services.Tests.Unit.AdditiveFacade
{
    public class AdditiveFacadeServiceTests : InMemoryAdditiveFacadeService
    {
        private readonly EntityIdBuilder<AdditiveId> _idBuilder;

        public AdditiveFacadeServiceTests()
        {
            _idBuilder = new EntityIdBuilder<AdditiveId>();
        }

        private const long BranchId = 100;
        private const long Id = 1;
        private const string Title = "Wdss 5002";

        [Fact]
        public void Create_should_add_Additive_to_repository_by_commandBus()
        {
            DispatchAnAdditiveCreate();

            var id = _idBuilder.WithId(Id).Build();
            var expectedResult = Repository.Get(id);

            expectedResult.Id.Should().BeEquivalentTo(id);
            expectedResult.BranchId.Should().Be(BranchId);
            expectedResult.Title.Should().Be(Title);
        }

        [Fact]
        public void Update_should_modify_Additive_in_repository_by_commandBus()
        {
            DispatchAnAdditiveCreate();

            var title = "Az90";
            var updateCommand = new AdditiveUpdate()
                { BranchId = BranchId, Id = Id, Title = title };
            Bus.Dispatch(updateCommand);

            var id = _idBuilder.WithId(Id).Build();
            var expectedResult = Repository.Get(id);

            expectedResult.Id.Should().BeEquivalentTo(id);
            expectedResult.BranchId.Should().Be(BranchId);
            expectedResult.Title.Should().Be(title);
        }

        [Fact]
        public void Delete_should_remove_Additive_from_repository_by_commandBus()
        {
            DispatchAnAdditiveCreate();

            var deleteCommand = new AdditiveDelete()
                { BranchId = BranchId, Id = Id };
            Bus.Dispatch(deleteCommand);

            var id = _idBuilder.WithId(Id).Build();
            var expectedResult = Repository.Get(id);

            expectedResult.Should().BeNull();
        }

        private void DispatchAnAdditiveCreate()
        {
            var command = new AdditiveCreate()
            {
                BranchId = BranchId,
                Title = Title
            };
            Bus.Dispatch(command);
        }
    }
}