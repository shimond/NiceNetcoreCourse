using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using firstWebapp.code;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace firstWebapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IDateData _dateData;

        [HttpGet()]
        public ActionResult<Person> Get()
        {
            return Ok(new Person { ID = 1, Name = "David", InvokedDate = this._dateData.InvokedTime });
        }

        public TestController(IDateData dateData)
        {
            this._dateData = dateData;
        }
    }
}