namespace Assignment.DataAccess.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T>
    {
        Guid Add(T item);
        Guid Update(T item);
        void Delete(T item);
        Task<T> GetById(Guid id);
        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
    }
}
