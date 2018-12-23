using System;
using FluentAssertions;
using Geotechnic.Application.CommandHandlers;
using Geotechnic.Application.Exceptions;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.Additives.Commands;
using Geotechnic.Persistence.Mappings;
using Geotechnic.Persistence.Repositories;
using Gravitest.NHibernate;
using Gravity.Application;
using Gravity.NHibernate;
using Xunit;

namespace Geotechnic.Application.Tests.Unit
{
    public class AdditiveCommandHandlerTests : InMemoryDatabase
    {
        private readonly EntityIdBuilder<AdditiveId> _entityIdBuilder;
        private readonly ISequenceHelper _sequenceHelper;
        private readonly IAdditiveRepository _additiveRepository;
        private readonly AdditiveCommandHandler _commandHandler;

        public AdditiveCommandHandlerTests() : base(typeof(AdditiveMapping).Assembly)
        {
            _entityIdBuilder = new EntityIdBuilder<AdditiveId>();
            _sequenceHelper = new FakeSequenceHelper();
            _additiveRepository = new AdditiveRepository(Session, _sequenceHelper);
            IOrderRepository orderRepository = new OrderRepository(Session, _sequenceHelper);
            _commandHandler = new AdditiveCommandHandler(_additiveRepository, orderRepository);
        }

        private const long BranchId = 100;
        private const long Id = 1;
        private const string Title = "kolomter AD400";

        
        [Fact]
        public void HandleCreate_should_add_Additive_to_repository()
        {
            InsertAdditive();

            var longId = _sequenceHelper.Next("");
            var iid = _entityIdBuilder.WithId(longId).Build();
            var expectedAdditive = _additiveRepository.Get(iid);
            expectedAdditive.Id.Should().BeEquivalentTo(iid);
            expectedAdditive.BranchId.Should().Be(BranchId);
            expectedAdditive.Title.Should().Be(Title);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void HandleCreate_should_throw_when_BranchId_is_not_valid(long branchId)
        {
            var createCommand = new AdditiveCreate { BranchId = branchId, Title = Title };
            Action expected = () => _commandHandler.Handle(createCommand);
            expected.Should().Throw<BranchNotFoundException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void HandleCreate_should_throw_when_Title_is_not_valid(string title)
        {
            var createCommand = new AdditiveCreate { BranchId = BranchId, Title = title };
            Action expected = () => _commandHandler.Handle(createCommand);
            expected.Should().Throw<AdditiveTitleRequiredException>();
        }

        private void InsertAdditive()
        {
            var createCommand = new AdditiveCreate { BranchId = BranchId, Title = Title };
            _commandHandler.Handle(createCommand);
        }
    }
}