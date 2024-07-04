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
    /// Class that contains logic for processing Bulk jobs
    /// </summary>
    public class BulkProcessService : IProcessService
    {
        private readonly IJobLogger _jobLogger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AccountService _accountService;

        public BulkProcessService(IUnitOfWork unitOfWork, AccountService accountService, IJobLogger jobLogger)
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
            
            if (await _unitOfWork.JobTypeRepository
                    .GetByPredicate(x => x.Name.Equals(job.Type.Name)) == null)
            {
                return ProcessingResult.UnSupportedType(job.Id, job.Type.Name);
            }

            _unitOfWork.JobRepository.Add(job);
            
            await _unitOfWork.SaveChangesAsync(default);
            
            var isErrorInProcessing = false;
            var processingResult = ProcessingResult.Successful(job.Id);

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
                    
                    if (!isErrorInProcessing)
                    {
                        processingResult = result;
                        isErrorInProcessing = true;
                    }
                }
                catch (NotEnoughMoneyException ex)
                {
                    var result = ProcessingResult.NotEnoughMoney(job.Id, ex.AccountId);
                    
                    await _jobLogger.LogError(job.Id, item.Id, result.Message);

                    if (!isErrorInProcessing)
                    {
                        processingResult = result;
                        isErrorInProcessing = true;
                    }
                }
                finally
                {
                    await _jobLogger.LogSuccess(job.Id, item.Id);
                }
            }

            return processingResult;
        }
    }
}
