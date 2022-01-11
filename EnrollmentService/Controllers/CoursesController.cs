using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private ICourse _course;
        private IMapper _mapper;

        public CoursesController(ICourse course, IMapper mapper)
        {
            _course = course;
            _mapper = mapper;
        }

        // [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> Get()
        { 
            var results = await _course.GetAll();
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(results));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> Get(int id)
        {
            try
            {
                var result = await _course.GetById(id.ToString());
                // if (result == null)
                //     return NotFound();

                return Ok(_mapper.Map<CourseDto>(result));
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<ActionResult<CourseDto>> Post([FromBody] CourseForCreateDto courseForCreateDto)
        {
            try
            {
                var course = _mapper.Map<Models.Course>(courseForCreateDto);
                var result = await _course.Insert(course);
                var coursedto = _mapper.Map<Dtos.CourseDto>(result);
                return Ok(coursedto);  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> Put(int id, [FromBody] CourseForCreateDto courseToCreateDto)
        {
            try
            {
                var course = _mapper.Map<Models.Course>(courseToCreateDto);
                var result = await _course.Update(id.ToString(), course);
                var coursedto = _mapper.Map<Dtos.CourseDto>(result);
                return Ok(coursedto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _course.Delete(id.ToString());
                return Ok($"delete data id {id} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get By Title
        [HttpGet("bytitle")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetByTitle(string title)
        {
            var courses = await _course.GetByTitle(title);
            var result = _mapper.Map<IEnumerable<CourseDto>>(courses);
            return Ok(result);
            //results;
        }
        
    }
}