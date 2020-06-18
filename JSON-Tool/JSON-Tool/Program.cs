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
            List<string> str = new List<string>() { "asd", "dsa", "sda" };
            object obj = new
            {
                x = 5,
                z = new int[3] { 0, 1, 2 },
                y = 6
            };
            List<List<object>> nested = new List<List<object>>()
            {
                //str,
                //str
                new List<object>() { obj },
                new List<object>() {obj}
            };
            Person p = new Person(22, "Nikolay");

            var mine = JsonSerializer.Serialize(nested);
            var original = JsonConvert.SerializeObject(nested);
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
