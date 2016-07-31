using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Data
{
    public abstract class BaseDao<T>
    {
        private readonly Database _database = Database.Instance;
        private readonly string tableName;

        protected BaseDao(string tableName)
        {
            this.tableName = tableName;
        }

        protected List<T> GetElements()
        {
            return _database.GetElements<T>(tableName);
        }
    }
}
