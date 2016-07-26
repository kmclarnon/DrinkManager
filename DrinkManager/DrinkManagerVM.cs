using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager
{
    public class DrinkManagerVM : BaseVM
    {
        public ObservableCollection<DrinkVM> Drinks { get; private set; }

        public DrinkManagerVM()
        {
            this.Drinks = new ObservableCollection<DrinkVM>();
            Drinks.Add(new DrinkVM("Daiquiri", "Rum", "Daiquiri"));
        }
    }
}
