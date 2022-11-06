using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Interfaces
{
    public interface IEntity<T>
    {
        public T? Id { get; set; }
    }
}