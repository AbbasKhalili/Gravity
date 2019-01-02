using Geotechnic.Facade.Contracts.Additives.Commands;
using Geotechnic.Facade.Contracts.Additives.Services;
using Gravity.Application;

namespace Geotechnic.Facade.Services
{
    public class AdditivesFacadeService : IAdditivesFacadeService
    {
        private readonly ICommandBus _bus;

        public AdditivesFacadeService(ICommandBus bus)
        {
            _bus = bus;
        }

        public void Create(AdditiveCreate command)
        {
            _bus.Dispatch(command);
        }

        public void Modify(AdditiveUpdate command)
        {
            _bus.Dispatch(command);
        }

        public void Delete(AdditiveDelete command)
        {
            _bus.Dispatch(command);
        }
    }
}
