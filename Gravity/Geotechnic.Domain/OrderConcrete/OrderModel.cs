using System;
using System.Collections.Generic;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.ExamplePlaces;

namespace Geotechnic.Domain.OrderConcrete
{
    public class OrderModel
    {
        public long ProjectId { get; set; }
        public long ExampleNumber { get; set; }
        public DateTime ExampleDate { get; set; }
        public ExamplePlaceId ExamplePlace { get; set; }
        public string ExamplePlaceDesc { get; set; }
        public CementTypes CementType { get; set; }
        public int EnvironmentTemperature { get; set; }
        public int ConcreteTemperature { get; set; }
        public int Cutie { get; set; }
        public double Slamp { get; set; }
        public int Volume { get; set; }
        public string Axis { get; set; }
        public string ConcreteSeller { get; set; }
        public int Fc { get; set; }
        public IList<AdditiveId> Additives { get; set; }
        public BreakTemplateId BreakTemplateId { get; set; }
    }
}