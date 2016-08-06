using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Data.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class Field : System.Attribute
    {
        public string Name { get; private set; }
        public bool StoreAsJson { get; set; } = false;

        public Field(string name)
        {
            this.Name = name;
        }
    }
}
