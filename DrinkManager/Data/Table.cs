using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Data
{
    public class Table<T,V>
    {
        private readonly Dictionary<T, V> _data;
        private readonly string _filePath;
        private readonly string _primaryKey;

        public Table(string filePath, string primaryKey)
        {
            _filePath = filePath;
            _primaryKey = primaryKey;
            _data = ReadFromDisk().ToDictionary(v => GetKeyValue(v), v => v);
        }

        public List<V> GetElements()
        {
            return _data.Values.ToList();
        }

        public V GetElement(T key)
        {
            return _data[key];
        }

        public T UpdateElement(V newValue)
        {
            T key = GetKeyValue(newValue);
            _data[key] = newValue;
            return key;
        }

        private List<V> ReadFromDisk()
        {
            using (var stream = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<V>));
                return (List<V>)serializer.ReadObject(stream);
            }
        } 

        private T GetKeyValue(V value)
        {
            return (T)typeof(V).GetProperty(_primaryKey).GetValue(value);
        }
    }
}
