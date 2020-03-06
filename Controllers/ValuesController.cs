using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeApp.Data;
using PracticeApp.Models;

namespace PracticeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Property for Context
        public DataContext _Context { get; }

        //Create Contsructor for DI and creating context for table
        public ValuesController(DataContext context)
        {
            _Context = context;
        }
   
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            //Asynchronous requests it deos not block thread it will keep open request for all users
            return await _Context.Students.ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Student GetById(int id)
        {
            return _Context.Students.FirstOrDefault(s=>s.RollNo==id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Student std)
        {
            _Context.Students.Add(std);
            _Context.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut]
        [Route("UpdateEmployeeDetails")]
        public void Put([FromBody] Student std)
        {
            var existingRecord = _Context.Students.FirstOrDefault(x => x.RollNo == std.RollNo);
            if (existingRecord != null)
            {
                existingRecord.FirstName = std.FirstName;
                existingRecord.LastName = std.LastName;
                existingRecord.Address = std.Address;
                _Context.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var existingRecord = _Context.Students.FirstOrDefault(x => x.RollNo == id);
            _Context.Students.Remove(existingRecord);
            _Context.SaveChanges();
        }
    }
}
