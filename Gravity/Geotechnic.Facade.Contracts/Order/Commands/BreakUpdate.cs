namespace Geotechnic.Facade.Contracts.Order.Commands
{
    public class BreakUpdate
    {
        public long OrderId { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int Weight { get; set; }
        public int Power { get; set; }
    }
}