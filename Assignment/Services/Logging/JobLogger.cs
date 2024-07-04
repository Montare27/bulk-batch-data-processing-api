namespace Assignment.Services.Logging
{
    using DataAccess.Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class with logging interactions
    /// </summary>
    public class JobLogger : IJobLogger
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobLogger(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Creates new instance of Log for current JobItem and adds it to Db
        /// if item's process failed
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="itemId"></param>
        /// <param name="message">description of error</param>
        public async Task LogError(Guid jobId, Guid itemId, string message)
        {
            var logCollection = await _unitOfWork.LogCollectionRepository
                .GetByPredicate(l => l.JobId == jobId);
            
            if (logCollection == null)
            {
                logCollection = new LogCollection {JobId = jobId };
                
                _unitOfWork.LogCollectionRepository.Add(logCollection);
            }

            var logEntry = logCollection.Logs
                .FirstOrDefault(e => e.ItemId == itemId);

            if (logEntry == null)
            {
                logEntry = new LogEntry{ItemId = itemId, Description = message, Success = false};
                
                logCollection.Logs.Add(logEntry);
            }
            else
            {
                logEntry.Success = false;
                
                logEntry.ItemId = itemId;
                
                logEntry.Description = message;
            }
            
            await _unitOfWork.SaveChangesAsync(default);
        }

        /// <summary>
        /// Creates new instance of Log for current JobItem and adds it to Db
        /// if item's process finished with success
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="itemId"></param>
        public async Task LogSuccess(Guid jobId, Guid itemId)
        {
            var logCollection = await _unitOfWork.LogCollectionRepository
                .GetByPredicate(l => l.JobId == jobId);
            
            if (logCollection == null)
            {
                logCollection = new LogCollection {JobId = jobId };
                
                _unitOfWork.LogCollectionRepository.Add(logCollection);
            }

            var logEntry = logCollection.Logs
                .FirstOrDefault(e => e.ItemId == itemId);

            if (logEntry == null)
            {
                logEntry = new LogEntry{ItemId = itemId};
                
                logCollection.Logs.Add(logEntry);
            }
            else
            {
                logEntry.ItemId = itemId;
            }
            
            await _unitOfWork.SaveChangesAsync(default);
        }

        /// <summary>
        /// Returns enumerable of logs by id of job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns>enumerable of logs</returns>
        public async Task<IEnumerable<LogEntry>> GetLogs(Guid jobId)
        {
            var logCollection = await _unitOfWork.LogCollectionRepository
                .GetByPredicate(l => l.JobId == jobId);

            if (logCollection == null)
            {
                return new List<LogEntry>();
            }

            return logCollection.Logs;
        }
    }
}
