using System.Collections.Generic;

namespace SpellCheckApp.Services
{
    /// <summary>
    /// Wraps a dictionary service to provide customized checking and suggestions
    /// on top of a standard dictionary.
    /// </summary>
    public class CustomDictionaryService : IDictionaryService
    {
        private Dictionary<string, List<string>> _mappings = new Dictionary<string, List<string>>();
        private IDictionaryService _service;

        public CustomDictionaryService(IDictionaryService service)
        {
            _service = service;
        }

        public CustomDictionaryService(IDictionaryService service, params KeyValuePair<string, string>[] mappingItems)
            : this(service)
        {
            foreach (var item in mappingItems)
            {
                List<string> value;
                if (_mappings.TryGetValue(item.Key, out value))
                {
                    value.Add(item.Value);
                }
                else
                {
                    _mappings[item.Key] = new List<string> { item.Value };
                }
            }
        }

        public bool IsCorrect(string word) => !_mappings.ContainsKey(word) && _service.IsCorrect(word);

        // Unlike in the case of IsCorrect, we do not forward the call for recommendations to 
        // the wrapped dictionary. This is because the user has provided us with explicit 
        // recommendations for exactly this case, and we want to return just those.
        public IEnumerable<string> Suggestions(string word)
        {
            List<string> value;
            if (_mappings.TryGetValue(word, out value))
            {
                return value;
            }
            else
            {
                return _service.Suggestions(word);
            }
        }
    }
}
