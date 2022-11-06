using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public class FactoryDevice : MongoEntity
    {
        public string? Name { get; set; }
        public int Year { get; set; }
        public string? Type { get; set; }
    }
}