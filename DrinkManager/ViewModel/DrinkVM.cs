using DrinkManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.ViewModel
{
    public class DrinkVM : BaseVM
    {
        public string Name
        {
            get { return _drink.Name; }
            set { SetProperty(_drink, value, nameof(_drink.Name)); }
        }

        public string Category
        {
            get { return _drink.CategoryClass; }
            set { SetProperty(_drink, value, nameof(_drink.CategoryClass)); }
        }

        public string Style
        {
            get { return _drink.Style; }
            set { SetProperty(_drink, value, nameof(_drink.Style)); }
        }

        private readonly Drink _drink;

        public DrinkVM(Drink drink)
        {
            _drink = drink;
        }
    }
}
