﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyKidsReg.Models;
using MyKidsReg.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyKidsReg.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    //URL: api/MyKidsReg
    [ApiController]
    public class TeacherRelationController : ControllerBase
    {
        private readonly ITeacherRelationServices _service;

        public TeacherRelationController(ITeacherRelationServices service)
        {
            _service = service;
        }

        // GET: api/<TeacherRelationController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherRelation>>> Get()
        {
            var teacherRelations = await _service.GetAll();
            return Ok(teacherRelations);
        }

        // GET api/<TeacherRelationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherRelation>> Get(int id)
        {
            var teacherRelation = await _service.GetById(id);
            if (teacherRelation == null)
            {
                return NotFound();
            }
            return Ok(teacherRelation);
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TeacherRelation>>> GetByUserId(int userId)
        {
            var teacherRelations = await _service.GetByUserId(userId);
            if (teacherRelations == null || !teacherRelations.Any())
            {
                return NotFound();
            }
            return Ok(teacherRelations);
        }
        [HttpGet("department/{department_id}")]
        public async Task<ActionResult<IEnumerable<TeacherRelation>>> GetByDepartmentid(int department_id)
        {
            var teacherRelations = await _service.GetByDepartmentId(department_id);
            if (teacherRelations == null || !teacherRelations.Any())
            {
                return NotFound();
            }
            return Ok(teacherRelations);
        }
        // POST api/<TeacherRelationController>
        [HttpPost]
        public async Task<IActionResult> CreateTeacherRelation(TeacherRelation newTeacherRelation)
        {
            if (ModelState.IsValid)
            {

                await _service.CreateTeacherRelations(newTeacherRelation);

                return Ok(newTeacherRelation);
            }
            return BadRequest(ModelState);
        }


        // PUT api/<TeacherRelationController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TeacherRelation teacherRelation)
        {
            if (id != teacherRelation.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateTeacherRelations(id, teacherRelation);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

            return NoContent();
        }


        // DELETE api/<TeacherRelationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var foundRelation = await _service.GetById(id);
            if (foundRelation != null)
            {
                await _service.DeleteTeacherRelations(id);
                return Ok(foundRelation);
            }

            
            return NoContent();
        }
    }
}
