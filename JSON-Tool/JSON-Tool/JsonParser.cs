using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                // Remove the first and last char ('{' and '}')
                string trimmed = input.Substring(1, input.Length - 2);
                List<string> kvp = SplitObjectByKeyValuePairs(trimmed);

                foreach (var kvpParsed in kvp)
                {
                    if (isObject(kvpParsed))
                    {
                        List<string> extractedKeyAndValue = ExtractKeyAndValue(kvpParsed);
                        var parsed = Parse(extractedKeyAndValue[1]);

                        result.Add(ParseString(extractedKeyAndValue[0]), parsed);
                    }
                    else
                    {
                        string[] parsed = kvpParsed.Split(':');
                        string key = ParseString(parsed[0]);
                        object value = Parse(parsed[1]);

                        result.Add(key, value);
                    }
                }

                return result;
            }
            else
            {
                return ParseFactory(input);
            }
        }

        private static List<string> ExtractKeyAndValue(string kvpParsed)
        {
            int indexOfSeparator = kvpParsed.IndexOf(':');
            string key = kvpParsed.Substring(0, indexOfSeparator);
            string value = kvpParsed.Substring(indexOfSeparator + 1);

            return new List<string>() { key, value };
        }

        private static bool isObject(string kvpParsed)
        {
            return kvpParsed.Split(':').Length > 2 ? true : false;
        }

        // Helpers
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
                        int opening = currentObject.ToString().Count(c => c == '{');
                        int closing = currentObject.ToString().Count(c => c == '}');

                        if (input[i] == '}' && opening == closing)
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

        private static List<string> SplitObjectByKeyValuePairs(string inputObject)
        {
            List<string> kvp = new List<string>();
            StringBuilder sb = new StringBuilder();
            bool isObject = false;
            int openingBrackets = 0;
            int closingBrackets = 0;

            for (int i = 0; i < inputObject.Length; i++)
            {
                if (inputObject[i] == ',' && !isObject)
                {
                    kvp.Add(sb.ToString());
                    sb = new StringBuilder();

                    continue;
                }

                if (inputObject[i] == '{')
                {
                    openingBrackets++;
                    isObject = true;
                }

                if (inputObject[i] == '}')
                {
                    closingBrackets++;

                    if (openingBrackets == closingBrackets)
                    {
                        isObject = false;
                    }
                }

                sb.Append(inputObject[i]);
            }

            kvp.Add(sb.ToString());

            return kvp;
        }
    }
}
