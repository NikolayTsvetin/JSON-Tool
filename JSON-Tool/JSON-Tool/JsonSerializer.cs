using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_Tool
{
    public static class JsonSerializer<T>
    {
        public static string Serialize(object input)
        {
            string inputType = input.GetType().Name;
            string result = String.Empty;

            if (inputType != typeof(object).Name)
            {
                result = SerializeFactory(input);
            }
            else
            {
                // ...
            }

            return result;
        }

        private static string SerializeChar(char input)
        {
            return SerializeString(input.ToString());
        }

        private static string SerializeString(string input)
        {
            input = ReplaceSpecialSymbols(input);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("\"");

            for (int i = 0; i < input.Length; i++)
            {
                stringBuilder.Append(input[i]);
            }

            stringBuilder.Append("\"");

            return stringBuilder.ToString();
        }

        private static string SerializeBool(bool input)
        {
            return input ? "true" : "false";
        }

        private static string SerializeInt(int input)
        {
            return input.ToString();
        }

        private static string SerializeDouble(double input)
        {
            return input.ToString().Replace(',', '.');
        }

        private static string SerializeDecimal(decimal input)
        {
            return input.ToString().Replace(',', '.');
        }

        private static string SerializeIEnumberable(IEnumerable<T> input)
        {
            StringBuilder result = new StringBuilder();
            result.Append("[");

            foreach (var item in input)
            {
                result.Append(SerializeFactory(item) + ",");
            }

            string toReturn = result.ToString().TrimEnd(',');
            toReturn += ']';

            return toReturn;
        }

        // Overloads for all other types of numbers...

        // Helpers
        private static string ReplaceSpecialSymbols(string input)
        {
            input = input.Replace("\\", @"\\");
            input = input.Replace("\t", @"\t");
            input = input.Replace("\n", @"\n");
            input = input.Replace("\b", @"\b");
            input = input.Replace("\f", @"\f");
            input = input.Replace("\a", @"\a");
            input = input.Replace("\r", @"\r");
            input = input.Replace("\v", @"\v");
            input = input.Replace("\"", @"\" + "\"");

            return input;
        }

        private static string SerializeFactory(object input)
        {
            string inputType = input.GetType().Name;
            string result = String.Empty;
            // Check if the input is some kind of collection i.e. List of lists etc...
            bool isICollection = input.GetType().GetInterface("ICollection") != null ? true : false;

            if (isICollection)
            {
                // Cast it to iterate over the collection
                IEnumerable<object> enumerable = (IEnumerable<object>)input;

                if (enumerable.Count() > 1)
                {
                    result += '[';
                }

                foreach (var item in enumerable)
                {
                    result += SerializeIEnumberable((IEnumerable<T>)item);
                    result += ',';
                }

                if (enumerable.Count() > 1)
                {
                    result = result.TrimEnd(',');
                    result += ']';
                }
            }
            else if (inputType == typeof(Int32).Name)
            {
                result = SerializeInt((int)input);
            }
            else if (inputType == typeof(decimal).Name)
            {
                result = SerializeDecimal((decimal)input);
            }
            else if (inputType == typeof(double).Name)
            {
                result = SerializeDouble((double)input);
            }
            else if (inputType == typeof(char).Name)
            {
                result = SerializeChar((char)input);
            }
            else if (inputType == typeof(bool).Name)
            {
                result = SerializeBool((bool)input);
            }
            else if (inputType == typeof(string).Name)
            {
                result = SerializeString((string)input);
            }
            else
            {
                // hmm?
            }

            return result;
        }
    }
}
