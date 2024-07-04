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
    /// Represents a repository for managing data access and operations for a Job
    /// </summary>
    public class EfJobRepository : IRepository<Job>
    {
        private readonly ApplicationDbContext _db;

        public EfJobRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid Add(Job item)
        {
            item.Id = (item.Id == Guid.Empty) ? Guid.NewGuid() : item.Id;

            var type = _db.JobTypes.FirstOrDefault(x=>x.Name.Equals(item.Type.Name));
            if (type != null)
            {
                item.Type = type;
            }

            _db.Jobs.Add(item);
            return item.Id;
        }

        public Guid Update(Job item)
        {
            var id = item.Id;

            var existedJob = _db.Jobs.FirstOrDefault(x => x.Id == id);
            if (existedJob == null)
            {
                throw new EntityNotFoundException(nameof(Job), id);
            }

            _db.Entry(existedJob).CurrentValues.SetValues(item);

            return id;
        }

        public void Delete(Job item)
        {
            _db.Jobs.Remove(item);
        }

        public async Task<Job> GetById(Guid id)
        {
            return await _db.Jobs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Job> GetByPredicate(Expression<Func<Job, bool>> predicate)
        {
            return await _db.Jobs.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Job> GetAll()
        {
            return _db.Jobs.AsEnumerable();
        }
    }
}
