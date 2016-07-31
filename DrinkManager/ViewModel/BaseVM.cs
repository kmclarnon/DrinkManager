using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.ViewModel
{
    /// <summary>
    ///     Implementation of <see cref="INotifyPropertyChanged" /> to simplify models.
    /// </summary>
    public class BaseVM : INotifyPropertyChanged
    {
        /// <summary>
        ///     Multicast event for property change notification
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Checks if a property already matches a desired value.  Sets the property and
        ///     notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            // check if the value differs form what is currently stored
            if (Object.Equals(storage, value))
            {
                return false;
            }

            // value is different, update and notify listeners of PropertyChanged
            storage = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        ///     Checks if a property already matches a desired value.  Sets the named property on 
        ///     the provided storage object and notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Object that contains a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="storageProperty">Name of property on the storage object</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected bool SetProperty<T>(object storage, T value, string storageProperty, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            // preform reflection to retrieve the property on the storage object that we are interested in
            var prop = storage.GetType().GetProperty(propertyName);

            // check if the value differs form what is currently stored
            if (Object.Equals(prop.GetValue(storage), value))
            {
                return false;
            }

            // value is different, update and notify listeners of PropertyChanged
            prop.SetValue(storage, value);
            NotifyPropertyChanged(prop.Name);
            return true;
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            // Creating a local copy of PropertyChanged avoids a potential race condition, see 
            // http://stackoverflow.com/questions/4461865/pattern-for-implementing-inotifypropertychanged
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
