using Geotechnic.Domain.ExamplePlaces;

namespace Geotechnic.Domain.Tests.Unit.ExamplePlaceUnitTests
{
    internal class ExamplePlaceBuilder
    {
        private long BranchId { get;  set; }
        private ExamplePlaceId Id { get;  set; }
        private string Title { get;  set; }
        private string Character { get;  set; }

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