using System.Collections.Generic;

namespace SpellCheckApp.Services
{
    public interface ISuggestionProvider
    {
        IEnumerable<string> Suggestions(string word, IList<string> dictionary);
        IEnumerable<string> Suggestions(string word, IList<string> dictionary, int count);
    }
}
