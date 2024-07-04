namespace Assignment.Services.Process
{
    using Models;
    using ProcessDictionary;
    using Results;
    using System.Threading.Tasks;
    using Validation;

    /// <summary>
    /// Class that honors the pattern "Strategy" and redirects the processing action
    /// to successors of the IProcessService interface depending on the type of job.
    /// </summary>
    public class JobServiceStrategy
    {
        private readonly IProcessServicesDictionary _processServicesDictionary;

        public JobServiceStrategy(IProcessServicesDictionary processServicesDictionary)
        {
            _processServicesDictionary = processServicesDictionary;
        }

        /// <summary>
        /// Read above
        /// </summary>
        /// <param name="job"></param>
        /// <returns>Result of processing this job</returns>
        public async Task<ProcessingResult> Procedure(Job job)
        {
            if (!ValidatorAssembly.Validate(job) || !ValidatorAssembly.Validate(job.Type))
            {
                return ProcessingResult.ValidationFailed(job.Id);
            }
            
            IProcessService service = _processServicesDictionary.TryGetValue(job.Type?.Name);
            if (service == null)
            {
                return ProcessingResult.UnSupportedType(job.Id , job.Type?.Name);
            }
            
            return await service.Process(job);
        }
    }
}
