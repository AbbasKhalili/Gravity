using Geotechnic.Domain.ExamplePlaces;
using NHibernate.Mapping.ByCode.Conformist;

namespace Geotechnic.Persistence.Mappings
{
    public class ExamplePlaceMapping : ClassMapping<ExamplePlace>
    {
        public ExamplePlaceMapping()
        {
            Table("ExamplePlaces");
            Lazy(false);

            ComponentAsId(a => a.Id, map => map.Property(a => a.DbId, a => a.Column("Id")));

            Property(x => x.Character, map => { map.Column("Character"); });
            Property(x => x.Title, map => { map.Column("Title"); map.NotNullable(true); });
            Property(x => x.BranchId, map => { map.Column("BranchId"); map.NotNullable(true); });

        }
    }
}
