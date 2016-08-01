using CsvHelper;
using DrinkManager.DataSource;
using DrinkManager.DataSource.Daos;
using DrinkManager.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrinkManager.ViewModel
{
    public class DrinkManagerVM : BaseVM
    {
        public ICommand ImportCmd { get { return GetCommand(this.HandleImportCmd); } }
        public ObservableCollection<DrinkVM> Drinks { get; private set; }

        private readonly DrinkDao _dao = new DrinkDao();

        public DrinkManagerVM()
        {
            this.Drinks = new ObservableCollection<DrinkVM>();
            this.UpdateDrinks();
        }

        private void HandleImportCmd()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (!ofd.ShowDialog() == true)
            {
                return;
            }

            List<Drink> drinks = new List<Drink>();
            using (var reader = File.OpenText(ofd.FileName))
            {
                var csv = new CsvReader(reader);
                while (csv.Read())
                {
                    Drink newDrink = new Drink();
                    newDrink.Name = csv.GetField<string>("Name");
                    newDrink.Category = csv.GetField<string>("Category");
                    newDrink.Style = csv.GetField<string>("Style");
                    drinks.Add(newDrink);
                }
            }
            _dao.InsertDrinks(drinks);
            UpdateDrinks();
        }

        private void UpdateDrinks()
        {
            foreach (var drink in _dao.GetAllDrinks())
            {
                this.Drinks.Add(new DrinkVM(drink));
            }
        }
    }
}
