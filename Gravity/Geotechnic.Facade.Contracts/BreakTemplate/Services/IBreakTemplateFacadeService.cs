using Geotechnic.Facade.Contracts.BreakTemplate.Commands;
using Gravity.Core;

namespace Geotechnic.Facade.Contracts.BreakTemplate.Services
{
    public interface IBreakTemplateFacadeService : IFacadeService
    {
        //[HasPermission(RealEstatePermissions.CreateTag)]
        void Create(BreakTemplateCreate command);

        //[HasPermission(RealEstatePermissions.ModifyTag)]
        void Modify(BreakTemplateUpdate command);

        //[HasPermission(RealEstatePermissions.DeleteTag)]
        void Delete(BreakTemplateDelete command);
    }
}