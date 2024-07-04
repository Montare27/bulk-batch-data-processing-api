namespace Assignment.DataAccess.Interfaces
{
    using Assignment.Models;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        IRepository<Job> JobRepository { get; }
        
        IRepository<JobType> JobTypeRepository { get; }
        
        IRepository<JobItem> JobItemRepository { get; }
        
        IRepository<Account> AccountRepository { get; }
        
        IRepository<LogEntry> LogEntryRepository { get; }
        
        IRepository<LogCollection> LogCollectionRepository { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
