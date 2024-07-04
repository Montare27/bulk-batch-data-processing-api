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
    /// Represents a repository for managing data access and operations for a JobItem
    /// </summary>
    public class EfJobItemRepository : IRepository<JobItem>
    {
        private readonly ApplicationDbContext _db;

        public EfJobItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid Add(JobItem item)
        {
            var id = Guid.NewGuid();
            item.Id = id;
            
            _db.JobItems.Add(item);
            return id;
        }

        public Guid Update(JobItem item)
        {
            var id = item.Id;

            var existedJob = _db.JobItems.FirstOrDefault(x => x.Id == id);
            if (existedJob == null)
            {
                throw new EntityNotFoundException(nameof(JobItem), id);
            }

            _db.Entry(existedJob).CurrentValues.SetValues(item);

            return id;
        }

        public void Delete(JobItem item)
        {
            _db.JobItems.Remove(item);
        }

        public async Task<JobItem> GetById(Guid id)
        {
            return await _db.JobItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<JobItem> GetByPredicate(Expression<Func<JobItem, bool>> predicate)
        {
            return await _db.JobItems.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<JobItem> GetAll()
        {
            return _db.JobItems.AsEnumerable();
        }
    }
}
