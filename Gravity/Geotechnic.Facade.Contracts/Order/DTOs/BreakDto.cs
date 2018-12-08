namespace Geotechnic.Facade.Contracts.Order.DTOs
{
    public class BreakDto
    {
        public long OrderId { get; set; }
        public string BreakDate { get; set; }
        public int Age { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int Weight { get; set; }
        public int Power { get; set; }
    }
}