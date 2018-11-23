using Gravity.Domain;

namespace Geotechnic.Domain.ExamplePlaces
{
    public class ExamplePlace : EntityBase<ExamplePlaceId>
    {
        public string Title { get; private set; }
        public string Character { get; private set; }


        protected ExamplePlace() { }
        public ExamplePlace(long branchId, ExamplePlaceId id, string character, string title)
        {
            BranchId = branchId;
            Id = id;
            SetProperties(character, title);
        }

        public void Update(string character, string title)
        {
            SetProperties(character, title);
        }

        private void SetProperties(string character, string title)
        {
            Character = character;
            Title = title;
        }
    }
}