using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpellCheckApp.Config;
using SpellCheckApp.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace SpellCheckApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var wordListPath = AppEnvironment.GetEnvironmentVariable("SPELL_CHECK_APP_DICT") ?? "./words.txt";
            var suggestionProvider = new LevenshteinSuggestionProvider();
            var dictionaryService = new WordListDictionaryService(suggestionProvider, ReadWordList(wordListPath));
            var wrappedDictionaryService = new CustomDictionaryService(dictionaryService, 
                new KeyValuePair<string, string>("Rudy", "Hodolfo"),
                new KeyValuePair<string, string>("Rudy", "Delicious"),
                new KeyValuePair<string, string>("Rodolfo", "Hodolfo"),
                new KeyValuePair<string, string>("Rodolfo", "Delicious"));

            services.AddMvc();
            services.Add(new ServiceDescriptor(typeof(IDictionaryService), wrappedDictionaryService));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private IEnumerable<string> ReadWordList(string path)
        {
            if (Directory.Exists(path))
            {
                return Directory.EnumerateFiles(path).SelectMany(file => File.ReadLines(file));
            }

            if (File.Exists(path))
            {
                return File.ReadLines(path);
            }

            return Enumerable.Empty<string>();
        }
    }
}
