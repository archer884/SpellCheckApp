using Moq;
using SpellCheckApp.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SpellCheckApp.Tests.Services
{
    using SystemUnderTest = CustomDictionaryService;

    public class CustomDictionaryServiceTests
    {
        public class Given_a_word_with_custom_corrections
        {
            public class When_that_word_is_otherwise_correct
            {
                SystemUnderTest Sut = InitSystemUnderTest(Mock.Of<IDictionaryService>(
                    x => x.IsCorrect("vegetarian") == true));

                [Fact]
                public void IsCorrect_returns_false()
                {
                    Assert.False(Sut.IsCorrect("vegetarian"));
                }

                [Fact]
                public void Suggestions_are_still_returned()
                {
                    Assert.Collection(Sut.Suggestions("vegetarian"),
                        suggestion => Assert.Equal("bad hunter", suggestion),
                        suggestion => Assert.Equal("commie", suggestion),
                        suggestion => Assert.Equal("bright and blessed child of God", suggestion));
                }
            }

            public class When_that_word_is_incorrect
            {
                // Here we provide several suggestions for "Rudy" which should go unused
                // because our custom dictionary provides its own suggestions.
                SystemUnderTest Sut = InitSystemUnderTest(Mock.Of<IDictionaryService>(
                    x => x.IsCorrect("Rudy") == false && x.Suggestions("Rudy") == new[] { "Judy", "Ruby" }));

                [Fact]
                public void IsCorrect_returns_false()
                {
                    Assert.False(Sut.IsCorrect("Rudy"));
                }

                [Fact]
                public void Custom_suggestions_are_returned()
                {
                    Assert.Collection(Sut.Suggestions("Rudy"),
                        suggestion => Assert.Equal("Hodolfo", suggestion));
                }
            }
        }

        public class Given_a_word_without_custom_corrections
        {
            public class When_that_word_is_correct
            {
                SystemUnderTest Sut = InitSystemUnderTest(Mock.Of<IDictionaryService>(
                    x => x.IsCorrect("brick") == true && x.Suggestions("brick") == Enumerable.Empty<string>()));

                [Fact]
                public void IsCorrect_returns_true()
                {
                    Assert.True(Sut.IsCorrect("brick"));
                }

                [Fact]
                public void Suggestions_are_not_returned()
                {
                    Assert.Empty(Sut.Suggestions("brick"));
                }
            }

            public class When_that_word_is_incorrect
            {
                SystemUnderTest Sut = InitSystemUnderTest(Mock.Of<IDictionaryService>(
                    x => x.IsCorrect("apple") == false && x.Suggestions("apple") == new[] { "banana" }));

                [Fact]
                public void IsCorrect_returns_false()
                {
                    Assert.False(Sut.IsCorrect("apple"));
                }

                [Fact]
                public void Suggestions_are_returned()
                {
                    Assert.Collection(Sut.Suggestions("apple"), suggestion => Assert.Equal("banana", suggestion));
                }
            }
        }

        static SystemUnderTest InitSystemUnderTest(IDictionaryService service)
        {
            return new SystemUnderTest(service,
                new KeyValuePair<string, string>("Rudy", "Hodolfo"),
                new KeyValuePair<string, string>("Rodolfo", "Hodolfo"),
                new KeyValuePair<string, string>("vegetarian", "bad hunter"),
                new KeyValuePair<string, string>("vegetarian", "commie"),
                new KeyValuePair<string, string>("vegetarian", "bright and blessed child of God"));
        }
    }
}
