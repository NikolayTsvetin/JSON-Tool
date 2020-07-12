using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_Tool
{
    public static class JsonParser<T>
    {
        public static object Parse(string input)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (input[0] == '{' && input[input.Length - 1] == '}')
            {
                string[] kvp = input.TrimStart('{').TrimEnd('}').Split(',');

                foreach (var kvpParsed in kvp)
                {
                    string[] parsed = kvpParsed.Split(':');
                    string key = ParseString(parsed[0]);
                    object value = Parse(parsed[1]);

                    result.Add(key, value);
                }

                return result;
            }
            else
            {
                return ParseFactory(input);
            }
        }

        private static object ParseFactory(string input)
        {
            bool boolResult = default(bool);
            double doubleResult = default(double);
            int intResult = default(int);

            if (input[0] == '"')
            {
                return ParseString(input);
            }
            else if (bool.TryParse(input, out boolResult))
            {
                return boolResult;
            }
            else if (int.TryParse(input, out intResult))
            {
                return intResult;
            }
            else if (double.TryParse(input, out doubleResult))
            {
                return doubleResult;
            }
            else if (input[0] == '[' && input[input.Length - 1] == ']')
            {
                input = input.TrimStart('[');
                input = input.TrimEnd(']');
                string[] splitted = input.Split(',');

                List<object> collection = new List<object>();

                if (input[0] == '{' && input[input.Length - 1] == '}')
                {
                    StringBuilder currentObject = new StringBuilder();

                    // Ugly way to handle IEnumerable<object>. Should be refactored.
                    for (int i = 0; i < input.Length; i++)
                    {
                        // Corner case. Awful. Fix it.
                        if (input[i] == ',' && input[i + 1] == '{' && input[i - 1] == '}')
                        {
                            continue;
                        }

                        currentObject.Append(input[i]);

                        if (input[i] == '}')
                        {
                            collection.Add(Parse(currentObject.ToString()));
                            currentObject = new StringBuilder();
                        }
                    }

                    return collection;
                }

                foreach (var item in splitted)
                {
                    collection.Add(Parse(item));
                }

                return collection;
            }
            else
            {
                // Replace '.' with ',' and try double again
                input = input.Replace('.', ',');

                if (double.TryParse(input, out doubleResult))
                {
                    return doubleResult;
                }
            }

            throw new InvalidOperationException("Couldn't parse the provided input.");
        }

        // Helpers
        private static string ParseString(string input)
        {
            input = input.Trim('"');
            input = ReplaceSpecialSymbols(input);

            return input;
        }

        private static string ReplaceSpecialSymbols(string input)
        {
            input = input.Replace(@"\\", "\\");
            input = input.Replace(@"\t", "\t");
            input = input.Replace(@"\n", "\n");
            input = input.Replace(@"\b", "\b");
            input = input.Replace(@"\f", "\f");
            input = input.Replace(@"\a", "\a");
            input = input.Replace(@"\r", "\r");
            input = input.Replace(@"\v", "\v");
            input = input.Replace(@"\" + "\"", "\"");

            return input;
        }

        public static Dictionary<string, object> ToDictionary(object inputObject)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            // ToString() and handle the objects one by one.

            return result;
        }
    }
}
