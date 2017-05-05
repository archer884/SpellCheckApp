using Microsoft.AspNetCore.Mvc;
using SpellCheckApp.Models;
using SpellCheckApp.Services;

namespace SpellCheckApp.Controllers
{
    [Route("api/word")]
    public class WordController : Controller
    {
        IDictionaryService _service;

        public WordController(IDictionaryService service)
        {
            _service = service;
        }

        [HttpGet("{word}")]
        public IActionResult Get(string word)
        {
            if (_service.IsCorrect(word))
            {
                return Ok(new CheckResult(word));
            }
            else
            {
                return Ok(new CheckResult(word, _service.Suggestions(word)));
            }
        }
    }
}
