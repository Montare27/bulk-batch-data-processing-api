namespace Assignment.Services.Results
{
    using System;

    public enum ProcessingValues
    {
        ValidationFailed,
        AccountDoesNotExist,
        NotEnoughMoney,
        UnSupportedType,
        Success,
    }
    
    public class ProcessingResult
    {
        public ProcessingResult(Guid id, string message, ProcessingValues status)
        {
            JobId = id;
            Message = message;
            Status = status;
        }
        
        public Guid JobId { get; set; }
        
        public ProcessingValues Status { get; set; }
        
        public string Message { get; set; }

        public static ProcessingResult ValidationFailed(Guid id = new Guid()) => 
            new ProcessingResult(
            id: id, 
            message: "Entity or it's field is null", 
            status: ProcessingValues.ValidationFailed);
        
        public static ProcessingResult UnSupportedType(Guid id, string type) => 
            new ProcessingResult(
            id: id, 
            message: $"{type} is unsupported job type for the current in job {id}", 
            status: ProcessingValues.UnSupportedType);
        
        public static ProcessingResult AccountDoesNotExist(Guid id, Guid accountId) => 
            new ProcessingResult(
            id, 
            message: $"Account by id {accountId} does not exist in job {id}", 
            status: ProcessingValues.AccountDoesNotExist);
        
        public static ProcessingResult NotEnoughMoney(Guid id, Guid accountId) => 
            new ProcessingResult(
            id, 
            message: $"Account by id {accountId} has not enough money in job {id}", 
            status: ProcessingValues.NotEnoughMoney);

        public static ProcessingResult Successful(Guid id) =>
            new ProcessingResult(
            id, 
            message: $"Job {id} was processed successfully", 
            status: ProcessingValues.Success);
    }
}
