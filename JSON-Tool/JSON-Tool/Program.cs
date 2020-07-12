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
            string str = "asd\n\"as\\d";
            object obj = new
            {
                z = 's',
                x = 5,
                y = "asd"
            };
            List<object> list = new List<object>() { obj, obj};
            var test = JsonConvert.SerializeObject(list);

            // to dictionary helper method???
            var original = JsonConvert.DeserializeObject(test);
            /*Dictionary<string, object>*/object mine = JsonParser<object>.Parse(test);
            ;
            // extension to object...
            var nz = JsonParser<object>.ToDictionary(mine);
        }
    }
}
