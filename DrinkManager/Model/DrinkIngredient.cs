using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Model
{
    public class DrinkIngredient
    {
        [JsonProperty]
        public int? Id { get; private set; }

        public int DrinkId { get; set; }
        public String Amount { get; set; }
        public String Unit { get; set; }
    }
}
