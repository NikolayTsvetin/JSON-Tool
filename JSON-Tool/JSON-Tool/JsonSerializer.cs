using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_Tool
{
    public static class JsonSerializer
    {
        public static string Serialize(char input)
        {
            return Serialize(input.ToString());
        }

        public static string Serialize(string input)
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

        public static string Serialize(object input)
        {
            return "TODO";
        }

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
    }
}
