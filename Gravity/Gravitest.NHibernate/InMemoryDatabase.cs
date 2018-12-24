using System;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace Gravitest.NHibernate
{
    public class InMemoryDatabase : IDisposable
    {
        private readonly Configuration _configuration;
        private readonly ISessionFactory _sessionFactory;
        protected ISession Session;
        private readonly ModelMapper _modelMapper = new ModelMapper();

        public InMemoryDatabase(Assembly assembly)
        {
            if (_configuration == null)
            {
                _configuration = new Configuration();
                _configuration.SessionFactoryName("geo");
                _configuration.DataBaseIntegration(cfg =>
                {
                    cfg.Dialect<SQLiteDialect>();
                    cfg.Driver<SQLite20Driver>();
                    cfg.ConnectionString = "Data Source=:memory:";
                    cfg.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                    //cfg.IsolationLevel = IsolationLevel.ReadCommitted;
                });
                _modelMapper.AddMappings(assembly.GetExportedTypes());

                var hbmMapping = _modelMapper.CompileMappingForAllExplicitlyAddedEntities();
                _configuration.AddDeserializedMapping(hbmMapping, "geo");

                _sessionFactory = _configuration.BuildSessionFactory();
            }
            Session = _sessionFactory.OpenSession();
            new SchemaExport(_configuration).Execute(true, true, false, Session.Connection, null);
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
