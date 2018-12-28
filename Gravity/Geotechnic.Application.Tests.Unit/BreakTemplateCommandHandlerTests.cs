using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Geotechnic.Application.CommandHandlers;
using Geotechnic.Application.Exceptions;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.BreakTemplate.Commands;
using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Geotechnic.Persistence.Mappings;
using Geotechnic.Persistence.Repositories;
using Gravitest.NHibernate;
using Gravity.Application;
using Gravity.NHibernate;
using Xunit;

namespace Geotechnic.Application.Tests.Unit
{
    public class BreakTemplateCommandHandlerTests : InMemoryDatabase
    {
        private readonly EntityIdBuilder<BreakTemplateId> _idBuilder;
        private readonly ISequenceHelper _sequenceHelper;
        private readonly IBreakTemplateRepository _repository;
        private readonly BreakTemplateCommandHandler _commandHandler;

        public BreakTemplateCommandHandlerTests() : base(typeof(BreakTemplateMapping).Assembly)
        {
            _idBuilder = new EntityIdBuilder<BreakTemplateId>();
            _sequenceHelper = new FakeSequenceHelper();
            _repository = new BreakTemplateRepository(Session, _sequenceHelper);
            IOrderRepository orderRepository = new OrderRepository(Session, _sequenceHelper);
            _commandHandler = new BreakTemplateCommandHandler(_repository, orderRepository);
        }
        
        private const long BranchId = 100;
        private const long Id = 1;
        private const int MoldCount = 5;
        private const string Title = "7-28-90";
        private readonly List<Molds> _moldList = new List<Molds>()
        {
            new Molds() {Age = 7, Count = 2}, new Molds() {Age = 28, Count = 2}, new Molds() {Age = 90, Count = 1}
        };

        [Fact]
        public void HandleCreate_should_add_BreakTemplate_to_repository()
        {
            InsertBreakTemplate();

            var longId = _sequenceHelper.Next("");
            var iid = _idBuilder.WithId(longId).Build();
            var moldList = _moldList.Select(a => new BreakTemplateMolds
                {Age = a.Age, Count = a.Count, BreakTemplateId = iid}).ToList();
            var expectedBreakTemplate = _repository.Get(iid);


            expectedBreakTemplate.Id.Should().BeEquivalentTo(iid);
            expectedBreakTemplate.BranchId.Should().Be(BranchId);
            expectedBreakTemplate.Title.Should().Be(Title);
            expectedBreakTemplate.MoldCount.Should().Be(MoldCount);
            expectedBreakTemplate.BreakTemplateMolds.Should().BeEquivalentTo(moldList);
        }
        [Fact]
        public void HandleUpdate_should_modify_BreakTemplate_in_repository()
        {
            InsertBreakTemplate();

            var moldCount = 4;
            var title = "11-42-90";
            var moldList = new List<Molds>()
            {
                new Molds() {Age = 11, Count = 1}, new Molds() {Age = 42, Count = 2}, new Molds() {Age = 90, Count = 1}
            };
            var command = new BreakTemplateUpdate { BranchId = BranchId, Id = Id, Title = title,MoldCount= moldCount, MoldList= moldList };
            _commandHandler.Handle(command);

            var longId = _sequenceHelper.Next("");
            var iid = _idBuilder.WithId(longId).Build();
            var expectedMoldList = moldList.Select(a => new BreakTemplateMolds
                { Age = a.Age, Count = a.Count, BreakTemplateId = iid }).ToList();

            var expectedBreakTemplate = _repository.Get(iid);
            expectedBreakTemplate.Id.Should().BeEquivalentTo(iid);
            expectedBreakTemplate.BranchId.Should().Be(BranchId);
            expectedBreakTemplate.Title.Should().Be(title);
            expectedBreakTemplate.MoldCount.Should().Be(moldCount);
            expectedBreakTemplate.BreakTemplateMolds.Should().BeEquivalentTo(expectedMoldList);
        }

        [Fact]
        public void HandleDelete_should_remove_BreakTemplate_from_repository()
        {
            InsertBreakTemplate();

            var command = new BreakTemplateDelete { BranchId = BranchId, Id = Id };
            _commandHandler.Handle(command);

            var id = _idBuilder.WithId(Id).Build();
            var expectedBreakTemplate = _repository.Get(id);
            expectedBreakTemplate.Should().BeNull();
        }

        private void InsertBreakTemplate()
        {
            var createCommand = new BreakTemplateCreate
                {BranchId = BranchId, Title = Title, MoldCount = MoldCount, MoldList = _moldList};
            _commandHandler.Handle(createCommand);
        }

        [Fact]
        public void HandleDelete_should_throw_when_BreakTemplate_not_found()
        {
            var command = new BreakTemplateDelete { BranchId = BranchId, Id = Id };
            Action expectedException = () => _commandHandler.Handle(command);

            expectedException.Should().Throw<BreakTemplateNotFoundException>();
        }
    }
}