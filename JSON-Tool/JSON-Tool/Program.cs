﻿using System;
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
            var mine = JsonSerializer.Serialize('\'');
            var original = JsonConvert.SerializeObject('\'');
            ;
        }
    }
}
