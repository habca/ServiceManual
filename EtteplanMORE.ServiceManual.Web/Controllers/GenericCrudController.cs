using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    public class GenericCrudController<T> : ControllerBase where T : IEntity
    {
        protected readonly IFactoryDeviceService<T> _service;

        public GenericCrudController(IFactoryDeviceService<T> service)
        {
            _service = service;
        }

        /// <summary>
        ///     HTTP GET: api/{controller}/
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await _service.GetAll()));
        }

        /// <summary>
        ///     HTTP GET: api/{controller}/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var obj = await _service.Get(id);
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        /// <summary>
        ///     HTTP POST: api/{controller}/
        /// </summary> 
        [HttpPost]
        public async Task<ActionResult> Post(T obj)
        {
            var found = await _service.Get(obj.Id);
            if (found != null)
            {
                return BadRequest();
            }

            obj = await _service.Post(obj);
            return CreatedAtAction(
                nameof(Get), new { id = obj.Id }, obj);
        }

        /// <summary>
        ///     HTTP PUT: api/{controller}/
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(T obj)
        {
            var fd = await _service.Get(obj.Id);
            if (fd == null)
            {
                return NotFound();
            }

            await _service.Put(obj);
            return NoContent();
        }

        /// <summary>
        ///     HTTP DELETE: api/{controller}/
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(T obj)
        {
            var found = await _service.Get(obj.Id);
            if (found == null)
            {
                return NotFound();
            }

            await _service.Delete(obj);
            return NoContent();
        }
    }
}