using SpellCheckApp.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SpellCheckApp.Test.Providers
{
    using SystemUnderTest = LevenshteinSuggestionProvider;

    public class LevenshteinSuggestionProviderTests
    {
        static SystemUnderTest Sut = new SystemUnderTest();
        static string SpellcheckedWord = "hello";
        static List<string> WordList = new List<string>
        {
            "hello",
            "finally!",
            "go away",
            "Emma Watson is a retard",
            "what about she for he",
            "I shouldn't listen to these shows while I code",
            "It just raises my blood pressure",
            "No one wants to see Emma Watson semi-nude anyway",
            "Ugh",
        };

        [Fact]
        public void All_words_are_returned()
        {
            Assert.Equal(WordList.Count, Sut.Suggestions(SpellcheckedWord, WordList).Count());
        }

        [Fact]
        public void Word_is_returned_as_its_own_suggestion()
        {
            Assert.Contains(SpellcheckedWord, Sut.Suggestions(SpellcheckedWord, WordList));
        }

        [Fact]
        public void Words_returned_are_limited_to_count()
        {
            Assert.Equal(3, Sut.Suggestions(SpellcheckedWord, WordList, 3).Count());
        }
    }
}
