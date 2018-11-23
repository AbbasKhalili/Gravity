using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System.Data;
using System.Reflection;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Gravity.NHibernate
{
    public class SessionFactoryBuilder
    {
        private static ModelMapper _modelMapper = new ModelMapper();

        public static ISessionFactory Create(string connectionStringName)
        {
            var configuration = new Configuration();
            configuration.SessionFactoryName("geo");
            configuration.DataBaseIntegration(cfg =>
            {
                cfg.Dialect<MsSql2012Dialect>();
                cfg.Driver<SqlClientDriver>();
                cfg.ConnectionStringName = connectionStringName;
                cfg.IsolationLevel = IsolationLevel.ReadCommitted;
                //cfg.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";
            });
            //var modelMapper = new ModelMapper();
            //_modelMapper.AddMappings(assembly.GetExportedTypes());
            //modelMapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            var hbmMapping = _modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            configuration.AddDeserializedMapping(hbmMapping, "geo");

            return configuration.BuildSessionFactory();
        }

        public static void AddNhibernateMapping(Assembly assembly)
        {
            _modelMapper.AddMappings(assembly.GetExportedTypes());
        }
    }
}
