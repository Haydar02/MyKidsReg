using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentRelationsController : ControllerBase
    {
        private readonly IParentRelationServices _service;

        public ParentRelationsController(IParentRelationServices service)
        {
            _service = service;
        }

        // GET: api/<ParentRelationsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParentsRelation>>> Get()
        {
            var parentsRelations = await _service.GetAll();
            return Ok(parentsRelations);
        }

        // GET api/<ParentRelationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParentsRelation>> Get(int id)
        {
            var parentsRelation = await _service.GetById(id);
            if (parentsRelation == null)
            {
                return NotFound();
            }
            return Ok(parentsRelation);
        }

        // POST api/<ParentRelationsController>
        [HttpPost]
        public async Task<IActionResult> CreateParentRelation(ParentsRelation newParentRelation)
        {
            if (ModelState.IsValid)
            {

                await _service.CreateParentRelations(newParentRelation);

                return Ok(newParentRelation);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<ParentRelationsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ParentsRelation parentsRelation)
        {
            if (id != parentsRelation.User_id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateParentRelations(id, parentsRelation);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            return NoContent();
        }

        // DELETE api/<ParentRelationsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteParentRelations(id);
            return NoContent();
        }
    }
}
