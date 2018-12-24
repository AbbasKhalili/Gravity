using System;
using System.Linq;
using Geotechnic.Application.Exceptions;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.Additives.Commands;
using Gravity.Application;
using Gravity.Tools;

namespace Geotechnic.Application.CommandHandlers
{
    public class AdditiveCommandHandler : ICommandHandler<AdditiveCreate>, 
        ICommandHandler<AdditiveUpdate>, ICommandHandler<AdditiveDelete>
    {
        private readonly IAdditiveRepository _additiveRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEntityIdBuilder<AdditiveId> _idBuilder;

        public AdditiveCommandHandler(IAdditiveRepository additiveRepository, IOrderRepository orderRepository)
        {
            _additiveRepository = additiveRepository;
            _orderRepository = orderRepository;
            _idBuilder = new EntityIdBuilder<AdditiveId>();
        }

        public void Handle(AdditiveCreate command)
        {
            Guard<BranchNotFoundException>.SmallerThan(command.BranchId,1);
            
            var nextId = _idBuilder.WithId(_additiveRepository.GetNextId()).Build();
            var model = new Additive(command.BranchId, nextId, command.Title);
            _additiveRepository.Create(model);
        }

        public void Handle(AdditiveUpdate command)
        {
            Guard<BranchNotFoundException>.SmallerThan(command.BranchId, 1);

            var id = _idBuilder.WithId(command.Id).Build();
            var model = _additiveRepository.Get(id);

            Guard<AdditiveNotFoundException>.AgainstNull(model);

            model.Update(command.Title);
        }

        public void Handle(AdditiveDelete command)
        {
            var orders = _orderRepository.GetAll().Where(x => x.Additives.Any(c => x.BranchId == command.BranchId && c.DbId == command.Id)).ToList();
            Guard<AdditiveUsedException>.BiggerThan(orders.Count, 0);

            var id = _idBuilder.WithId(command.Id).Build();
            var model = _additiveRepository.Get(id);

            Guard<AdditiveNotFoundException>.AgainstNull(model);

            _additiveRepository.Delete(model);
        }
    }
}
