using Geotechnic.Domain.OrderConcrete;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Geotechnic.Persistence.Mappings
{
    public class OrderMapping : ClassMapping<Order>
    {
        public OrderMapping()
        {
            Table("OrderConcretes");
            Lazy(false);

            ComponentAsId(a => a.Id, map => map.Property(a => a.DbId, a => a.Column("Id")));

            Property(x => x.Axis, map => { map.Column("Axis"); });
            Property(x => x.ConcreteSeller, map => { map.Column("ConcreteSeller"); });
            Property(x => x.ConcreteTemperature, map => { map.Column("ConcreteTemperature"); });
            Property(x => x.Cutie, map => { map.Column("Cutie"); });
            Property(x => x.EnvironmentTemperature, map => { map.Column("EnvironmentTemperature"); });
            Property(x => x.ExampleDate, map => { map.Column("ExampleDate"); map.NotNullable(true); });
            Property(x => x.CementType, map => { map.Column("CementTypes"); map.NotNullable(true); });
            Property(x => x.ExampleNumber, map => { map.Column("ExampleNumber"); map.NotNullable(true); });
            Property(x => x.Fc, map => { map.Column("Fc"); map.NotNullable(true); });
            Property(x => x.Slamp, map => { map.Column("Slamp"); map.NotNullable(true); });
            Property(x => x.Volume, map => { map.Column("Volume"); });
            Property(x => x.BranchId, map => { map.Column("BranchId"); map.NotNullable(true); });
            Property(x => x.ExamplePlaceDesc, map => { map.Column("ExamplePlaceDesc"); });


            //Component(a => a.ProjectId, map =>
            //{
            //    map.Access(Accessor.Property);
            //    map.Property(a => a.DbId, a => a.Column("ProjectId"));
            //});

            Component(a => a.ExamplePlace, map =>
            {
                map.Access(Accessor.Property);
                map.Property(a => a.DbId, a => a.Column("ExamplePlaceId"));
            });

            Component(a => a.BreakTemplateId, map =>
            {
                map.Access(Accessor.Property);
                map.Property(a => a.DbId, a => a.Column("BreakTemplateId"));
            });


            IdBag(a => a.Additives, map =>
            {
                //map.Access(Accessor.Property);
                map.Table("OrderConcretesAdditives");
                map.Key(a => a.Column("OrderConcreteId"));
                map.Id(a =>
                {
                    a.Column("Id");
                    a.Generator(Generators.Identity);
                });
            }
            , rel =>
            {
                rel.Component(a => a.Property(z => z.DbId, z => z.Column("AdditiveId")));
            }
            );

        }
    }
}
