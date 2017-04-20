using System;
using System.Collections.Generic;
using System.IO;

namespace SpellCheckApp.Config
{
    public static class AppEnvironment
    {
        private static Dictionary<string, string> _variables;

        static AppEnvironment()
        {
            _variables = BuildKeyValuePairs(File.ReadLines(".env"));
        }

        public static string GetEnvironmentVariable(string key)
        {
            return _variables.TryGetValue(key, out string value) ? value : Environment.GetEnvironmentVariable(key);
        }

        private static Dictionary<string, string> BuildKeyValuePairs(IEnumerable<string> input)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var line in input)
            {
                var parts = line.Split('=');

                if (parts.Length != 2)
                {
                    continue;
                }

                var value = parts[1];
                if (value.StartsWith("\'") || value.StartsWith("\""))
                {
                    dictionary[parts[0]] = value.Substring(1, value.Length - 2);
                }
                else
                {
                    dictionary[parts[0]] = value;
                }
            }
            return dictionary;
        }
    }
}
