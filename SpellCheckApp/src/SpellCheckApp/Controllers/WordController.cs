using Microsoft.AspNetCore.Mvc;
using SpellCheckApp.Models;
using SpellCheckApp.Services;

namespace SpellCheckApp.Controllers
{
    public class WordController : Controller
    {
        IDictionaryService _service;

        public WordController(IDictionaryService service)
        {
            _service = service;
        }

        [HttpGet]
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
