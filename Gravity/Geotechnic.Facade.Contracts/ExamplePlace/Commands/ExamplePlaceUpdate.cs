namespace Geotechnic.Facade.Contracts.ExamplePlace.Commands
{
    public class ExamplePlaceUpdate 
    {
        public long BranchId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Character { get; set; }
        //public string Username { get; set; }
    }
}