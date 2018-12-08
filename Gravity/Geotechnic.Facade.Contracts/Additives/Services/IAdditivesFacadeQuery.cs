using System.Collections.Generic;
using Geotechnic.Facade.Contracts.Additives.DTOs;
using Gravity.Core;

namespace Geotechnic.Facade.Contracts.Additives.Services
{
    public interface IAdditivesFacadeQuery : IFacadeService
    {
        //[HasPermission(RealEstatePermissions.ViewTags)]
        AdditiveDto Get(long id, long branchId);

        //[HasPermission(RealEstatePermissions.ViewTags)]
        List<AdditiveDto> GetAll(long branchId);
    }
}
