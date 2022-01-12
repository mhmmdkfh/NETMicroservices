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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private IEnrollment _enrollment;
        private IMapper _mapper;
        public EnrollmentsController(IEnrollment enrollment, IMapper mapper)
        {
            _enrollment = enrollment ?? throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/<EnrollmentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> Get()
        {
            var enrollments = await _enrollment.GetAll();
            var results = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
            return Ok(results);
        }

        //Get By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDto>> Get(int id)
        {
            try
            {
                var result = await _enrollment.GetById(id.ToString());
                // if (result == null)
                //     return NotFound();

                return Ok(_mapper.Map<EnrollmentDto>(result));
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        // POST api/<EnrollmentController>
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> Post([FromBody] EnrollmentForCreateDto enrollmentForCreateDto)
        {
            try
            {
                var enrollment = _mapper.Map<Models.Enrollment>(enrollmentForCreateDto);
                var result = await _enrollment.Insert(enrollment);
                var enrollmentdto = _mapper.Map<Dtos.EnrollmentDto>(result);
                return Ok(enrollmentdto);  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [Authorize(Roles = "admin")]
        // PUT api/<EnrollmentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EnrollmentDto>> Put(int id, [FromBody] EnrollmentForCreateDto enrollmentToCreateDto)
        {
            try
            {
                var enrollment = _mapper.Map<Models.Enrollment>(enrollmentToCreateDto);
                var result = await _enrollment.Update(id.ToString(), enrollment);
                var enrollmentdto = _mapper.Map<Dtos.EnrollmentDto>(result);
                return Ok(enrollmentdto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        // DELETE api/<EnrollmentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _enrollment.Delete(id.ToString());
                return Ok($"delete data id {id} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
    }
}