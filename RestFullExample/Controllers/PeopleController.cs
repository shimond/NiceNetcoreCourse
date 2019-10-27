using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestFullExample.Model;
using RestFullExample.Model.Configuration;

namespace RestFullExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Category1")]
    public class PeopleController : ControllerBase
    {
        private static List<Person> people = new List<Person>();
        private FirebaseConfig fireBaseConfig;

        [HttpGet("config")]
        public ActionResult<FirebaseConfig> GetConfig()
        {
            return Ok(this.fireBaseConfig);
        }
        [HttpGet("Claims")]

        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(x=> new { x.Type, x.Value }).ToList());
        }

        [HttpGet]
        [Authorize(Roles  = "Admin")]
        public ActionResult<Person[]> Get()
        {
            return Ok(people.ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult<Person[]> GetById(int id)
        {
            var person = people.FirstOrDefault(x => x.ID == id);
            if (person != null)
            {
                return Ok(person);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Person[]> AddNewPerson(Person p)
        {
            
            p.ID = people.Last().ID + 1;
            people.Add(p);
            return Created("api/people/" + p.ID, p);
        }

        [HttpPut("{id}")]
        public ActionResult<Person[]> Update(int id, Person p)
        {
            var person = people.FirstOrDefault(x => x.ID == id);
            if (person == null)
            {
                return NotFound();
            }
            person.Email = p.Email;
            person.Birthdate = p.Birthdate;
            person.Name = p.Name;
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public ActionResult<Person[]> DeletePerson(int id)
        {
            var person = people.FirstOrDefault(x => x.ID == id);
            if (person == null)
            {
                return NotFound();
            }
            people.Remove(person);
            return Ok(person);
        }

        static PeopleController()
        {
            for (int i = 1; i < 10; i++)
            {
                people.Add(new Person
                {
                    ID = i,
                    Name = "Person" + i,
                    Birthdate = DateTime.Today.AddDays(-1 * i),
                    Email = $"Person{i}@gmail.com"
                });
            }
        }

        public PeopleController(IOptions<FirebaseConfig> firebaseConfig)
        {
            this.fireBaseConfig = firebaseConfig.Value;
        }
    }

}




