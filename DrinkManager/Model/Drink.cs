using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Model
{
    public class Drink
    {
        public int? Id { get; private set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Style { get; set; }
    }
}
