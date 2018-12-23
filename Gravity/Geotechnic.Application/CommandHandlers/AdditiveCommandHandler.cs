using System;
using Geotechnic.Application.Exceptions;
using Geotechnic.Domain.Additives;
using Geotechnic.Domain.OrderConcrete;
using Geotechnic.Facade.Contracts.Additives.Commands;
using Gravity.Application;
using Gravity.Tools;

namespace Geotechnic.Application.CommandHandlers
{
    public class AdditiveCommandHandler : ICommandHandler<AdditiveCreate>
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

        public void Handle(AdditiveCreate handle)
        {
            Guard<BranchNotFoundException>.SmallerThan(handle.BranchId,1);
            Guard<AdditiveTitleRequiredException>.AgainstNullOrEmpty(handle.Title);
            
            var nextId = _idBuilder.WithId(_additiveRepository.GetNextId()).Build();
            var model = new Additive(handle.BranchId, nextId, handle.Title);
            _additiveRepository.Create(model);
        }
    }
}
