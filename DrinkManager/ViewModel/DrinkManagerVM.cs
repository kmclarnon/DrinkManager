using CsvHelper;
using DrinkManager.Data.Daos;
using DrinkManager.Model;
using DrinkManager.Utils;
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

        private readonly DrinkDao _drinkDao = new DrinkDao();

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
                    try
                    {
                        Drink newDrink = new Drink();
                        newDrink.Name = csv.GetField<string>("Name");
                        newDrink.Source = csv.GetField<string>("Source");
                        newDrink.CategoryClass = csv.GetField<string>("Category Class");
                        newDrink.Style = csv.GetField<string>("Style");
                        newDrink.Source = csv.GetField<string>("Source");
                        newDrink.Garnish = csv.GetField<string>("Garnish");
                        newDrink.Glass = csv.GetField<string>("Glass");
                        newDrink.Directions = csv.GetField<string>("Directions");
                        newDrink.Comments = csv.GetField<string>("Comments");
                        newDrink.Rating = csv.GetField<string>("Rating");
                        newDrink.DateAdded = DateTime.FromOADate(csv.GetField<long>("Date Added")).ToEpochTime();
                        newDrink.DateTried = DateTime.FromOADate(csv.GetField<long>("Date Tried")).ToEpochTime();
                        drinks.Add(newDrink);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed to read row {0}", csv.Row);
                    }
                }
            }
            _drinkDao.InsertDrinks(drinks);
            UpdateDrinks();
        }

        private void UpdateDrinks()
        {
            foreach (var drink in _drinkDao.GetAllDrinks())
            {
                this.Drinks.Add(new DrinkVM(drink));
            }
        }
    }
}
