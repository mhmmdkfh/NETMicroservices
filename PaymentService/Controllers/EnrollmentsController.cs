using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/p/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollment _enrollment;
        private readonly IMapper _mapper;

        public EnrollmentsController(IEnrollment enrollment, IMapper mapper)
        {
            _enrollment = enrollment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentCreateDto>>> GetEnrollments()
        {
            Console.WriteLine("--> Get Enrollment From Payment Service");
            var enrollmentItems = await _enrollment.GetAllEnrollments();
            var results = _mapper.Map<IEnumerable<EnrollmentCreateDto>>(enrollmentItems);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEnrollment(EnrollmentCreateDto enrollment)
        {
            try
            {
                Console.WriteLine("--> Get Message");
                var enroll = _mapper.Map<Enrollment>(enrollment);
                await _enrollment.CreateEnrollemnt(enroll);
                Console.WriteLine("--> Enrollments added !");
                return Ok($"Data enrollment StudentId: {enrollment.StudentID} dan CourseId: {enrollment.CourseID} berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}