namespace Assignment.DataAccess.EFImplementations
{
    using Exceptions;
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a repository for managing data access and operations for a LogEntry
    /// </summary>
    public class EfLogEntryRepository : IRepository<LogEntry>
    {

        private readonly ApplicationDbContext _db;

        public EfLogEntryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid Add(LogEntry item)
        {
            var id = Guid.NewGuid();
            item.Id = id;
            
            _db.LogEntries.Add(item);
            return id;
        }

        public Guid Update(LogEntry item)
        {
            var id = item.Id;

            var existedJob = _db.LogEntries.FirstOrDefault(x => x.Id == id);
            if (existedJob == null)
            {
                throw new EntityNotFoundException(nameof(LogEntry), id);
            }
            
            _db.Entry(existedJob).CurrentValues.SetValues(item);

            return id;
        }

        public void Delete(LogEntry item)
        {
            _db.LogEntries.Remove(item);
        }

        public async Task<LogEntry> GetById(Guid id)
        {
            return await _db.LogEntries.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<LogEntry> GetByPredicate(Expression<Func<LogEntry, bool>> predicate)
        {
            return await _db.LogEntries.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<LogEntry> GetAll()
        {
            return _db.LogEntries.AsEnumerable();
        }
    }
}
