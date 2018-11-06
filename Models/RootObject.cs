using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestePratico.Models
{
    public class RootObject
    {
            public int total_count { get; set; }
            public bool incomplete_results { get; set; }
            public List<Repositorios> items { get; set; }
        }
}
