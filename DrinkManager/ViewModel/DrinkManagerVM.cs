using DrinkManager.DataSource;
using DrinkManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.ViewModel
{
    public class DrinkManagerVM : BaseVM
    {
        public ObservableCollection<DrinkVM> Drinks { get; private set; }

        public DrinkManagerVM()
        {
            this.Drinks = new ObservableCollection<DrinkVM>();
            DrinkDao dao = new DrinkDao();
            foreach(var drink in dao.GetAllDrinks())
            {
                this.Drinks.Add(new DrinkVM(drink));
            }
        }
    }
}
