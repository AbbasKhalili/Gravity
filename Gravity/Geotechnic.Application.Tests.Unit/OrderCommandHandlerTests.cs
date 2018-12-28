using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Geotechnic.Application.CommandHandlers;
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
        private readonly EntityIdBuilder<AdditiveId> _additiveIdBuilder;
        private readonly EntityIdBuilder<BreakTemplateId> _breakTemplateIdBuilder;
        private readonly EntityIdBuilder<ExamplePlaceId> _examplePlaceBuilder;
        
        private readonly ISequenceHelper _sequenceHelper;
        private readonly IOrderRepository _repository;
        private readonly OrderCommandHandler _commandHandler;

        public OrderCommandHandlerTests() : base(typeof(OrderMapping).Assembly)
        {
            _idBuilder = new EntityIdBuilder<OrderId>();
            _additiveIdBuilder = new EntityIdBuilder<AdditiveId>();
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
        public void HandleCreate_should_add_BreakTemplate_to_repository()
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
    }
}