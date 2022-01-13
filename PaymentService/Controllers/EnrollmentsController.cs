using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Dtos;
using PaymentService.EventProcessing;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/p/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IPaymentRepo _repository;
        private readonly IMapper _mapper;
        private readonly IEventProcessor _eventProcessor;

        public EnrollmentsController(IPaymentRepo repository, IMapper mapper, IEventProcessor eventProcessor)
        {
            _repository = repository;
            _mapper = mapper;
            _eventProcessor = eventProcessor;
        }

        
        public ActionResult<IEnumerable<EnrollmentReadDto>> GetEnrollments()
        {
            Console.WriteLine("--> Get Enrollment From Payment Service");
            var enrollmentItems = _repository.GetAllEnrollments();
            return Ok(_mapper.Map<IEnumerable<EnrollmentReadDto>>(enrollmentItems));
        }

        [HttpPost]
        public ActionResult<IEnumerable<EnrollmentReadDto>> CreateEnrollment(string enrollmentPublished){
            Console.WriteLine("--> Get Message");
            var message = JsonSerializer.Deserialize<EnrollmentPublishedDto>(enrollmentPublished);
            var enrollment = _mapper.Map<Enrollment>(message);
            var result = _repository.Insert(enrollment);
            var enrollmentDto = _mapper.Map<EnrollmentReadDto>(result);
            return Ok(enrollmentDto);
           
        }

        // [HttpPost]
        public ActionResult TestInBoundConnection(){
            Console.WriteLine("--> Inbound Post Payment Service");
            return Ok("Inbound test from payments controller");
        }
        
        // public Task<ActionResult<EnrollmentReadDto>> Post(EnrollmentPublishedDto enrollmentPublished)
        // {
        //     var enrollmentGet = JsonSerializer.Deserialize<EnrollmentReadDto>(enrollmentPublished);
        //     try
        //     {
        //          var enroll = _mapper.Map<Enrollment>(enrollmentGet);
        //          var result =  _repository.Insert(enroll);
        //          var enrollDto = _mapper.Map<EnrollmentReadDto>(result);

        //          return(enrollDto);
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
        //     }
            
        // }
        
    }
}