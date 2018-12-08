using Geotechnic.Facade.Contracts.Additives.Commands;
using Gravity.Core;

namespace Geotechnic.Facade.Contracts.Additives.Services
{
    public interface IAdditivesFacadeService : IFacadeService
    {
        //[HasPermission(RealEstatePermissions.CreateTag)]
        void Create(AdditiveCreate command);

        //[HasPermission(RealEstatePermissions.ModifyTag)]
        void Modify(AdditiveUpdate command);

        //[HasPermission(RealEstatePermissions.DeleteTag)]
        void Delete(AdditiveDelete command);
    }
}