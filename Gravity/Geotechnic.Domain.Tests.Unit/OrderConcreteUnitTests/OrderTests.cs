using System;
using System.Collections.Generic;
using FluentAssertions;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Domain.OrderConcrete.Exceptions;
using Gravity.Domain;
using Xunit;

namespace Geotechnic.Domain.Tests.Unit.OrderConcreteUnitTests
{
    public class OrderTests : EntityIdTest<OrderId>
    {
        private readonly OrderBuilder _builder;
        private readonly EntityIdBuilder<AdditiveId> _additiveIdBuilder;
        private readonly EntityIdBuilder<BreakTemplateId> _breakTemplateIdBuilder;
        private readonly EntityIdBuilder<ExamplePlaceId> _examplePlaceBuilder;
        private readonly EntityIdBuilder<OrderId> _idBuilder;

        private const long BranchId = 1000;
        private const long Id = 10;

        private const int Volume = 120;
        private const string Axis = "A1-S2";
        private const string ConcreteSeller = "Omran Beton";
        private const int Cutie = 400;
        private const CementTypes CementType = CementTypes.Type2;
        private const int EnvTemp = 20;
        private const int ConcreteTemp = 23;
        private readonly DateTime _exampleDate = DateTime.Now;
        private const long ExampleNumber = 153;
        private const double Slamp = 8.9;
        private const int Fc = 250;
        private const long ProjectId = 553;
        private ExamplePlaceId ExamplePlace => _examplePlaceBuilder.WithId(3).Build();
        private const string ExamplePlaceDesc = "Roof number 5";
        private OrderId OrderId => _idBuilder.WithId(Id).Build();
        private List<AdditiveId> Additives => new List<AdditiveId>() { _additiveIdBuilder.WithId(100).Build(), _additiveIdBuilder.WithId(101).Build() };
        private BreakTemplateId BreakTempId => _breakTemplateIdBuilder.WithId(5).Build();

        public OrderTests()
        {
            _builder = new OrderBuilder();
            _idBuilder = new EntityIdBuilder<OrderId>();
            _additiveIdBuilder = new EntityIdBuilder<AdditiveId>();
            _breakTemplateIdBuilder = new EntityIdBuilder<BreakTemplateId>();
            _examplePlaceBuilder = new EntityIdBuilder<ExamplePlaceId>();
        }

        [Fact]
        public void Constructor_should_create_properly_Order()
        {
            var orderModel = _builder.With(Additives).With(Volume, Axis).With(BreakTempId)
                .WithConcreteSeller(ConcreteSeller).WithCutie(Cutie, CementType)
                .WithTemperature(ConcreteTemp, EnvTemp).WithExampleDate(_exampleDate)
                .WithExampleNumber(ExampleNumber).WithSlamp(Slamp, Fc)
                .WithExamplePlace(ExamplePlace, ExamplePlaceDesc).WithProject(ProjectId).Build();

            var order = new Order(BranchId, OrderId, orderModel);

            order.Axis.Should().Be(Axis);
            order.Additives.Should().BeEquivalentTo(Additives);
            order.BreakTemplateId.Should().BeEquivalentTo(BreakTempId);
            order.CementType.Should().Be(CementType);
            order.ConcreteSeller.Should().Be(ConcreteSeller);
            order.ConcreteTemperature.Should().Be(ConcreteTemp);
            order.Cutie.Should().Be(Cutie);
            order.EnvironmentTemperature.Should().Be(EnvTemp);
            order.ExampleDate.Should().Be(_exampleDate);
            order.ExampleNumber.Should().Be(ExampleNumber);
            order.ExamplePlace.Should().Be(ExamplePlace);
            order.ExamplePlaceDesc.Should().Be(ExamplePlaceDesc);
            order.Fc.Should().Be(Fc);
            order.ProjectId.Should().Be(ProjectId);
            order.Slamp.Should().Be(Slamp);
            order.Volume.Should().Be(Volume);
            order.Id.Should().Be(OrderId);
            order.BranchId.Should().Be(BranchId);
        }

