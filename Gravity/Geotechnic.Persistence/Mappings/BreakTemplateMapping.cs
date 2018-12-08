using Geotechnic.Domain.BreakTemplates;
using NHibernate.Mapping.ByCode.Conformist;

namespace Geotechnic.Persistence.Mappings
{
    public class BreakTemplateMapping : ClassMapping<BreakTemplate>
    {
        public BreakTemplateMapping()
        {
            Table("BreakTemplates");
            Lazy(false);

            ComponentAsId(a => a.Id, map => map.Property(a => a.DbId, a => a.Column("Id")));

            Property(x => x.MoldCount, map => { map.Column("MoldCount"); map.NotNullable(true); });
            Property(x => x.Title, map => { map.Column("Title"); map.NotNullable(true); });
            Property(x => x.BranchId, map => { map.Column("BranchId"); map.NotNullable(true); });

            Bag(a => a.BreakTemplateMolds, map =>
                {
                    //map.Access(Accessor.Field);
                    map.Table("BreakTemplatesMolds");
                    map.Key(a => a.Column("BreakTemplateId"));

                }
                , rel =>
                {
                    rel.Component(a => a.Property(z => z.Count, z => z.Column("Count")));
                    rel.Component(a => a.Property(z => z.Age, z => z.Column("Age")));
                }
            );
        }
    }
}
