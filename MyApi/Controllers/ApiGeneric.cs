using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Business.Abstract;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : ControllerBase where T : class
    {
        private readonly IGenericService<T> _entityService;

        public GenericController(IGenericService<T> entityService)
        {
            _entityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<T>> GetAll()
        {
            var entities = _entityService.GetAll();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public ActionResult<T> GetById(int id)
        {
            var entity = _entityService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public ActionResult<T> Add([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _entityService.Add(entity);
            return Ok(entity);
        }

        [HttpPut]
        public ActionResult<T> Update([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _entityService.Update(entity);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var entity = _entityService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            _entityService.Delete(entity.Data);
            return NoContent();
        }
    }
}
