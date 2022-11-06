using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using MongoDB.Driver;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public abstract class GenericDatabaseService<T, S> : IFactoryDeviceService<T, S> where T : IEntity<S>
    {
        protected readonly IMongoCollection<T> _collection;

        public GenericDatabaseService(string connection, string database, string collection)
        {
            var mongoClient = new MongoClient(connection);

            var mongoDatabase = mongoClient.GetDatabase(database);

            _collection = mongoDatabase.GetCollection<T>(collection);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> Get(S? id)
        {
            if (id == null)
            {
                throw new NullReferenceException();
            }
            return await _collection.Find(c => id.Equals(c.Id)).FirstOrDefaultAsync();
        }

        public async Task<T> Post(T obj)
        {
            if (obj == null || obj.Id != null)
            {
                throw new NullReferenceException();
            }
            await _collection.InsertOneAsync(obj);
            return await Get(obj.Id);
        }

        public async Task Put(T obj)
        {
            if (obj == null || obj.Id == null)
            {
                throw new NullReferenceException();
            }
            await _collection.ReplaceOneAsync(c => obj.Id.Equals(c.Id), obj);
        }

        public async Task Delete(T obj)
        {
            if (obj == null || obj.Id == null)
            {
                throw new NullReferenceException();
            }
            await _collection.DeleteOneAsync(c => obj.Id.Equals(c.Id));
        }
    }
}