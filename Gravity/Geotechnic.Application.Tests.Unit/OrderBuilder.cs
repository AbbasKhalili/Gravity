using System;
using System.Collections.Generic;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;

namespace Geotechnic.Application.Tests.Unit
{
    internal class OrderBuilder
    {
        private long ProjectId { get; set; }
        private long ExampleNumber { get; set; }
        private DateTime ExampleDate { get; set; }
        private ExamplePlaceId ExamplePlace { get; set; }
        private string ExamplePlaceDesc { get; set; }
        private CementTypes CementType { get; set; }
        private int EnvironmentTemperature { get; set; }
        private int ConcreteTemperature { get; set; }
        private int Cutie { get; set; }
        private double Slamp { get; set; }
        private int Volume { get; set; }
        private string Axis { get; set; }
        private string ConcreteSeller { get; set; }
        private int Fc { get; set; }
        private IList<AdditiveId> Additives { get; set; }
        private BreakTemplateId BreakTemplateId { get; set; }


        public OrderModel Build()
        {
            return new OrderModel()
            {
                Additives = Additives,
                Axis = Axis,
                BreakTemplateId = BreakTemplateId,
                CementType = CementType,
                ConcreteSeller = ConcreteSeller,
                Cutie = Cutie,
                ConcreteTemperature = ConcreteTemperature,
                EnvironmentTemperature = EnvironmentTemperature,
                ExampleDate = ExampleDate,
                ExampleNumber = ExampleNumber,
                ExamplePlace = ExamplePlace,
                ExamplePlaceDesc = ExamplePlaceDesc,
                Fc = Fc,
                ProjectId = ProjectId,
                Slamp = Slamp,
                Volume = Volume
            };
        }
        public OrderBuilder WithSlamp(double slamp, int fc)
        {
            Slamp = slamp;
            Fc = fc;
            return this;
        }
        public OrderBuilder WithExampleDate(DateTime exampleDate)
        {
            ExampleDate = exampleDate;
            return this;
        }
        public OrderBuilder WithExamplePlace(ExamplePlaceId examplePlaceId, string examplePlaceDesc)
        {
            ExamplePlaceDesc = examplePlaceDesc;
            ExamplePlace = examplePlaceId;
            return this;
        }
        public OrderBuilder WithExampleNumber(long number)
        {
            ExampleNumber = number;
            return this;
        }
        public OrderBuilder WithProject(long id)
        {
            ProjectId = id;
            return this;
        }
        public OrderBuilder WithTemperature(int concreteTemperature, int environmentTemperature)
        {
            EnvironmentTemperature = environmentTemperature;
            ConcreteTemperature = concreteTemperature;
            return this;
        }
        public OrderBuilder WithCutie(int cutie, CementTypes cementType)
        {
            Cutie = cutie;
            CementType = cementType;
            return this;
        }
        public OrderBuilder WithConcreteSeller(string concreteSeller)
        {
            ConcreteSeller = concreteSeller;
            return this;
        }
        public OrderBuilder With(IList<AdditiveId> additiveIds)
        {
            Additives = additiveIds;
            return this;
        }

        public OrderBuilder With(int volume, string axis)
        {
            Volume = volume;
            Axis = axis;
            return this;
        }
        public OrderBuilder With(BreakTemplateId breakTemplateId)
        {
            BreakTemplateId = breakTemplateId;
            return this;
        }
    }
}