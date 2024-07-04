namespace Assignment.Tests.Common
{
    using DataAccess.EFImplementations;
    using DataAccess.Interfaces;
    using Services.Account;
    using Services.Logging;
    using Services.Process;
    using Services.ProcessDictionary;
    using System;

    public class TestBase : TestContextBase, IDisposable
    {
        protected readonly JobServiceStrategy JobService;

        public TestBase()
        {
            IUnitOfWork unitOfWork = new EfUnitOfWork(Context);
            
            IJobLogger jobLogger = new JobLogger(unitOfWork);
            
            AccountService accountService = new AccountService(unitOfWork);
            
            IProcessServicesDictionary jobProcessServicesDictionary = new JobProcessServicesDictionary( jobLogger, unitOfWork, accountService);
            
            JobService = new JobServiceStrategy(jobProcessServicesDictionary);
         }
        
        public new void Dispose()
        {
            base.Dispose();
        }
    }
}
