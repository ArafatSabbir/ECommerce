using Autofac;
using Microsoft.Extensions.Configuration;
using System;

namespace NecessaryDrugs.Data
{
    public class DataModule : Module
    {
        private IConfiguration _configuration;
        private string _connectionString;
        private string _migrationAssemblyName;

        public DataModule(IConfiguration configuration, string connectionStringName, string migrationAssemblyName)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(connectionStringName);
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>))
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
