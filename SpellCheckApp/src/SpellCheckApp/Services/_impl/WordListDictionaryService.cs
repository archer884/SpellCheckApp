using System.Collections.Generic;
using System.Linq;

namespace SpellCheckApp.Services
{
    public class WordListDictionaryService : IDictionaryService
    {
        private List<string> _words;
        private ISuggestionProvider _suggestionProvider;

        public WordListDictionaryService(ISuggestionProvider suggestionProvider, IEnumerable<string> words)
        {
            _suggestionProvider = suggestionProvider;
            _words = words.Where(word => !string.IsNullOrWhiteSpace(word)).OrderBy(word => word).ToList();
        }

        public WordListDictionaryService(ISuggestionProvider suggestionProvider, List<string> words)
        {
            _suggestionProvider = suggestionProvider;
            _words = words;
            _words.Sort();
        }

        public bool IsCorrect(string word)
        {
            return _words.BinarySearch(word) >= 0;
        }

        public IEnumerable<string> Suggestions(string word)
        {
            return Suggestions(word, 5);
        }

        public IEnumerable<string> Suggestions(string word, int count)
        {
            return _suggestionProvider.Suggestions(word, _words, count);
        }
    }
}
