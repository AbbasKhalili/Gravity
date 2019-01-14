using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.Order.Commands;
using Gravity.Application;
using Gravity.Tools.DateTools;
using Xunit;

namespace Geotechnic.Facade.Services.Tests.Unit.OrderFacade
{
    public class OrderFacadeServiceTests : InMemoryOrderFacadeService
    {
        private readonly EntityIdBuilder<OrderId> _idBuilder;
        private readonly EntityIdBuilder<BreakTemplateId> _breakTemplateIdBuilder;
        private readonly EntityIdBuilder<ExamplePlaceId> _examplePlaceBuilder;

        public OrderFacadeServiceTests()
        {
            _idBuilder = new EntityIdBuilder<OrderId>();
            _breakTemplateIdBuilder = new EntityIdBuilder<BreakTemplateId>();
            _examplePlaceBuilder = new EntityIdBuilder<ExamplePlaceId>();
        }

        private const long BranchId = 100;
        private const long Id = 1;
        private OrderId OrderId => _idBuilder.WithId(Id).Build();

        private const int Volume = 120;
        private const string Axis = "A1-S2";
        private const string ConcreteSeller = "Omran Beton";
        private const int Cutie = 400;
        private const CementTypes CementType = CementTypes.Type2;
        private const int EnvTemp = 20;
        private const int ConcreteTemp = 23;
        private string _exampleDate = "1397/10/01";
        private DateTime ExampleDate => _exampleDate.ToGregorianDate();
        private const long ExampleNumber = 153;
        private const double Slamp = 8.9;
        private const int Fc = 250;
        private const long ProjectId = 553;

        private long ExamplePlaceid = 1;
        private ExamplePlaceId ExamplePlaceId => _examplePlaceBuilder.WithId(ExamplePlaceid).Build();
        private const string ExamplePlaceDesc = "Roof NO.5";


        private readonly List<int> _additivesId = new List<int>() { 1, 2 };
        private List<AdditiveId> Additives => _additivesId.Select(a => new AdditiveId(a)).ToList();

        private readonly long _breakTemplateid = 1;
        private BreakTemplateId BreakTemplateId => _breakTemplateIdBuilder.WithId(_breakTemplateid).Build();

        [Fact]
        public void Create_should_add_Order_to_repository_by_commandBus()
        {
            DispatchAnOrderCreate();

            var expectedResult = Repository.Get(OrderId);

            expectedResult.Id.Should().BeEquivalentTo(OrderId);
            expectedResult.BranchId.Should().Be(BranchId);
            expectedResult.BreakTemplateId.Should().BeEquivalentTo(BreakTemplateId);
            expectedResult.Additives.Should().BeEquivalentTo(Additives);
            expectedResult.Axis.Should().Be(Axis);
            expectedResult.CementType.Should().Be(CementType);
            expectedResult.ConcreteSeller.Should().Be(ConcreteSeller);
            expectedResult.ConcreteTemperature.Should().Be(ConcreteTemp);
            expectedResult.Cutie.Should().Be(Cutie);
            expectedResult.EnvironmentTemperature.Should().Be(EnvTemp);
            expectedResult.ExampleDate.Should().Be(ExampleDate);
            expectedResult.ExampleNumber.Should().Be(ExampleNumber);
            expectedResult.ExamplePlace.Should().Be(ExamplePlaceId);
            expectedResult.ExamplePlaceDesc.Should().Be(ExamplePlaceDesc);
            expectedResult.Fc.Should().Be(Fc);
            expectedResult.ProjectId.Should().Be(ProjectId);
            expectedResult.Slamp.Should().Be(Slamp);
            expectedResult.Volume.Should().Be(Volume);
        }

