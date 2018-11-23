using FluentAssertions;
using Geotechnic.Domain.ExamplePlaces;
using Xunit;

namespace Geotechnic.Domain.Tests.Unit.ExamplePlaceUnitTests
{
    public class ExamplePlaceTests : EntityIdTest<ExamplePlaceId>
    {
        private const string Title = "Wall";
        private const string Character = "W";
        private const long BranchId = 1000;

        private readonly ExamplePlaceBuilder _builder;

        public ExamplePlaceTests()
        {
            _builder = new ExamplePlaceBuilder();
        }

        [Fact]
        public void Constructor_should_create_properly_ExamplePlace()
        {
            var id = IdBuilder.WithId(100).Build();
            var examplePlace = _builder.WithTitle(Title).WithCharacter(Character).Build();
            examplePlace.Character.Should().Be(Character);
            examplePlace.Title.Should().Be(Title);
            examplePlace.BranchId.Should().Be(BranchId);
            examplePlace.Id.Should().Be(id);
        }

        [Fact]
        public void Update_should_change_the_examplePlace_to_new_values()
        {
            var examplePlace = _builder.WithTitle(Title).WithCharacter(Character).Build();

            var expectedCharacter = "C";
            var expectedTitle = "Column";
            examplePlace.Update(expectedCharacter, expectedTitle);

            examplePlace.Character.Should().Be(expectedCharacter);
            examplePlace.Title.Should().Be(expectedTitle);
        }
    }
}