using System.Linq;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.BreakTemplates;
using Geotechnic.Domain.ExamplePlaces;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.Order.Commands;
using Gravity.Application;
using Gravity.Tools.DateTools;

namespace Geotechnic.Application.CommandHandlers
{
    public class OrderCommandHandler : ICommandHandler<OrderCreate>, 
        ICommandHandler<OrderUpdate>, ICommandHandler<OrderDelete>, ICommandHandler<BreakUpdate>
    {

        private readonly IEntityIdBuilder<OrderId> _idBuilder;
        private readonly IEntityIdBuilder<ExamplePlaceId> _examplePlaceIdBuilder;
        private readonly IEntityIdBuilder<BreakTemplateId> _breakTemplateIdBuilder;
        private readonly IEntityIdBuilder<AdditiveId> _additiveIdBuilder;
        private readonly IOrderRepository _repository;

        public OrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
            _idBuilder = new EntityIdBuilder<OrderId>();
            _examplePlaceIdBuilder = new EntityIdBuilder<ExamplePlaceId>();
            _breakTemplateIdBuilder = new EntityIdBuilder<BreakTemplateId>();
            _additiveIdBuilder = new EntityIdBuilder<AdditiveId>();
        }


        public void Handle(OrderCreate command)
        {
            var nextId = _repository.GetNextId();
            var id = _idBuilder.WithId(nextId).Build();

            var orderModel = new OrderModel()
            {
                Axis = command.Axis,
                CementType = (CementTypes) command.CementType,
                ConcreteSeller = command.ConcreteSeller,
                ConcreteTemperature = command.ConcreteTemperature,
                Cutie = command.Cutie,
                EnvironmentTemperature = command.EnvironmentTemperature,
                ExampleDate = command.ExampleDate.ToGregorianDate(),
                ExampleNumber = command.ExampleNumber,
                ExamplePlace = _examplePlaceIdBuilder.WithId(command.ExamplePlaceId).Build(),
                ExamplePlaceDesc = command.ExamplePlaceDesc,
                Fc = command.Fc,
                ProjectId = command.ProjectId,
                Slamp = command.Slamp,
                Volume = command.Volume,
                BreakTemplateId = _breakTemplateIdBuilder.WithId(command.BreakTemplateId).Build(),
                Additives = command.AdditivesId.Select(a => new AdditiveId(a)).ToList(),
            };

            var examplePlace = new Order(command.BranchId, id, orderModel);

            _repository.Create(examplePlace);
        }

        public void Handle(OrderUpdate command)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(OrderDelete command)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(BreakUpdate command)
        {
            throw new System.NotImplementedException();
        }
    }
}