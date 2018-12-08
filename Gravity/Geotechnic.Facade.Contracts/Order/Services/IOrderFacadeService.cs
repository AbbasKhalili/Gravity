using Geotechnic.Facade.Contracts.Order.Commands;
using Gravity.Core;

namespace Geotechnic.Facade.Contracts.Order.Services
{
    public interface IOrderFacadeService : IFacadeService
    {
        //[HasPermission(RealEstatePermissions.CreateTag)]
        void Create(OrderCreate command);

        //[HasPermission(RealEstatePermissions.ModifyTag)]
        void Modify(OrderUpdate command);

        //[HasPermission(RealEstatePermissions.DeleteTag)]
        void Delete(OrderDelete command);
    }
}