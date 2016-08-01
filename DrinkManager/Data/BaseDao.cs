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
        private readonly string _tableName;

        protected BaseDao(string tableName)
        {
            this._tableName = tableName;
        }

        protected List<T> GetElements()
        {
            return _database.GetElements<T>(_tableName);
        }

        protected void InsertElement(T element)
        {
            _database.InsertElement(_tableName, element);
        }

        protected void InsertElements(List<T> elements)
        {
            _database.InsertElements(_tableName, elements);
        }
    }
}
