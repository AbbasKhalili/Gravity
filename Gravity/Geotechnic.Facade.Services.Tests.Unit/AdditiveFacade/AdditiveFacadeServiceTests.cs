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
        private AdditiveId AdditiveId => _idBuilder.WithId(Id).Build();

        [Fact]
        public void Create_should_add_Additive_to_repository_by_commandBus()
        {
            DispatchAnAdditiveCreate();

            var expectedResult = Repository.Get(AdditiveId);

            expectedResult.Id.Should().BeEquivalentTo(AdditiveId);
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
            FacadeService.Modify(updateCommand);

            var expectedResult = Repository.Get(AdditiveId);

            expectedResult.Id.Should().BeEquivalentTo(AdditiveId);
            expectedResult.BranchId.Should().Be(BranchId);
            expectedResult.Title.Should().Be(title);
        }

        [Fact]
        public void Delete_should_remove_Additive_from_repository_by_commandBus()
        {
            DispatchAnAdditiveCreate();

            var deleteCommand = new AdditiveDelete()
                { BranchId = BranchId, Id = Id };
            FacadeService.Delete(deleteCommand);

            var expectedResult = Repository.Get(AdditiveId);

            expectedResult.Should().BeNull();
        }

        private void DispatchAnAdditiveCreate()
        {
            var command = new AdditiveCreate()
            {
                BranchId = BranchId,
                Title = Title
            };
            FacadeService.Create(command);
        }
    }
}