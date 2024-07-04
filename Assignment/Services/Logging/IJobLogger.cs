namespace Assignment.Services.Logging
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJobLogger
    {
        Task LogError(Guid jobId, Guid itemId, string message);
        
        Task LogSuccess(Guid jobId, Guid itemId);

        Task<IEnumerable<LogEntry>> GetLogs(Guid jobId);
    }
}
