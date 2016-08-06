using DrinkManager.Data;
using DrinkManager.Data.Attributes;
using Newtonsoft.Json;
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
        [Field("id")]
        public long Id { get; private set; }

        [Field("name")]
        public string Name { get; set; }
        
        [Field("category_class")]
        public string CategoryClass { get; set; }

        [Field("style")]
        public string Style { get; set; }

        [Field("source")]
        public string Source { get; set; }

        [Field("garnish")]
        public string Garnish { get; set; }

        [Field("glass")]
        public string Glass { get; set; }

        [Field("directions")]
        public string Directions { get; set; }

        [Field("comments")]
        public string Comments { get; set; }
        
        [Field("rating")]
        public string Rating { get; set; }
        
        [Field("date_added")]
        public long DateAdded { get; set; }

        [Field("date_tried")]
        public long DateTried { get; set; }
    }
}
