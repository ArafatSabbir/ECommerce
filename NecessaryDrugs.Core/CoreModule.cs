using Autofac;
using Microsoft.Extensions.Configuration;
using NecessaryDrugs.Core.Contexts;
using NecessaryDrugs.Core.Repositories;
using NecessaryDrugs.Core.Services;
using NecessaryDrugs.Core.UnitOfWorks;
using System;

namespace NecessaryDrugs.Core
{
    public class CoreModule: Module
    {
        private IConfiguration _configuration;
        private string _connectionString;
        private string _migrationAssemblyName;

        public CoreModule(IConfiguration configuration, string connectionStringName, string migrationAssemblyName)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString(connectionStringName);
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //DbContexts
            builder.RegisterType<MedicineStoreContext>()
                   .WithParameter("connectionString", _connectionString)
                   .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                   .InstancePerLifetimeScope();
            builder.RegisterType<MedicineStoreContext>().As<IMedicineStoreContext>()
                .WithParameter("connectionString",_connectionString)
                .WithParameter("migrationAssemblyName",_migrationAssemblyName)
                .InstancePerLifetimeScope();

            //Repositories
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MedicineRepository>().As<IMedicineRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MedicineCategoryRepository>().As<IMedicineCategoryRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockRepository>().As<IStockRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>()
                .InstancePerLifetimeScope();

            //Services
            builder.RegisterType<CategoryService>().As<ICategoryService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MedicineService>().As<IMedicineService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockService>().As<IStockService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>()
                .InstancePerLifetimeScope();

            //UnitOfWorks
            builder.RegisterType<MedicineStoreUnitOfWork>().As<IMedicineStoreUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
