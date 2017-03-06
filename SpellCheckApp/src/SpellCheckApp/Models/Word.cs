using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheckApp.Models
{
    public class Word
    {
        public string WordToBeChecker { get; set; }
        public bool IsCorrectlySpelled { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Suggestions { get; set; }
    }
}
