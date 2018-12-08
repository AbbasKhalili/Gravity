using System.Collections.Generic;
using Geotechnic.Facade.Contracts.BreakTemplate.DTOs;
using Gravity.Core;

namespace Geotechnic.Facade.Contracts.BreakTemplate.Services
{
    public interface IBreakTemplateFacadeQuery : IFacadeService
    {
        //[HasPermission(RealEstatePermissions.ViewTags)]
        BreakTemplateDto Get(long id, long branchId);

        //[HasPermission(RealEstatePermissions.ViewTags)]
        List<BreakTemplateDto> GetAll(long branchId);
    }
}
