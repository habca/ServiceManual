using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IFactoryDeviceService<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(string? id);

        Task<T> Post(T factoryDevice);

        Task Put(T factoryDevice);

        Task Delete(T factoryDevice);
    }
}