using SpellCheckApp.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SpellCheckApp.Test.Services
{
    using SystemUnderTest = WordListDictionaryService;

    public class WordListDictionaryServiceTests
    {
        public class When_a_word_is_correct
        {
            SystemUnderTest Sut = InitSystemUnderTest(new List<string> { "hello" });
            string SpellcheckedWord = "hello";

            [Fact]
            public void IsCorrect_returns_true()
            {
                Assert.True(Sut.IsCorrect(SpellcheckedWord));
            }

            [Fact]
            public void Suggestions_are_still_returned()
            {
                Assert.Equal(new[] { "finally!", "go away" }, Sut.Suggestions(SpellcheckedWord));
            }
        }

        public class When_a_word_is_incorrect
        {
            SystemUnderTest Sut = InitSystemUnderTest(new List<string> { "hello" });
            string SpellcheckedWord = "notaword";

            [Fact]
            public void IsCorrect_returns_false()
            {
                Assert.False(Sut.IsCorrect(SpellcheckedWord));
            }

            [Fact]
            public void Suggestions_are_still_returned()
            {
                Assert.Equal(new[] { "finally!", "go away" }, Sut.Suggestions(SpellcheckedWord));
            }
        }

        static SystemUnderTest InitSystemUnderTest(List<string> dictionary)
        {
            return new SystemUnderTest(new DummySuggestionProvider(), dictionary);
        }

        class DummySuggestionProvider : ISuggestionProvider
        {
            static string[] _suggestions = { "finally!", "go away" };

            public IEnumerable<string> Suggestions(string word, IList<string> dictionary)
            {
                return _suggestions;
            }

            public IEnumerable<string> Suggestions(string word, IList<string> dictionary, int count)
            {
                return _suggestions.Take(count);
            }
        }
    }
}
