namespace Assignment.Services.Process
{
    using Account;
    using DataAccess.Interfaces;
    using Exceptions;
    using Logging;
    using Models;
    using Results;
    using System.Threading.Tasks;
    using Validation;

    /// <summary>
    /// Class that contains logic for processing Batch jobs
    /// </summary>
    public class BatchProcessService : IProcessService
    {
        private readonly IJobLogger _jobLogger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AccountService _accountService;

        public BatchProcessService(IUnitOfWork unitOfWork, AccountService accountService, IJobLogger jobLogger)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _jobLogger = jobLogger;
        }

        public async Task<ProcessingResult> Process(Job job)
        {
            if (!ValidatorAssembly.Validate(job) || !ValidatorAssembly.Validate(job.Type))
            {
                return ProcessingResult.ValidationFailed(job.Id);
            }

            if (await _unitOfWork.JobTypeRepository.GetByPredicate(x => x.Name.Equals(job.Type.Name)) == null)
            {
                return ProcessingResult.UnSupportedType(job.Id, job.Type.Name);
            }
            
            _unitOfWork.JobRepository.Add(job);
            
            await _unitOfWork.SaveChangesAsync(default);
            
            foreach (var item in job.Items)
            {
                try
                {
                    await _accountService.Transfer(item.AccountId, item.DestinationAccountId, item.Amount);
                }
                catch (AccountDoesNotExistException ex)
                {
                    var result = ProcessingResult.AccountDoesNotExist(job.Id, ex.AccountId);
                    
                    await _jobLogger.LogError(job.Id, item.Id, result.Message);
                    
                    return result;
                }
                catch (NotEnoughMoneyException ex)
                {
                    var result = ProcessingResult.NotEnoughMoney(job.Id, ex.AccountId);
                    
                    await _jobLogger.LogError(job.Id, item.Id, result.Message);
                        
                    return result;
                }
                finally
                {
                    await _jobLogger.LogSuccess(job.Id, item.Id);
                }
            }

            return ProcessingResult.Successful(job.Id);
        }
    }
}
