using System.Collections.Generic;
using Geotechnic.Facade.Contracts.ExamplePlace.DTOs;
using Gravity.Core;

namespace Geotechnic.Facade.Contracts.ExamplePlace.Services
{
    public interface IExamplePlaceFacadeQuery : IFacadeService
    {
        //[HasPermission(RealEstatePermissions.ViewTags)]
        ExamplePlaceDto Get(long id, long branchId);

        //[HasPermission(RealEstatePermissions.ViewTags)]
        List<ExamplePlaceDto> GetAll(long branchId);
    }
}