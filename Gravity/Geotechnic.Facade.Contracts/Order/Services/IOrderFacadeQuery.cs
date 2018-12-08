using System.Collections.Generic;
using Geotechnic.Facade.Contracts.Order.DTOs;
using Gravity.Core;

namespace Geotechnic.Facade.Contracts.Order.Services
{
    public interface IOrderFacadeQuery : IFacadeService
    {
        //[HasPermission(RealEstatePermissions.ViewTags)]
        OrderDto Get(long id, long branchId);

        //[HasPermission(RealEstatePermissions.ViewTags)]
        List<OrderDto> GetAll(long branchId);

        long GetLastExampleNumber(long branchId);
    }
}
