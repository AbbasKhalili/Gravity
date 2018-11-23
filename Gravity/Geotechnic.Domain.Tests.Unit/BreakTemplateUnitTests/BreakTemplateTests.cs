using FluentAssertions;
using Geotechnic.Domain.BreakTemplates;
using Xunit;

namespace Geotechnic.Domain.Tests.Unit.BreakTemplateUnitTests
{
    public class BreakTemplateTests : EntityIdTest<BreakTemplateId>
    {
        private readonly BreakTemplateBuilder _builder;
        private readonly BreakTemplateMoldsBuilder _moldsBuilder;
        
        private const long Id = 100;
        private const long BranchId = 1000;
        private const string Title = "7-28-90";
        private const int Count = 5;

        public BreakTemplateTests()
        {
            _builder = new BreakTemplateBuilder();
            _moldsBuilder = new BreakTemplateMoldsBuilder();
        }

        [Fact]
        public void Constructor_should_create_breakTemplate_properly()
        {
            
            var id = IdBuilder.WithId(Id).Build();
            var molds = _moldsBuilder.WithItem(id, 7, 2).WithItem(id, 28, 2).WithItem(id, 90, 1).Build();
            var breakTemplate = _builder.WithMoldCount(Count).WithTitle(Title).WithMoldsList(molds).Build();
            breakTemplate.Id.Should().Be(id);
            breakTemplate.Title.Should().Be(Title);
            breakTemplate.MoldCount.Should().Be(Count);
            breakTemplate.BranchId.Should().Be(BranchId);
            breakTemplate.BreakTemplateMolds.Should().BeSameAs(molds);
        }

        [Fact]
        public void Update_should_change_the_breakTemplate_to_new_values()
        {
            var id = IdBuilder.WithId(Id).Build();
            var molds = _moldsBuilder.WithItem(id, 7, 2).WithItem(id, 28, 2).WithItem(id, 90, 1).Build();
            var examplePlace = _builder.WithMoldCount(Count).WithTitle(Title).WithMoldsList(molds).Build();

            var expectedTitle = "11-42-90";
            var expectedCount = 4;
            var expectedMolds = _moldsBuilder.WithItem(id, 11, 1).WithItem(id, 42, 2).WithItem(id, 90, 1).Build();
            examplePlace.Update(expectedTitle, expectedCount, expectedMolds);

            examplePlace.Title.Should().Be(expectedTitle);
            examplePlace.MoldCount.Should().Be(expectedCount);
            examplePlace.BreakTemplateMolds.Should().BeEquivalentTo(expectedMolds);
        }
    }
}