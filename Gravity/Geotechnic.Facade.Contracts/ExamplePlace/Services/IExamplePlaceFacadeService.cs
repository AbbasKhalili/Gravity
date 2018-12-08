using Geotechnic.Facade.Contracts.ExamplePlace.Commands;
using Gravity.Core;

namespace Geotechnic.Facade.Contracts.ExamplePlace.Services
{
    public interface IExamplePlaceFacadeService : IFacadeService
    {
        //[HasPermission(RealEstatePermissions.CreateTag)]
        void Create(ExamplePlaceCreate command);

        //[HasPermission(RealEstatePermissions.ModifyTag)]
        void Modify(ExamplePlaceUpdate command);

        //[HasPermission(RealEstatePermissions.DeleteTag)]
        void Delete(ExamplePlaceDelete command);
    }
}
