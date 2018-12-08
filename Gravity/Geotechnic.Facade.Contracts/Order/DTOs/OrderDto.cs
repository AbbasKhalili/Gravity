using System.Collections.Generic;
using Geotechnic.Facade.Contracts.Additives.DTOs;
using Geotechnic.Facade.Contracts.BreakTemplate.DTOs;
using Geotechnic.Facade.Contracts.ExamplePlace.DTOs;

namespace Geotechnic.Facade.Contracts.Order.DTOs
{
    public class OrderDto
    {
        public long BranchId { get; set; }
        public string BranchName { get; set; }
        public long Id { get; set; }
        //public ProjectDto Project { get; set; }
        public long ExampleNumber { get; set; }
        public string ExampleDate { get; set; }
        public ExamplePlaceDto ExamplePlace { get; set; }
        public string ExamplePlaceDesc { get; set; }
        public int CementTypes { get; set; }
        public int EnvironmentTemperature { get; set; }
        public int ConcreteTemperature { get; set; }
        public int Cutie { get; set; }
        public float Slamp { get; set; }
        public int Volume { get; set; }
        public string Axis { get; set; }
        public string ConcreteSeller { get; set; }
        public int Fc { get; set; }
        public IList<AdditiveDto> AdditivesId { get; set; }
        public BreakTemplateDto BreakTemplate { get; set; }
        public IList<BreakDto> BreaksList { get; set; }
    }
}
