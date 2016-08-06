using DrinkManager.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrinkManager.Data.Daos
{
    public class BaseDao
    {
        protected List<SQLiteParameter> GetParametersFromModel<T>(T model)
        {
            var parameters = new List<SQLiteParameter>();
            foreach (var prop in typeof(T).GetProperties())
            {
                var field = prop.GetCustomAttribute<Field>();
                if (field == null)
                    continue;

                parameters.Add(new SQLiteParameter(field.Name, GetValue(field, prop, model)));
            }

            return parameters;
        }

        private object GetValue(Field field, PropertyInfo prop, object model)
        {
            if (field.StoreAsJson)
            {
                throw new NotImplementedException();
            }
            else
            {
                return prop.GetValue(model);
            }
        }
    }
}
