using Domain.Catalog.Entities;
using Domain.Catalog.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infraestructure.Catalog.Repository
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(IDBSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.DataBase);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute) documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault())?.CollectionName;
        }

        public void Create(T entity)
        {
            _collection.InsertOne(entity);
        }

        public void Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq(t => t.Id, entity.Id);
            _collection.FindOneAndReplace(filter, entity);
        }

        public void Delete(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(t => t.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }


        public T GetById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(t => t.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<T> GetByFilter(FilterDefinition<T> filter = null)
        {

            if (filter == null)
            {
                return _collection.Find(p => true).ToEnumerable();
            }

            return _collection.Find(filter).ToEnumerable();

        }
    }
}
