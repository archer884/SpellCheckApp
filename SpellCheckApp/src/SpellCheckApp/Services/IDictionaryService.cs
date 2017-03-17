using System.Collections.Generic;

namespace SpellCheckApp.Services
{
    public interface IDictionaryService
    {
        bool IsCorrect(string word);
        IEnumerable<string> Suggestions(string word);
    }
}
