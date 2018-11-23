using FluentAssertions;
using Gravity.Domain;
using Xunit;

namespace Geotechnic.Domain.Tests.Unit
{
    public abstract class EntityIdTest<T> where T : ValueObjectBase<T>
    {
        protected EntityIdBuilder<T> IdBuilder => new EntityIdBuilder<T>();

        private const long DbId = 100;


        //[Fact]
        //public void Constructor_should_create_properly_EntityId()
        //{
        //    var actual = IdBuilder.WithId(DbId).Build();
        //    //actual.DbId.Should().Be(DbId);
        //}

        [Fact]
        public void SameValueAs_should_return_true_when_parameter_is_sameAs_currentId()
        {
            var currentId = IdBuilder.WithId(DbId).Build();
            var parameter = IdBuilder.WithId(DbId).Build();

            var result = currentId.SameValueAs(parameter);

            result.Should().BeTrue();
        }

        [Fact]
        public void SameValueAs_should_return_false_when_parameter_has_different_value_of_currentId()
        {
            var currentId = IdBuilder.WithId(DbId).Build();

            var parameter = IdBuilder.WithId(200).Build();

            var result = currentId.SameValueAs(parameter);

            result.Should().BeFalse();
        }

        [Fact]
        public void HashCode_should_return_hashcode_of_EntityId()
        {
            var breakTemplateId = IdBuilder.WithId(DbId).Build();
            var expectedHashCode = 729;
            var hashcode = breakTemplateId.HashCode();
            hashcode.Should().Be(expectedHashCode);
        }
    }
}
