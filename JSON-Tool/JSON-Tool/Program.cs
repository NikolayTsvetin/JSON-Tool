using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JSON_Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            var mine = JsonSerializer<int>.Serialize(list);
            var original = JsonConvert.SerializeObject(list);
            ;
        }
    }
}
