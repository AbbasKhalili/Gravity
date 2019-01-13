using System.Collections.Generic;
using FluentAssertions;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Facade.Contracts.Additives.Commands;
using Geotechnic.Facade.Contracts.BreakTemplate.Commands;
using Gravity.Application;
using Xunit;

namespace Geotechnic.Facade.Services.Tests.Unit
{
    public class BreakTemplateFacadeServiceTests : InMemoryBreakTemplateFacadeService
    {
        private readonly EntityIdBuilder<BreakTemplateId> _idBuilder;

        public BreakTemplateFacadeServiceTests()
        {
            _idBuilder = new EntityIdBuilder<BreakTemplateId>();
        }

        private const long BranchId = 100;
        private const long Id = 1;
        private const int MoldCount = 5;
        private const string Title = "7-28-90";
        private readonly List<Molds> _moldList = new List<Molds>()
        {
            new Molds() {Age = 7, Count = 2}, new Molds() {Age = 28, Count = 2}, new Molds() {Age = 90, Count = 1}
        };

        [Fact]
        public void Create_should_add_BreakTemplate_to_repository_by_commandBus()
        {
            DispatchBreakTemplateCreate();

            var id = _idBuilder.WithId(Id).Build();
            var expectedResult = Repository.Get(id);

            expectedResult.Id.Should().BeEquivalentTo(id);
            expectedResult.BranchId.Should().Be(BranchId);
            expectedResult.Title.Should().Be(Title);
            expectedResult.MoldCount.Should().Be(MoldCount);
            expectedResult.BreakTemplateMolds.Should().BeEquivalentTo(_moldList);
        }

        [Fact]
        public void Update_should_modify_BreakTemplate_in_repository_by_commandBus()
        {
            DispatchBreakTemplateCreate();

            var title = "7-28";
            var moldCount = 4;
            var moldList = new List<Molds>()
            {
                new Molds() {Age = 7, Count = 2}, new Molds() {Age = 28, Count = 2}
            };
            var updateCommand = new BreakTemplateUpdate()
                { BranchId = BranchId, Id = Id, Title = title,MoldCount = moldCount, MoldList = moldList };
            Bus.Dispatch(updateCommand);

            var id = _idBuilder.WithId(Id).Build();
            var expectedResult = Repository.Get(id);

            expectedResult.Id.Should().BeEquivalentTo(id);
            expectedResult.BranchId.Should().Be(BranchId);
            expectedResult.Title.Should().Be(title);
            expectedResult.MoldCount.Should().Be(moldCount);
            expectedResult.BreakTemplateMolds.Should().BeEquivalentTo(moldList);
        }

        [Fact]
        public void Delete_should_remove_BreakTemplate_from_repository_by_commandBus()
        {
            DispatchBreakTemplateCreate();

            var deleteCommand = new BreakTemplateDelete()
                { BranchId = BranchId, Id = Id };
            Bus.Dispatch(deleteCommand);

            var id = _idBuilder.WithId(Id).Build();
            var expectedResult = Repository.Get(id);

            expectedResult.Should().BeNull();
        }

        private void DispatchBreakTemplateCreate()
        {
            var command = new BreakTemplateCreate()
            {
                BranchId = BranchId,
                Title = Title,
                MoldCount = MoldCount,
                MoldList = _moldList
            };
            Bus.Dispatch(command);
        }
    }
}