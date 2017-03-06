using Microsoft.AspNetCore.Mvc;
using SpellCheckApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheckApp.Controllers
{
    public class WordController : Controller
    {
        [HttpGet()]
        public IActionResult Get(string word)
        {

            var response = new Word();
            return Ok(response);
        }
    }
}
