namespace Geotechnic.Facade.Contracts.Order.Commands
{
    public class OrderDelete 
    {
        public long BranchId { get; set; }
        public long Id { get; set; }
    }
}