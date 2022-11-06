using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IFactoryDeviceService<T, S> where T : IEntity<S>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(S? id);

        Task<T> Post(T obj);

        Task Put(T obj);

        Task Delete(T obj);
    }
}