using DrinkManager.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrinkManager.Data.Daos
{
    public class DrinkDao : BaseDao
    {
        private static readonly string INSERT_FIELDS = "name, category_class, style, source, garnish, glass, directions, comments, rating, date_added, date_tried";
        private static readonly string INSERT_PARAMS = "@name, @category_class, @style, @source, @garnish, @glass, @directions, @comments, @rating, @date_added, @date_tried"; 

        private readonly Database _database;
           
        public DrinkDao()
        {
            _database = Database.Instance;
        }

        public List<Drink> GetAllDrinks()
        {
            return _database.ExecuteQuery<Drink>("SELECT * FROM drinks").ToList();
        }

        public void InsertDrink(Drink drink)
        {
            _database.ExecuteCommand("INSERT INTO drinks (" + INSERT_FIELDS + ") values (" + INSERT_PARAMS + ")", GetParametersFromModel(drink));
        }

        public void InsertDrinks(List<Drink> drinks)
        {
            drinks.ForEach(d => InsertDrink(d));
        }
    }
}
