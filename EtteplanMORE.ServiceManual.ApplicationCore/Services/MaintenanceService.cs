using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using MongoDB.Driver;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Services
{
    public class MaintenanceService : IFactoryDeviceService<Maintenance>
    {
        private readonly IMongoCollection<Maintenance> _context;

        public MaintenanceService()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");

            var mongoDatabase = mongoClient.GetDatabase("ServiceManual");

            _context = mongoDatabase.GetCollection<Maintenance>("Maintenances");
        }

        public async Task<IEnumerable<Maintenance>> GetAll()
        {
            return await _context.Find(_ => true).ToListAsync();
        }

        public async Task<Maintenance> Get(string? id)
        {
            return await _context.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Maintenance> Post(Maintenance obj)
        {
            await _context.InsertOneAsync(obj);

            // Return the newly added entity, because the primary key is automatically generated.
            // Otherwise the user should search for the most recent record, which is not thread-safe.
            return await Get(obj.Id);
        }

        public async Task Put(Maintenance obj)
        {
            await _context.ReplaceOneAsync(c => c.Id == obj.Id, obj);
        }

        public async Task Delete(Maintenance obj)
        {
            await _context.DeleteOneAsync(c => c.Id == obj.Id);
        }
    }
}