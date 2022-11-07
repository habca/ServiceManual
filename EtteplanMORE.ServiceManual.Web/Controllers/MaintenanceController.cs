using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    public class MaintenanceController : GenericCrudController<Maintenance, string>
    {
        public MaintenanceController(IFactoryDeviceService<Maintenance, string> factoryDeviceService) : base(factoryDeviceService)
        {
        }

        /// <summary>
        /// Every maintenance task sorted first by criticality and then by date of record.
        /// </summary>
        /// <remarks>The result may not have any record at all.</remarks>
        /// <returns>All entries from the database in an array.</returns>
        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            IEnumerable<Maintenance> array = await _service.GetAll();
            array = array.OrderBy(c => c.DateTime);
            array = array.OrderBy(c => c.Criticality);
            return Ok(array);
        }

        /// <summary>
        /// Those maintenance tasks that affect the factory devices to be repaired.
        /// </summary>
        /// <param name="fdId">The factory device being repaired.</param>
        /// <returns>Maintenance tasks associated to the same factory device to be repaired.</returns>
        [HttpGet("filter/{fdId}")]
        public async Task<IActionResult> Filter(string fdId)
        {
            if (fdId == null)
            {
                return BadRequest();
            }

            IEnumerable<Maintenance> array = await _service.GetAll();
            array = array.Where(c => c.FactoryDevice == fdId);
            return Ok(array);
        }
    }
}