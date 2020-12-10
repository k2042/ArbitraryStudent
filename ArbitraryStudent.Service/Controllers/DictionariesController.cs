using ArbitraryStudent.Service.Controllers.Model;
using ArbitraryStudent.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArbitraryStudent.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly DictionaryService _dictionarySvc;

        public DictionariesController(DictionaryService dictionarySvc)
        {
            _dictionarySvc = dictionarySvc;
        }

        [HttpGet("grades")]
        public async Task<IActionResult> Get()
        {
            var result = await _dictionarySvc.GetGradesAsync();
            return Ok(result.ToDictionary(i => i.Key.ToString(), i => i.Value));
        }

        [HttpPost("grades")]
        public async Task<IActionResult> Post([FromBody] DictionaryValue value)
        {
            var result = await _dictionarySvc.NewGradeAsync(value.Name);
            return Ok(result);
        }

        [HttpDelete("grades/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _dictionarySvc.DeleteGradeAsync(id);
            return Ok();
        }
    }
}
