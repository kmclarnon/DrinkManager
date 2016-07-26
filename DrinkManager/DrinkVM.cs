using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager
{
    public class DrinkVM : BaseVM
    {
        private string _name;
        private string _category;
        private string _style;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    NotifyPropertyChanged("Category");
                }
            }
        }

        public string Style
        {
            get { return _style; }
            set
            {
                if (_style != value)
                {
                    _style = value;
                    NotifyPropertyChanged("Style");
                }
            }
        }

        public DrinkVM(string name, string category, string style)
        {
            this.Name = name;
            this.Category = category;
            this.Style = style;
        }
    }
}
