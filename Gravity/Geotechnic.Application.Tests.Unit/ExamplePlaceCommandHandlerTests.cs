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
using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Gravity.Application;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace Geotechnic.Application.Tests.Unit
{
    public class ExamplePlaceCommandHandlerTests
    {
        private readonly EntityIdBuilder<ExamplePlaceId> _examplePlaceBuilder;
        private readonly EntityIdBuilder<OrderId> _orderIdBuilder;
        private readonly EntityIdBuilder<BreakTemplateId> _breakTemplateIdBuilder;
        private readonly OrderBuilder _orderBuilder;

        public ExamplePlaceCommandHandlerTests()
        {
            _examplePlaceBuilder = new EntityIdBuilder<ExamplePlaceId>();
            _orderIdBuilder = new EntityIdBuilder<OrderId>();
            _orderBuilder = new OrderBuilder();
            _breakTemplateIdBuilder = new EntityIdBuilder<BreakTemplateId>();
        }


        private const long BranchId = 100;
        private const long Id = 1;
        private const string Character = "C";
        private const string Title = "Column";



        private readonly DateTime _exampleDate = DateTime.Now;
        private const long ExampleNumber = 153;
        private const long ProjectId = 553;
        private ExamplePlaceId ExamplePlace => _examplePlaceBuilder.WithId(1).Build();
        private BreakTemplateId BreakTempId => _breakTemplateIdBuilder.WithId(5).Build();



        [Fact]
        public void HandleCreate_should_add_ExamplePlace_to_repository()
        {
            var command = new ExamplePlaceCreate { BranchId = BranchId, Character = Character, Title=Title};
            var id = _examplePlaceBuilder.WithId(Id).Build();
            var expectedExamplePlace = new ExamplePlace(BranchId, id, Character, Title);
            var repository = Substitute.For<IExamplePlaceRepository>();
            var commandHandler = new ExamplePlaceCommandHandler(repository,null);

            commandHandler.Handle(command);

            repository.Received(1)
                .Create(Verify.That<ExamplePlace>(
                    a => a.Should().BeEquivalentTo(expectedExamplePlace,
                        z => z.Excluding(b => b.Id).ComparingByMembers<ExamplePlace>())));
        }

        [Fact]
        public void HandleUpdate_should_modify_ExamplePlace_in_repository()
        {
            var id = _examplePlaceBuilder.WithId(Id).Build();
            var returnValue = new ExamplePlace(BranchId, id, Character, Title);
            var repository = Substitute.For<IExamplePlaceRepository>();
            repository.GetByIdAndBranchId(id, BranchId).Returns(returnValue);
            
            var character = "W";
            var title = "Wall";
            var command = new ExamplePlaceUpdate { BranchId = BranchId, Id = Id, Character = character, Title = title };
            var commandHandler = new ExamplePlaceCommandHandler(repository,null);
            commandHandler.Handle(command);

            returnValue.Character.Should().Be(character);
            returnValue.Title.Should().Be(title);
        }

        [Fact]
        public void HandleDelete_should_remove_ExamplePlace_from_repository()
        {
            var command = new ExamplePlaceDelete { BranchId = BranchId, Id = Id };
            var id = _examplePlaceBuilder.WithId(Id).Build();
            var expectedExamplePlace = new ExamplePlace(BranchId, id, Character, Title);
            
            var returnValue = new ExamplePlace(BranchId, id, Character, Title);
            var repository = Substitute.For<IExamplePlaceRepository>();
            repository.GetByIdAndBranchId(id, BranchId).Returns(returnValue);
            
            var orderRepository = Substitute.For<IOrderRepository>();

            var commandHandler = new ExamplePlaceCommandHandler(repository, orderRepository);
            commandHandler.Handle(command);

            repository.Received(1)
                .Delete(Verify.That<ExamplePlace>(
                    a => a.Should().BeEquivalentTo(expectedExamplePlace)));
        }

        [Fact]
        public void HandleDelete_should_throw_when_ExamplePlace_is_used_in_order()
        {
            var command = new ExamplePlaceDelete { BranchId = BranchId, Id = Id };

            var id = _orderIdBuilder.WithId(Id).Build();
            var returnValue = new List<Order>()
            {
                new Order(BranchId, id, CreateValidOrderModel())
            }.AsQueryable();
                              

            var orderQueryable = Substitute.For<IQueryable<Order>>();
            //orderQueryable.Provider.Returns(returnValue.Provider);
            //orderQueryable.Expression.Returns(returnValue.Expression);
            //orderQueryable.ElementType.Returns(returnValue.ElementType);
            orderQueryable.GetEnumerator().Returns(returnValue.GetEnumerator());

            var orderRepository = Substitute.For<IOrderRepository>();
            orderRepository.GetAll().Returns(returnValue);

            var repository = Substitute.For<IExamplePlaceRepository>();

            var commandHandler = new ExamplePlaceCommandHandler(repository, orderRepository);
            Action expected = () => commandHandler.Handle(command);

            expected.Should().Throw<ExamplePlaceUsedException>();
        }

        private OrderModel CreateValidOrderModel()
        {
            return _orderBuilder.WithExampleNumber(ExampleNumber)
                .WithProject(ProjectId).WithExampleDate(_exampleDate)
                .WithExamplePlace(ExamplePlace, "").WithCutie(0, CementTypes.Type2)
                .With(BreakTempId).Build();
        }
    }
}
