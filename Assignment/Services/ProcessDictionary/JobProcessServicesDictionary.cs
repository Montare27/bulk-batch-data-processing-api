namespace Assignment.Services.ProcessDictionary
{
    using Account;
    using DataAccess.Interfaces;
    using Logging;
    using Process;
    using System.Collections.Generic;

    public class JobProcessServicesDictionary : IProcessServicesDictionary
    {
        private readonly Dictionary<string, IProcessService> _processServices;
        
        public JobProcessServicesDictionary(IJobLogger jobLogger, IUnitOfWork unitOfWork, AccountService accountService)
        {
            _processServices = new Dictionary<string, IProcessService>
            {
                {"bulk", new BulkProcessService(unitOfWork, accountService, jobLogger)},
                {"batch", new BatchProcessService(unitOfWork, accountService, jobLogger)},
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// Returns successor of IProcessResult dependent on name of job
        /// Otherwise returns null
        /// </returns>
        public IProcessService TryGetValue(string name)
        {
            _processServices.TryGetValue(name, out var value);
            return value;
        }
        
    }
}
