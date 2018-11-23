using Geotechnic.Domain.ExamplePlaces;

namespace Geotechnic.Domain.Tests.Unit.ExamplePlaceUnitTests
{
    internal class ExamplePlaceBuilder
    {
        public long BranchId { get; private set; }
        public ExamplePlaceId Id { get; private set; }
        public string Title { get; private set; }
        public string Character { get; private set; }

        public ExamplePlaceBuilder()
        {
            BranchId = 1000;
            Id = new ExamplePlaceId(100);
        }

        public ExamplePlace Build()
        {
            return new ExamplePlace(BranchId,Id, Character, Title);
        }

        public ExamplePlaceBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public ExamplePlaceBuilder WithCharacter(string character)
        {
            Character = character;
            return this;
        }
    }
}