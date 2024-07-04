namespace Assignment.DataAccess.EFImplementations
{
    using Interfaces;
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Wrapper on DbContext
    /// </summary>
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        
        public EfUnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public IRepository<Job> JobRepository => 
            new EfJobRepository(_db);
        
        public IRepository<JobType> JobTypeRepository => 
            new EfJobTypeRepository(_db);
        
        public IRepository<JobItem> JobItemRepository => 
            new EfJobItemRepository(_db);
        
        public IRepository<Account> AccountRepository => 
            new EfAccountRepository(_db);
        
        public IRepository<LogEntry> LogEntryRepository =>
            new EfLogEntryRepository(_db);
        
        public IRepository<LogCollection> LogCollectionRepository => 
            new EfLogCollectionRepository(_db);

        /// <summary>
        /// Save changes to db
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// Number of saved entities
        /// </returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
