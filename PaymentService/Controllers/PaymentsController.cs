using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/p/[controller]")]
    public class PaymentsController : ControllerBase
    {
        public PaymentsController()
        {
            
        }

        [HttpPost]
        public ActionResult TestInBoundConnection(){
            Console.WriteLine("--> Inbound Post Payment Service");
            return Ok("Inbound test from payments controller");
        }
        
    }
}