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
            object obj = new { x = 5, y = 6 };
            List<List<string>> nested = new List<List<string>>()
            {
                new List<string>() {"Helo", "halo" },
                new List<string>() {"koi dali poznavam" }
            };
            Person p = new Person(22, "Nikolay");

            var mine = JsonSerializer<string>.Serialize(p);
            var original = JsonConvert.SerializeObject(p);
            ;
        }
    }

    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
        //public string Secret { get; set; }

        public Person(int age, string name)
        {
            Age = age;
            Name = name;
        }
    }
}
