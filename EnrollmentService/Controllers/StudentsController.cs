using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private IStudent _student;
        private IMapper _mapper;

        public StudentsController(IStudent student,IMapper mapper)
        {
            _student = student ?? throw new ArgumentNullException(nameof(student));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<StudentDto>>> Get()
        {
            var students = await _student.GetAll();

            /*List<StudentDto> lstStudentDto = new List<StudentDto>();
            foreach (var student in students)
            {
                lstStudentDto.Add(new StudentDto
                {
                    ID = student.ID,
                    Name = $"{student.FirstName} {student.LastName}",
                    EnrollmentDate = student.EnrollmentDate
                });
            }*/

            var dtos = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            try
            {
                var result = await _student.GetById(id.ToString());
                // if (result == null)
                //     return NotFound();

                return Ok(_mapper.Map<StudentDto>(result));
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<StudentDto>> Post([FromBody] StudentForCreateDto studentforCreateDto)
        {
            try
            {
                var student = _mapper.Map<Models.Student>(studentforCreateDto);
                var result = await _student.Insert(student);
                var studentReturn = _mapper.Map<Dtos.StudentDto>(result);
                return Ok(studentReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> Put(int id,[FromBody] StudentForCreateDto studentForCreateDto)
        {
            try
            {
                var student = _mapper.Map<Models.Student>(studentForCreateDto);
                var result = await _student.Update(id.ToString(), student);
                var studentdto = _mapper.Map<Dtos.StudentDto>(result);
                return Ok(studentdto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _student.Delete(id.ToString());
                return Ok($"Data student {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}