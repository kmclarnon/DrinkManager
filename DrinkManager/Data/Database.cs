using DrinkManager.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrinkManager.Data
{
    class Database
    {
        public static Database Instance { get { return LAZY_INSTANCE.Value; } }

        private static readonly Lazy<Database> LAZY_INSTANCE = new Lazy<Database>(() => new Database());
        private static readonly string DATABASE_FILENAME = "database.sqlite";

        private readonly string _dbLocation;

        private Database()
        {
            _dbLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DATABASE_FILENAME); 
            if (!File.Exists(_dbLocation))
            {
                SQLiteConnection.CreateFile(_dbLocation);
            }

            InitializeTables();
        }

        public IEnumerable<T> ExecuteQuery<T>(string query)
        {
            using (var con = new SQLiteConnection(GetConnectionString()))
            {
                con.Open();
                SQLiteDataReader reader = new SQLiteCommand(query, con).ExecuteReader();
                while (reader.Read())
                {
                    yield return GenerateObjectFromResultSet<T>(reader);
                }
            }
        }

        public void ExecuteCommand(string sqlCommand, params SQLiteParameter[] parameters)
        {
            ExecuteCommand(sqlCommand, parameters.ToList());
        }

        public void ExecuteCommand(string sqlCommand, List<SQLiteParameter> parameters)
        {
            using (var con = new SQLiteConnection(GetConnectionString()))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(sqlCommand, con);
                cmd.Parameters.AddRange(parameters.ToArray());
                cmd.ExecuteNonQuery();
            }
        }

        private void InitializeTables()
        {
            // create the ingredients table if it doesn't exist
            ExecuteCommand("CREATE TABLE IF NOT EXISTS ingredients ("
               + "id INTEGER PRIMARY KEY AUTOINCREMENT,"
               + "name TEXT"
               + ")");

            // create the drinks table if it doesn't exist
            ExecuteCommand("CREATE TABLE IF NOT EXISTS drinks ("
                + "id INTEGER PRIMARY KEY,"
                + "name TEXT,"
                + "category_class,"
                + "style TEXT,"
                + "source TEXT,"
                + "garnish TEXT,"
                + "glass TEXT,"
                + "directions TEXT,"
                + "comments TEXT,"
                + "rating TEXT,"
                + "date_added INTEGER,"
                + "date_tried INTEGER)");
        }

        private string GetConnectionString()
        {
            return "Data Source=" + _dbLocation + ";Version=3;Pooling=True;Max Pool Size=100;";
        }

        private T GenerateObjectFromResultSet<T>(SQLiteDataReader dataReader)
        {
            T result = Activator.CreateInstance<T>();
            foreach(var prop in typeof(T).GetProperties())
            {
                var field = prop.GetCustomAttribute<Field>();
                if (field == null)
                    continue;

                if (field.StoreAsJson)
                    throw new NotImplementedException("Json deserializing is not supported yet");

                Type fieldType = prop.PropertyType;
                object value = dataReader[field.Name];
                if (value == null)
                    throw new ResultMapperException(field.Name);

                prop.SetValue(result, value);
            }

            return result;
        }
   }
}
