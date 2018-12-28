using System.Collections.Generic;

namespace Geotechnic.Facade.Contracts.Order.Commands
{
    public class OrderCreate
    {
        public long BranchId { get; set; }
        public long ProjectId { get; set; }
        public long ExampleNumber { get; set; }
        public string ExampleDate { get; set; }
        public long ExamplePlaceId { get; set; }
        public int CementType { get; set; }
        public int EnvironmentTemperature { get; set; }
        public int ConcreteTemperature { get; set; }
        public int Cutie { get; set; }
        public double Slamp { get; set; }
        public int Volume { get; set; }
        public string Axis { get; set; }
        public string ConcreteSeller { get; set; }
        public int Fc { get; set; }
        public IList<int> AdditivesId { get; set; }
        public long BreakTemplateId { get; set; }
        public string ExamplePlaceDesc { get; set; }
    }
}