        [Fact]
        public void Update_should_change_the_current_State_of_order()
        {
            var orderModel = _builder.With(Additives).With(Volume, Axis).With(BreakTempId)
                .WithConcreteSeller(ConcreteSeller).WithCutie(Cutie, CementType)
                .WithTemperature(ConcreteTemp, EnvTemp).WithExampleDate(_exampleDate)
                .WithExampleNumber(ExampleNumber).WithSlamp(Slamp, Fc)
                .WithExamplePlace(ExamplePlace, ExamplePlaceDesc).WithProject(ProjectId).Build();

            var order = new Order(BranchId, OrderId, orderModel);

            var volume = 110;
            var axis = "C5-D2";
            var concreteSeller = "Kamaro";
            var cutie = 350;
            var cementType = CementTypes.Type2;
            var envTemp = 22;
            var concreteTemp = 21;
            var exampleDate = DateTime.Now.AddDays(-5);
            var slamp = 9;
            var fc = 210;
            var projectId = 221;
            var examplePlace = _examplePlaceBuilder.WithId(11).Build();
            var examplePlaceDesc = "Wall west 5";
            var additives = new List<AdditiveId>()
                {_additiveIdBuilder.WithId(98).Build(), _additiveIdBuilder.WithId(99).Build()};
            var breakTempId = _breakTemplateIdBuilder.WithId(8).Build();

            var expectedOrderModel = _builder.With(additives).With(volume, axis).With(breakTempId)
                .WithConcreteSeller(concreteSeller).WithCutie(cutie, cementType)
                .WithTemperature(concreteTemp, envTemp).WithExampleDate(exampleDate)
                .WithSlamp(slamp, fc).WithExamplePlace(examplePlace, examplePlaceDesc)
                .WithProject(projectId).Build();

            order.Update(expectedOrderModel);

            order.BreakTemplateId.Should().BeEquivalentTo(breakTempId);
            order.Axis.Should().Be(axis);
            order.Additives.Should().BeEquivalentTo(additives);
            order.CementType.Should().Be(cementType);
            order.ConcreteSeller.Should().Be(concreteSeller);
            order.ConcreteTemperature.Should().Be(concreteTemp);
            order.Cutie.Should().Be(cutie);
            order.EnvironmentTemperature.Should().Be(envTemp);
            order.ExampleDate.Should().Be(exampleDate);
            order.ExamplePlace.Should().Be(examplePlace);
            order.ExamplePlaceDesc.Should().Be(examplePlaceDesc);
            order.Fc.Should().Be(fc);
            order.ProjectId.Should().Be(projectId);
            order.Slamp.Should().Be(slamp);
            order.Volume.Should().Be(volume);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_should_throw_when_ExampleNumber_is_lower_than_One(long exampleNumber)
        {
            var orderModel = _builder.WithExampleNumber(exampleNumber).Build();
            Action order = () => new Order(BranchId, OrderId, orderModel);
            order.Should().Throw<ExampleNumberException>();
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_should_throw_when_ProjectId_is_lower_than_One(long projectId)
        {
            var orderModel = _builder.WithExampleNumber(ExampleNumber).WithProject(projectId).Build();
            Action order = () => new Order(BranchId, OrderId, orderModel);

            order.Should().Throw<ProjectException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Update_should_throw_when_ProjectId_is_lower_than_One(long projectId)
        {
            var orderModel = CreateValidOrderModel();
            var order = new Order(BranchId, OrderId, orderModel);

            var expectedOrderModel = _builder.WithExampleNumber(ExampleNumber)
                .WithProject(projectId).Build();
            Action update = () => order.Update(expectedOrderModel);

            update.Should().Throw<ProjectException>();
        }

        [Fact]
        public void Constructor_should_throw_when_ExampleDate_is_null()
        {
            var orderModel = _builder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(default(DateTime)).Build();
            Action order = () => new Order(BranchId, OrderId, orderModel);

            order.Should().Throw<ExampleDateException>();
        }
        
        [Fact]
        public void Update_should_throw_when_ExampleDate_is_null()
        {
            var orderModel = CreateValidOrderModel();
            var order = new Order(BranchId, OrderId, orderModel);

            var expectedOrderModel = _builder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(default(DateTime)).Build();
            Action update = () => order.Update(expectedOrderModel);

            update.Should().Throw<ExampleDateException>();
        }

        [Fact]
        public void Constructor_should_throw_when_ExamplePlace_is_null()
        {
            var orderModel = _builder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(_exampleDate)
                .WithExamplePlace(null,"").Build();
            Action order = () => new Order(BranchId, OrderId, orderModel);

            order.Should().Throw<ExamplePlaceException>();
        }

        [Fact]
        public void Update_should_throw_when_ExamplePlace_is_null()
        {
            var orderModel = CreateValidOrderModel();
            var order = new Order(BranchId, OrderId, orderModel);

            var expectedOrderModel = _builder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(_exampleDate)
                .WithExamplePlace(null,"").Build();
            Action update = () => order.Update(expectedOrderModel);

            update.Should().Throw<ExamplePlaceException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        public void Constructor_should_throw_when_CementType_is_not_between_1_to_5(int cementType)
        {
            var orderModel = _builder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(_exampleDate)
                .WithExamplePlace(ExamplePlace, "").WithCutie(0,(CementTypes)cementType).Build();
            Action order = () => new Order(BranchId, OrderId, orderModel);

            order.Should().Throw<CementTypeException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        public void Update_should_throw_when_CementType_is_not_between_1_to_5(int cementType)
        {
            var orderModel = CreateValidOrderModel();
            var order = new Order(BranchId, OrderId, orderModel);
            var expectedOrderModel = _builder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(_exampleDate)
                .WithExamplePlace(ExamplePlace, "").WithCutie(0, (CementTypes) cementType).Build();
            Action update = () => order.Update(expectedOrderModel);

            update.Should().Throw<CementTypeException>();
        }

        [Fact]
        public void Constructor_should_throw_when_BreakTemplate_is_null()
        {
            var orderModel = _builder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(_exampleDate)
                .WithExamplePlace(ExamplePlace, "").WithCutie(0,CementTypes.Type2)
                .With(breakTemplateId:null).Build();
            Action order = () => new Order(BranchId, OrderId, orderModel);

            order.Should().Throw<BreakTemplateException>();
        }

        [Fact]
        public void Update_should_throw_when_BreakTemplate_is_null()
        {
            var orderModel = CreateValidOrderModel();
            var order = new Order(BranchId, OrderId, orderModel);

            var expectedOrderModel = _builder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(_exampleDate)
                .WithExamplePlace(ExamplePlace, "").WithCutie(0, CementTypes.Type2)
                .With(breakTemplateId: null).Build();
            Action update = () => order.Update(expectedOrderModel);

            update.Should().Throw<BreakTemplateException>();
        }

        private OrderModel CreateValidOrderModel()
        {
            return _builder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(_exampleDate)
                .WithExamplePlace(ExamplePlace, "").WithCutie(0, CementTypes.Type2)
                .With(BreakTempId).Build();
        }
    }
}
