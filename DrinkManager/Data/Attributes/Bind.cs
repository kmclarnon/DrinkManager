using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Data.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class Bind : Attribute
    {
        public String Name { get; private set; }

        public Bind(string name)
        {
            this.Name = name;
        }
    }
}
