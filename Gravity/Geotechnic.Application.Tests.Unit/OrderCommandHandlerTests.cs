using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Geotechnic.Application.CommandHandlers;
using Geotechnic.Application.Exceptions;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.Order.Commands;
using Geotechnic.Persistence.Mappings;
using Geotechnic.Persistence.Repositories;
using Gravitest.NHibernate;
using Gravity.Application;
using Gravity.NHibernate;
using Gravity.Tools.DateTools;
using Xunit;

namespace Geotechnic.Application.Tests.Unit
{
    public class OrderCommandHandlerTests : InMemoryDatabase
    {
        private readonly EntityIdBuilder<OrderId> _idBuilder;
        private readonly EntityIdBuilder<BreakTemplateId> _breakTemplateIdBuilder;
        private readonly EntityIdBuilder<ExamplePlaceId> _examplePlaceBuilder;
        
        private readonly ISequenceHelper _sequenceHelper;
        private readonly IOrderRepository _repository;
        private readonly OrderCommandHandler _commandHandler;

        public OrderCommandHandlerTests() : base(typeof(OrderMapping).Assembly)
        {
            _idBuilder = new EntityIdBuilder<OrderId>();
            _breakTemplateIdBuilder = new EntityIdBuilder<BreakTemplateId>();
            _examplePlaceBuilder = new EntityIdBuilder<ExamplePlaceId>();

            _sequenceHelper = new FakeSequenceHelper();
            _repository = new OrderRepository(Session, _sequenceHelper);
            _commandHandler = new OrderCommandHandler(_repository);
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
        private string _exampleDate ="1397/10/01";
        private DateTime ExampleDate => _exampleDate.ToGregorianDate();
        private const long ExampleNumber = 153;
        private const double Slamp = 8.9;
        private const int Fc = 250;
        private const long ProjectId = 553;

        private long ExamplePlaceid = 1;
        private ExamplePlaceId ExamplePlaceId => _examplePlaceBuilder.WithId(ExamplePlaceid).Build();
        private const string ExamplePlaceDesc = "Roof NO.5";
        

        private readonly List<int> _additivesId = new List<int>() {1, 2};
        private List<AdditiveId> Additives => _additivesId.Select(a => new AdditiveId(a)).ToList();

        private readonly long _breakTemplateid = 1;
        private BreakTemplateId BreakTemplateId => _breakTemplateIdBuilder.WithId(_breakTemplateid).Build();


        [Fact]
        public void HandleCreate_should_add_Order_to_repository()
        {
            InsertOrder();

            var longId = _sequenceHelper.Next("");
            var iid = _idBuilder.WithId(longId).Build();
            
            var expectedOrder = _repository.Get(iid);

            expectedOrder.Id.Should().BeEquivalentTo(iid);
            expectedOrder.BranchId.Should().Be(BranchId);
            expectedOrder.BreakTemplateId.Should().BeEquivalentTo(BreakTemplateId);
            expectedOrder.Additives.Should().BeEquivalentTo(Additives);
            expectedOrder.Axis.Should().Be(Axis);
            expectedOrder.CementType.Should().Be(CementType);
            expectedOrder.ConcreteSeller.Should().Be(ConcreteSeller);
            expectedOrder.ConcreteTemperature.Should().Be(ConcreteTemp);
            expectedOrder.Cutie.Should().Be(Cutie);
            expectedOrder.EnvironmentTemperature.Should().Be(EnvTemp);
            expectedOrder.ExampleDate.Should().Be(ExampleDate);
            expectedOrder.ExampleNumber.Should().Be(ExampleNumber);
            expectedOrder.ExamplePlace.Should().Be(ExamplePlaceId);
            expectedOrder.ExamplePlaceDesc.Should().Be(ExamplePlaceDesc);
            expectedOrder.Fc.Should().Be(Fc);
            expectedOrder.ProjectId.Should().Be(ProjectId);
            expectedOrder.Slamp.Should().Be(Slamp);
            expectedOrder.Volume.Should().Be(Volume);
        }

        [Fact]
        public void HandleUpdate_should_modify_Order_in_repository()
        {
            InsertOrder();

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
            var additivesId = new List<int>(){5,8};
            var additivesIds = additivesId.Select(a => new AdditiveId(a)).ToList();
            var command = new OrderUpdate { BranchId = BranchId, Id = Id,
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
            _commandHandler.Handle(command);

            var longId = _sequenceHelper.Next("");
            var iid = _idBuilder.WithId(longId).Build();
            
            var expectedOrder = _repository.Get(iid);
            expectedOrder.Id.Should().BeEquivalentTo(iid);
            expectedOrder.BranchId.Should().Be(BranchId);
            expectedOrder.BreakTemplateId.Should().BeEquivalentTo(breakTemplateId);
            expectedOrder.Additives.Should().BeEquivalentTo(additivesIds);
            expectedOrder.Axis.Should().Be(axis);
            expectedOrder.CementType.Should().Be(cementType);
            expectedOrder.ConcreteSeller.Should().Be(concreteSeller);
            expectedOrder.ConcreteTemperature.Should().Be(concreteTemp);
            expectedOrder.Cutie.Should().Be(cutie);
            expectedOrder.EnvironmentTemperature.Should().Be(envTemp);
            expectedOrder.ExampleDate.Should().Be(exampleDateConverted);
            expectedOrder.ExampleNumber.Should().Be(ExampleNumber);
            expectedOrder.ExamplePlace.Should().Be(examplePlaceIdObject);
            expectedOrder.ExamplePlaceDesc.Should().Be(examplePlaceDesc);
            expectedOrder.Fc.Should().Be(fc);
            expectedOrder.ProjectId.Should().Be(projectId);
            expectedOrder.Slamp.Should().Be(slamp);
            expectedOrder.Volume.Should().Be(volume);
        }

        [Fact]
        public void HandleDelete_should_remove_Order_from_repository()
        {
            InsertOrder();

            var command = new OrderDelete { BranchId = BranchId, Id = Id };
            _commandHandler.Handle(command);

            var id = _idBuilder.WithId(Id).Build();
            var expectedBreakTemplate = _repository.Get(id);
            expectedBreakTemplate.Should().BeNull();
        }

        private void InsertOrder()
        {
            var createCommand = new OrderCreate
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
            _commandHandler.Handle(createCommand);
        }

        [Fact]
        public void HandleDelete_should_throw_when_Order_not_found()
        {
            var command = new OrderDelete { BranchId = BranchId, Id = Id };
            Action expectedException = () => _commandHandler.Handle(command);

            expectedException.Should().Throw<OrderNotFoundException>();
        }
    }
}