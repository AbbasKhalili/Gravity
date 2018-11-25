using System;
using System.Collections.Generic;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete.Exceptions;
using Gravity.Domain;
using Gravity.Tools;

namespace Geotechnic.Domain.OrderConcrete
{
    public class Order : EntityBase<OrderId>
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

        public Order(long branchId, OrderId id, OrderModel order)
        {
            Guard<ExampleNumberException>.SmallerThan(order.ExampleNumber, 1);

            BranchId = branchId;
            Id = id;
            ExampleNumber = order.ExampleNumber;
            SetProperties(order);
        }
        
        public void Update(OrderModel order)
        {
            SetProperties(order);
        }

        private void SetProperties(OrderModel order)
        {
            Guard<ProjectException>.SmallerThan(order.ProjectId, 1);
            Guard<ExampleDateException>.AgainstNull(order.ExampleDate);
            Guard<ExamplePlaceException>.AgainstNull(order.ExamplePlace);
            Guard<CementTypeException>.Between((int)order.CementType, 1, 5);
            Guard<BreakTemplateException>.AgainstNull(order.BreakTemplateId);

            Additives = order.Additives;
            Axis = order.Axis;
            BreakTemplateId = order.BreakTemplateId;
            CementType = order.CementType;
            ConcreteSeller = order.ConcreteSeller;
            ConcreteTemperature = order.ConcreteTemperature;
            Cutie = order.Cutie;
            ProjectId = order.ProjectId;
            ExampleDate = order.ExampleDate;
            ExamplePlace = order.ExamplePlace;
            ExamplePlaceDesc = order.ExamplePlaceDesc;
            Volume = order.Volume;
            Fc = order.Fc;
            Slamp = order.Slamp;
            EnvironmentTemperature = order.EnvironmentTemperature;
        }
    }
}
