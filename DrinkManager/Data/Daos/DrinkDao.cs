using DrinkManager.Data;
using DrinkManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.DataSource.Daos
{
    public class DrinkDao : BaseDao<Drink>
    {
        private static readonly string TABLE_NAME = "drinks";

        public DrinkDao() : base(TABLE_NAME)
        {

        }

        public List<Drink> GetAllDrinks()
        {
            return GetElements();
        }

        public void InsertDrink(Drink drink)
        {
            InsertElement(drink);
        }

        public void InsertDrinks(List<Drink> drinks)
        {
            InsertElements(drinks);
        }
    }
}
