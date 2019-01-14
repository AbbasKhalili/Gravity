using Geotechnic.Facade.Contracts.Order.Commands;
using Geotechnic.Facade.Contracts.Order.Services;
using Gravity.Application;

namespace Geotechnic.Facade.Services
{
    public class OrderFacadeService : IOrderFacadeService
    {
        private readonly ICommandBus _bus;

        public OrderFacadeService(ICommandBus bus)
        {
            _bus = bus;
        }

        public void Create(OrderCreate command)
        {
            _bus.Dispatch(command);
        }

        public void Modify(OrderUpdate command)
        {
            _bus.Dispatch(command);
        }

        public void Delete(OrderDelete command)
        {
            _bus.Dispatch(command);
        }
    }
}