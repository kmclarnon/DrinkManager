using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Data
{
    public class Database
    {
        private static readonly string DB_FOLDER_NAME = "data";
        private static readonly Lazy<Database> LAZY_INSTANCE = new Lazy<Database>(() => new Database());

        public static Database Instance { get { return LAZY_INSTANCE.Value; } }

        private readonly string _dbLocation;
        private readonly Dictionary<string, object> _tables;

        private Database()
        {
            _dbLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DB_FOLDER_NAME);
            if (!Directory.Exists(_dbLocation))
            {
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(_dbLocation);
                    Console.WriteLine("Created database directory {0} at {1}", di.FullName, Directory.GetCreationTime(_dbLocation));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create database directory {0}, {1}", _dbLocation, e.Message);
                    throw e;
                }
            }
            _tables = new Dictionary<string, object>();
        }

        public List<T> GetElements<T>(string tableName)
        {
            return GetTable<T>(tableName).GetElements();
        }

        public void InsertElement<T>(string tableName, T element)
        {
            GetTable<T>(tableName).InsertElement(element);
        }

        public void InsertElements<T>(string tableName, List<T> elements)
        {
            GetTable<T>(tableName).InsertElements(elements);
        }

        private Table<T> GetTable<T>(string tableName)
        {
            object result;
            if (!_tables.TryGetValue(tableName, out result))
            {
                result = new Table<T>(GetTableFile(tableName), "Id");
                _tables[tableName] = result;
            }

            return (Table<T>)result;
        }

        private string GetTableFile(string tableName)
        {
            return Path.Combine(_dbLocation, tableName + ".json");
        }
    }
}
