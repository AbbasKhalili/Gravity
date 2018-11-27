using System;
using FluentAssertions;
using Geotechnic.Domain.OrderConcrete;
using Xunit;

namespace Geotechnic.Domain.Tests.Unit.OrderConcreteUnitTests
{
    public class BreakTests
    {
        private readonly BreakBuilder _breakBuilder;

        private const int Age = 7;
        private readonly DateTime _breakDate = DateTime.Now.Date;
        private const double Height = 14.8;
        private const double Length = 14.9;
        private const double Width = 15.1;
        private const int Weight = 7985;
        private const int Power = 35500;


        public BreakTests()
        {
            _breakBuilder = new BreakBuilder();
        }

        [Fact]
        public void Constructor_should_create_properly_Break_item()
        {
            var abreak = CreateValidBreak();

            abreak.BreakDate.Should().Be(_breakDate);
            abreak.Height.Should().Be(Height);
            abreak.Length.Should().Be(Length);
            abreak.Width.Should().Be(Width);
            abreak.Weight.Should().Be(Weight);
            abreak.Power.Should().Be(Power);
        }

        [Fact]
        public void SameValueAs_should_return_true_when_all_items_are_same()
        {
            var firstBreak = CreateValidBreak();
            var secondBreak = CreateValidBreak();
            var result = firstBreak.SameValueAs(secondBreak);
            result.Should().BeTrue();
        }

        [Fact]
        public void SameValueAs_should_return_False_when_one_of_property_has_different_value()
        {
            var firstBreak = CreateValidBreak();

            var height = 15.2;
            var secondBreak = _breakBuilder.WithAge(Age).WithBreakDate(_breakDate)
                .WithHeight(height).WithLength(Length).WithWidth(Width)
                .WithWeight(Weight).WithPower(Power).Build();

            var result = firstBreak.SameValueAs(secondBreak);
            result.Should().BeFalse();
        }

        private Break CreateValidBreak()
        {
            return _breakBuilder.WithAge(Age).WithBreakDate(_breakDate)
                .WithHeight(Height).WithLength(Length).WithWidth(Width)
                .WithWeight(Weight).WithPower(Power).Build();
        }
    }
}