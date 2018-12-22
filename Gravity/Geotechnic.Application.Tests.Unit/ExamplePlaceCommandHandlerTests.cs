using System;
using FluentAssertions;
using Geotechnic.Application.CommandHandlers;
using Geotechnic.Application.Exceptions;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Geotechnic.Persistence.Mappings;
using Geotechnic.Persistence.Repositories;
using Gravitest.NHibernate;
using Gravity.Application;
using Gravity.NHibernate;
using Xunit;

namespace Geotechnic.Application.Tests.Unit
{
    public class ExamplePlaceCommandHandlerTests : InMemoryDatabase
    {
        private readonly EntityIdBuilder<ExamplePlaceId> _examplePlaceIdBuilder;
        private readonly ISequenceHelper _sequenceHelper;
        private readonly IExamplePlaceRepository _placeRepository;
        private readonly ExamplePlaceCommandHandler _commandHandler;

        public ExamplePlaceCommandHandlerTests(): base(typeof(ExamplePlaceMapping).Assembly)
        {
            _examplePlaceIdBuilder = new EntityIdBuilder<ExamplePlaceId>();
            _sequenceHelper = new FakeSequenceHelper();
            _placeRepository = new ExamplePlaceRepository(_sequenceHelper, Session);
            IOrderRepository orderRepository = new OrderRepository(Session, _sequenceHelper);
            _commandHandler = new ExamplePlaceCommandHandler(_placeRepository, orderRepository);
        }


        private const long BranchId = 100;
        private const long Id = 1;
        private const string Character = "C";
        private const string Title = "Column";

        
        [Fact]
        public void HandleCreate_should_add_ExamplePlace_to_repository()
        {
            InsertExamplePlace();

            var longId = _sequenceHelper.Next(""); 
            var iid = _examplePlaceIdBuilder.WithId(longId).Build();
            var expectedExamplePlace = _placeRepository.Get(iid);
            expectedExamplePlace.Id.Should().BeEquivalentTo(iid);
            expectedExamplePlace.BranchId.Should().Be(BranchId);
            expectedExamplePlace.Title.Should().Be(Title);
            expectedExamplePlace.Character.Should().Be(Character);
        }

        [Fact]
        public void HandleUpdate_should_modify_ExamplePlace_in_repository()
        {
            InsertExamplePlace();

            var character = "W";
            var title = "Wall";
            var command = new ExamplePlaceUpdate { BranchId = BranchId, Id = Id, Character = character, Title = title };
            _commandHandler.Handle(command);

            var longId = _sequenceHelper.Next("");
            var iid = _examplePlaceIdBuilder.WithId(longId).Build();
            var expectedExamplePlace = _placeRepository.Get(iid);
            expectedExamplePlace.Id.Should().BeEquivalentTo(iid);
            expectedExamplePlace.BranchId.Should().Be(BranchId);
            expectedExamplePlace.Title.Should().Be(title);
            expectedExamplePlace.Character.Should().Be(character);
        }

        [Fact]
        public void HandleDelete_should_remove_ExamplePlace_from_repository()
        {
            InsertExamplePlace();
            
            var command = new ExamplePlaceDelete { BranchId = BranchId, Id = Id };
            _commandHandler.Handle(command);

            var id = _examplePlaceIdBuilder.WithId(Id).Build();
            var expectedExamplePlace = _placeRepository.Get(id);
            expectedExamplePlace.Should().BeNull();
        }

        private void InsertExamplePlace()
        {
            var createCommand = new ExamplePlaceCreate { BranchId = BranchId, Character = Character, Title = Title };
            _commandHandler.Handle(createCommand);
        }

        [Fact]
        public void HandleDelete_should_throw_when_ExamplePlace_not_found()
        {
            var command = new ExamplePlaceDelete { BranchId = BranchId, Id = Id };
            Action expectedException = () => _commandHandler.Handle(command);

            expectedException.Should().Throw<ExamplePlaceNotFoundException>();
        }
    }
}
