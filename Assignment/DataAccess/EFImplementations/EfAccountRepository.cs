namespace Assignment.DataAccess.EFImplementations
{
    using Interfaces;
    using Exceptions;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    
    /// <summary>
    /// Represents a repository for managing data access and operations for an Account
    /// </summary>
    public class EfAccountRepository : IRepository<Account>
    {
        private readonly ApplicationDbContext _db;

        public EfAccountRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid Add(Account item)
        {
            return _db.Set<Account>().Add(item).Id;
        }

        public Guid Update(Account item)
        {
            var id = item.Id;

            var existedItem = _db.Accounts.FirstOrDefault(x => x.Id == id);
            if (existedItem == null)
            {
                throw new EntityNotFoundException(nameof(Account), id);
            }

            _db.Entry(existedItem).CurrentValues.SetValues(item);
            
            return id;
        }

        public void Delete(Account item)
        {
            _db.Accounts.Remove(item);
        }

        public async Task<Account> GetById(Guid id)
        {
            return await _db.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> GetByPredicate(Expression<Func<Account, bool>> predicate)
        {
            return await _db.Accounts.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Account> GetAll()
        {
            return _db.Accounts.AsEnumerable();
        }
    }
}
