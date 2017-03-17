using System.Collections.Generic;
using System.Linq;

namespace SpellCheckApp.Models
{
    public class CheckResult
    {
        public string Word { get; set; }
        public bool IsCorrect { get; set; }
        public IEnumerable<string> Suggestions { get; set; } = Enumerable.Empty<string>();

        public CheckResult(string word, bool isCorrect)
        {
            Word = word;
            IsCorrect = isCorrect;
        }

        public CheckResult(string word)
            : this(word, true)
        {
        }

        public CheckResult(string word, IEnumerable<string> suggestions)
            : this(word, false)
        {
            Suggestions = suggestions;
        }
    }
}
