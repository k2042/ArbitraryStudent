using ArbitraryStudent.Service.Controllers.Model;
using ArbitraryStudent.Service.Services;
using ArbitraryStudent.Service.Services.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArbitraryStudent.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentSvc;

        public StudentsController(StudentService studentSvc)
        {
            _studentSvc = studentSvc;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _studentSvc.GetStudentsAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _studentSvc.GetStudentAsync(id);

            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostStudent student)
        {
            var result = await _studentSvc.NewStudentAsync(new Student()
            {
                FullName = student.FullName,
                Birthdate = student.Birthdate,
            });

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PutStudent student)
        {
            var result = await _studentSvc.UpdateStudentAsync(new Student()
            {
                Id = id,
                FullName = student.FullName,
                Birthdate = student.Birthdate,
                GradeId = student.GradeId,
            });

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _studentSvc.DeleteStudentAsync(id);

            if (result == null)
                return NotFound(id);
            else
                return Ok(result);
        }

    }
}
