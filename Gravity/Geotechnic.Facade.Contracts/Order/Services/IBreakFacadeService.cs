using Geotechnic.Facade.Contracts.Order.Commands;
using Gravity.Core;

namespace Geotechnic.Facade.Contracts.Order.Services
{
    public interface IBreakFacadeService : IFacadeService
    {
        //[HasPermission(RealEstatePermissions.ModifyTag)]
        void Modify(BreakUpdate command);
    }
}