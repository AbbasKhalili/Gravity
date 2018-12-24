using System;
using FluentAssertions;
using Geotechnic.Domain.Additives;
using Xunit;

namespace Geotechnic.Domain.Tests.Unit.AdditivesUnitTests
{
    public class AdditiveTests : EntityIdTest<AdditiveId>
    {
        private const string Title = "Lubricant H250 z";
        private const long BranchId = 1000;
        private const long Id = 100;
        private AdditiveId AdditiveId => IdBuilder.WithId(Id).Build();
        private readonly AdditiveBuilder _builder;

        public AdditiveTests()
        {
            _builder = new AdditiveBuilder();
        }


        [Fact]
        public void Constructor_should_create_properly_Additive()
        {
            var additive = _builder.WithTitle(Title).Build();
            additive.Title.Should().Be(Title);
            additive.BranchId.Should().Be(BranchId);
            additive.Id.Should().Be(AdditiveId);
        }

        [Fact]
        public void Update_should_change_the_Additive_to_new_values()
        {
            var additive = _builder.WithTitle(Title).Build();
            var expectedTitle = "Lubricant X2300 green";
            additive.Update(expectedTitle);
            additive.Title.Should().Be(expectedTitle);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Constructor_and_Update_should_throw_when_Title_is_not_valid(string title)
        {
            Action expected = () => _builder.WithTitle(title).Build();
            expected.Should().Throw<AdditiveTitleRequiredException>();
        }
    }
}