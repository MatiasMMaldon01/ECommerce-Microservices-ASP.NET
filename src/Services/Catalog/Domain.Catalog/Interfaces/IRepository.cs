using Domain.Catalog.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Domain.Catalog.Interfaces
{
    public interface IRepository<T> where T : Base
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(string id);

        T GetById(string id);
        IEnumerable<T> GetByFilter(FilterDefinition<T> filter = null);
    }
}
