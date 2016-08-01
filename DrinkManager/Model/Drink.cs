using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Model
{
    [DataContract]
    public class Drink
    {
        [DataMember]
        public int? Id { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Style { get; set; }
    }
}
