using Geotechnic.Facade.Contracts.BreakTemplate.Commands;
using Geotechnic.Facade.Contracts.BreakTemplate.Services;
using Gravity.Application;

namespace Geotechnic.Facade.Services
{
    public class BreakTemplateFacadeService : IBreakTemplateFacadeService
    {
        private readonly ICommandBus _bus;

        public BreakTemplateFacadeService(ICommandBus bus)
        {
            _bus = bus;
        }

        public void Create(BreakTemplateCreate command)
        {
            _bus.Dispatch(command);
        }

        public void Modify(BreakTemplateUpdate command)
        {
            _bus.Dispatch(command);
        }

        public void Delete(BreakTemplateDelete command)
        {
            _bus.Dispatch(command);
        }
    }
}