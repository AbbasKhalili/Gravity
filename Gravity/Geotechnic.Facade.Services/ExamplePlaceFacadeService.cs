using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Geotechnic.Facade.Contracts.ExamplePlace.Services;
using Gravity.Application;

namespace Geotechnic.Facade.Services
{
    public class ExamplePlaceFacadeService : IExamplePlaceFacadeService
    {
        private readonly ICommandBus _bus;

        public ExamplePlaceFacadeService(ICommandBus bus)
        {
            _bus = bus;
        }

        public void Create(ExamplePlaceCreate command)
        {
            _bus.Dispatch(command);
        }

        public void Modify(ExamplePlaceUpdate command)
        {
            _bus.Dispatch(command);
        }

        public void Delete(ExamplePlaceDelete command)
        {
            _bus.Dispatch(command);
        }
    }
}