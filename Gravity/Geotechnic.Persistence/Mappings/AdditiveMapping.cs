using Geotechnic.Domain.Additives;
using NHibernate.Mapping.ByCode.Conformist;

namespace Geotechnic.Persistence.Mappings
{
    public class AdditiveMapping : ClassMapping<Additive>
    {
        public AdditiveMapping()
        {
            Table("Additives");
            Lazy(false);

            ComponentAsId(a => a.Id, map => { map.Property(a => a.DbId, z => z.Column("Id")); });

            Property(x => x.BranchId, map => { map.Column("BranchId"); map.NotNullable(true); });
            Property(x => x.Title, map => { map.Column("Title"); map.NotNullable(true); });

        }
    }
}