        [Fact]
        public void Update_should_modify_Order_in_repository_by_commandBus()
        {
            DispatchAnOrderCreate();

            var axis = "W2-R4";
            var cementType = 4;
            var concreteSeller = "Abtos";
            var concreteTemp = 14;
            var envTemp = 18;
            var cutie = 199;
            var exampleDate = "1397/05/05";
            var exampleDateConverted = exampleDate.ToGregorianDate();
            var examplePlaceId = 3;
            var examplePlaceIdObject = _examplePlaceBuilder.WithId(examplePlaceId).Build();
            var examplePlaceDesc = "Cutter Wall 3";
            var fc = 230;
            var projectId = 147;
            var slamp = 9.9;
            var volume = 130;
            var breakTemplateid = 5;
            var breakTemplateId = _breakTemplateIdBuilder.WithId(breakTemplateid).Build();
            var additivesId = new List<int>() { 5, 8 };
            var additivesIds = additivesId.Select(a => new AdditiveId(a)).ToList();
            var updateCommand = new OrderUpdate
            {
                BranchId = BranchId,
                Id = Id,
                Axis = axis,
                CementType = cementType,
                ConcreteSeller = concreteSeller,
                ConcreteTemperature = concreteTemp,
                Cutie = cutie,
                EnvironmentTemperature = envTemp,
                ExampleDate = exampleDate,
                ExampleNumber = ExampleNumber,
                ExamplePlaceId = examplePlaceId,
                ExamplePlaceDesc = examplePlaceDesc,
                Fc = fc,
                ProjectId = projectId,
                Slamp = slamp,
                Volume = volume,
                BreakTemplateId = breakTemplateid,
                AdditivesId = additivesId,
            };
            FacadeService.Modify(updateCommand);

            var expectedResult = Repository.Get(OrderId);

            expectedResult.Id.Should().BeEquivalentTo(OrderId);
            expectedResult.BranchId.Should().Be(BranchId);
            expectedResult.BreakTemplateId.Should().BeEquivalentTo(breakTemplateId);
            expectedResult.Additives.Should().BeEquivalentTo(additivesIds);
            expectedResult.Axis.Should().Be(axis);
            expectedResult.CementType.Should().Be(cementType);
            expectedResult.ConcreteSeller.Should().Be(concreteSeller);
            expectedResult.ConcreteTemperature.Should().Be(concreteTemp);
            expectedResult.Cutie.Should().Be(cutie);
            expectedResult.EnvironmentTemperature.Should().Be(envTemp);
            expectedResult.ExampleDate.Should().Be(exampleDateConverted);
            expectedResult.ExampleNumber.Should().Be(ExampleNumber);
            expectedResult.ExamplePlace.Should().Be(examplePlaceIdObject);
            expectedResult.ExamplePlaceDesc.Should().Be(examplePlaceDesc);
            expectedResult.Fc.Should().Be(fc);
            expectedResult.ProjectId.Should().Be(projectId);
            expectedResult.Slamp.Should().Be(slamp);
            expectedResult.Volume.Should().Be(volume);
        }

        [Fact]
        public void Delete_should_remove_Order_from_repository_by_commandBus()
        {
            DispatchAnOrderCreate();

            var deleteCommand = new OrderDelete()
            { BranchId = BranchId, Id = Id };
            FacadeService.Delete(deleteCommand);

            var expectedResult = Repository.Get(OrderId);

            expectedResult.Should().BeNull();
        }

        private void DispatchAnOrderCreate()
        {
            var command = new OrderCreate()
            {
                BranchId = BranchId,
                Axis = Axis,
                CementType = (int)CementType,
                ConcreteSeller = ConcreteSeller,
                ConcreteTemperature = ConcreteTemp,
                Cutie = Cutie,
                EnvironmentTemperature = EnvTemp,
                ExampleDate = _exampleDate,
                ExampleNumber = ExampleNumber,
                ExamplePlaceId = ExamplePlaceid,
                ExamplePlaceDesc = ExamplePlaceDesc,
                Fc = Fc,
                ProjectId = ProjectId,
                Slamp = Slamp,
                Volume = Volume,
                BreakTemplateId = _breakTemplateid,
                AdditivesId = _additivesId,
            };
            FacadeService.Create(command);
        }
    }
}
