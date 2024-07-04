namespace Assignment.DataAccess.EFImplementations
{
    using Models;
    using System;
    using System.Linq;
    using Exceptions;
    using Interfaces;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    
    /// <summary>
    /// Represents a repository for managing data access and operations for a JobType
    /// </summary>
    public class EfJobTypeRepository : IRepository<JobType>
    {
        private readonly ApplicationDbContext _db;

        public EfJobTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid Add(JobType item)
        {
            var id = Guid.NewGuid();
            item.Id = id;
            
            _db.JobTypes.Add(item);
            return id;
        }

        public Guid Update(JobType item)
        {
            var id = item.Id;

            var existedJob = _db.JobTypes.FirstOrDefault(x => x.Id == id);
            if (existedJob == null)
            {
                throw new EntityNotFoundException(nameof(JobType), id);
            }

            _db.Entry(existedJob).CurrentValues.SetValues(item);

            return id;
        }

        public void Delete(JobType item)
        {
            _db.JobTypes.Remove(item);
        }

        public async Task<JobType> GetById(Guid id)
        {
            return await _db.JobTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<JobType> GetByPredicate(Expression<Func<JobType, bool>> predicate)
        {
            return await _db.JobTypes.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<JobType> GetAll()
        {
            return _db.JobTypes.AsEnumerable();
        }
    }
}
