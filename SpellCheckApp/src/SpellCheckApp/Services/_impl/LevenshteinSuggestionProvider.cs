using Fastenshtein;
using System.Collections.Generic;
using System.Linq;

namespace SpellCheckApp.Services
{
    public class LevenshteinSuggestionProvider : ISuggestionProvider
    {
        public IEnumerable<string> Suggestions(string word, IList<string> dictionary)
        {
            var levenshtein = new Levenshtein(word);
            var results = dictionary.Select(candidate => new EditDistance
            {
                Word = candidate,
                Distance = levenshtein.Distance(candidate),
            });

            return results.OrderBy(item => item.Distance).Select(item => item.Word);
        }

        public IEnumerable<string> Suggestions(string word, IList<string> dictionary, int count)
        {
            return Suggestions(word, dictionary).Take(count);
        }

        class EditDistance
        {
            public string Word;
            public int Distance;
        }
    }
}
