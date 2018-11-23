using System;
using System.Collections.Generic;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;

namespace Geotechnic.Domain.Tests.Unit.OrderConcreteUnitTests
{
    internal class OrderBuilder
    {
        public long ProjectId { get; private set; }
        public long ExampleNumber { get; private set; }
        public DateTime ExampleDate { get; private set; }
        public ExamplePlaceId ExamplePlace { get; private set; }
        public string ExamplePlaceDesc { get; private set; }
        public CementTypes CementType { get; private set; }
        public int EnvironmentTemperature { get; private set; }
        public int ConcreteTemperature { get; private set; }
        public int Cutie { get; private set; }
        public double Slamp { get; private set; }
        public int Volume { get; private set; }
        public string Axis { get; private set; }
        public string ConcreteSeller { get; private set; }
        public int Fc { get; private set; }
        public IList<AdditiveId> Additives { get; private set; }
        public BreakTemplateId BreakTemplateId { get; private set; }
        

        public OrderModel Build()
        {
            return new OrderModel()
            {
                Additives = Additives,Axis = Axis,BreakTemplateId =BreakTemplateId,
                CementType=CementType,ConcreteSeller=ConcreteSeller,Cutie=Cutie,
                ConcreteTemperature=ConcreteTemperature,EnvironmentTemperature=EnvironmentTemperature,
                ExampleDate=ExampleDate,ExampleNumber=ExampleNumber,ExamplePlace=ExamplePlace,
                ExamplePlaceDesc=ExamplePlaceDesc,Fc=Fc,ProjectId=ProjectId,Slamp=Slamp,Volume=Volume
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
        public OrderBuilder WithTemperature(int concreteTemperature,int environmentTemperature)
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