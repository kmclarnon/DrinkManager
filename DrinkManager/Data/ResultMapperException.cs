using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Data
{
    class ResultMapperException : Exception
    {
        public ResultMapperException() { }

        public ResultMapperException(string field) 
            : base(String.Format("{0} field not found on result set", field)) { }
    }
}
