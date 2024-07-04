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
    /// Represents a repository for managing data access and operations for a LogCollection
    /// </summary>
    public class EfLogCollectionRepository : IRepository<LogCollection>
    {
        private readonly ApplicationDbContext _db;

        public EfLogCollectionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid Add(LogCollection item)
        {
            var id = Guid.NewGuid();
            item.Id = id;
            
            _db.LogCollections.Add(item);
            return id;
        }

        public Guid Update(LogCollection item)
        {
            var id = item.Id;

            var existedJob = _db.LogCollections.FirstOrDefault(x => x.Id == id);
            if (existedJob == null)
            {
                throw new EntityNotFoundException(nameof(LogCollection), id);
            }

            _db.Entry(existedJob).CurrentValues.SetValues(item);

            return id;
        }

        public void Delete(LogCollection item)
        {
            _db.LogCollections.Remove(item);
        }

        public async Task<LogCollection> GetById(Guid id)
        {
            return await _db.LogCollections
                .Include(x=>x.Logs)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<LogCollection> GetByPredicate(Expression<Func<LogCollection, bool>> predicate)
        {
            return await _db.LogCollections
                .Include(x=>x.Logs)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<LogCollection> GetAll()
        {
            return _db.LogCollections.AsEnumerable();
        }
    }
}
