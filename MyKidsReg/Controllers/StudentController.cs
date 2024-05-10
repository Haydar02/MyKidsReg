using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;
using MyKidsReg.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _service;

        public StudentController(IStudentServices service)
        {
            _service = service;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            var students = await _service.GetAll();
            return Ok(students);
        }


        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _service.GetByID(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student newStudent)
        {
            if (ModelState.IsValid)
            {                         
                               
                await _service.CreateStudentAsync(newStudent);

                
                return Ok(newStudent);
            }
            
            return BadRequest(ModelState);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateStudents(id, student);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            return NoContent();
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteStudent(id);
            return NoContent();
        }
    }
}
