using System;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    public class GenericCrudController<T, S> : ControllerBase where T : IEntity<S>
    {
        protected readonly IFactoryDeviceService<T, S> _service;

        public GenericCrudController(IFactoryDeviceService<T, S> service)
        {
            _service = service;
        }

        /// <summary>
        ///     HTTP GET: api/{controller}/
        /// </summary>
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        /// <summary>
        ///     HTTP GET: api/{controller}/{id}
        /// </summary>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(S id)
        {
            if (id == null)
            {
                return BadRequest();
            }

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
        public virtual async Task<ActionResult> Post(T obj)
        {
            if (obj == null || obj.Id != null)
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
        public virtual async Task<IActionResult> Put(T obj)
        {
            if (obj == null || obj.Id == null)
            {
                return BadRequest();
            }

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
        public virtual async Task<IActionResult> Delete(T obj)
        {
            if (obj == null || obj.Id == null)
            {
                return BadRequest();
            }

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