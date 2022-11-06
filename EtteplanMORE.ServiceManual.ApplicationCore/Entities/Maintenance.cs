using System;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Entities
{
    public class Maintenance : MongoEntity
    {
        public string? FactoryDevice { get; set; }
        public CriticalityEnum Criticality { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string? Description { get; set; }

        public enum CriticalityEnum
        {
            Critical, High, Small
        }

        public enum StatusEnum
        {
            Open, Closed
        }
    }
}