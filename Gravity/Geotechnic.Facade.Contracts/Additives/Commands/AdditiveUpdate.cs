namespace Geotechnic.Facade.Contracts.Additives.Commands
{
    public class AdditiveUpdate 
    {
        public long BranchId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
    }
}