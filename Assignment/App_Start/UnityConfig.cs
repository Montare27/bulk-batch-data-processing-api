using Assignment.DataAccess.EFImplementations;
using Assignment.DataAccess.Interfaces;
using Assignment.Models;
using Assignment.Services.Account;
using Assignment.Services.Logging;
using Assignment.Services.Process;
using Assignment.Validation;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Assignment
{
    using DataAccess;
    using Services.ProcessDictionary;
    using System.Data.Entity;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IUnitOfWork, EfUnitOfWork>(new HierarchicalLifetimeManager());

            container.RegisterType<AccountService>(new HierarchicalLifetimeManager());
            
            container.RegisterType<IProcessServicesDictionary, JobProcessServicesDictionary>(new TransientLifetimeManager());

            container.RegisterType<IProcessService, BatchProcessService>(new TransientLifetimeManager());
            container.RegisterType<IProcessService, BulkProcessService>(new TransientLifetimeManager());
            
            container.RegisterType<IJobLogger, JobLogger>(new HierarchicalLifetimeManager());

            container.RegisterType<JobServiceStrategy>(new TransientLifetimeManager());
            
            container.RegisterType<IValidator<Job>, JobValidator>(new TransientLifetimeManager());
            container.RegisterType<IValidator<JobType>, JobTypeValidator>(new TransientLifetimeManager());
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}