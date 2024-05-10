using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServices _service;

        public DepartmentController(IDepartmentServices service)
        {
            _service = service;
        }

        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get() 
        {
            var departments = await _service.GetAll();
            return Ok(departments);
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
       public async Task<ActionResult<Department>> Get(int id)
        {
            var department = await _service.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department newDepartment)
        {
            if (ModelState.IsValid)
            {

                await _service.CreateDepartment(newDepartment);

                    return Ok(newDepartment);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.UpdateDepartment(id, department);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            return NoContent();
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            await _service.DeleteDepartment(id);
            return NoContent();
        }
    }
}
