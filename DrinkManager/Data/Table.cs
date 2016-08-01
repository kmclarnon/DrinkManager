using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.Data
{
    public class Table<V>
    {
        private readonly Dictionary<int, V> _data;
        private readonly string _filePath;
        private readonly string _primaryKey;
        private readonly JsonSerializer _serializer;

        public Table(string filePath, string primaryKey)
        {
            _filePath = filePath;
            _primaryKey = primaryKey;
            _serializer = new JsonSerializer();
            _serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });

            _data = ReadFromDisk().ToDictionary(v => GetKeyValue(v), v => v);
        }

        public List<V> GetElements()
        {
            return _data.Values.ToList();
        }

        public V GetElement(int key)
        {
            return _data[key];
        }

        public int UpdateElement(V newValue)
        {
            int key = GetKeyValue(newValue);
            _data[key] = newValue;
            return key;
        }

        public void InsertElement(V newElement)
        {
            InsertElementInMemory(newElement);
            WriteToDisk();
        }

        public void InsertElements(List<V> newElements)
        {
            newElements.ForEach(v => InsertElementInMemory(v));
            WriteToDisk();
        }

        private int GetNextKey()
        {
            if (_data.Count == 0)
            {
                return 1;
            }

            return _data.Keys.Max() + 1;
        }

        private int InsertElementInMemory(V newElement)
        {
            int key = GetNextKey();
            SetKeyValue(newElement, key);
            _data.Add(key, newElement);
            return key;
        }
            
        private List<V> ReadFromDisk()
        {
            using (var stream = new FileStream(_filePath, FileMode.OpenOrCreate))
            using (var reader = new StreamReader(stream))
            {
                try
                {
                    List<V> results = (List<V>)_serializer.Deserialize(reader, typeof(List<V>));
                    if (results == null)
                    {
                        return new List<V>();
                    }

                    return results;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to read table file {0} : {1}", _filePath, e.Message);
                    return new List<V>();
                }
            }
        }
        
        private void WriteToDisk()
        {
            using (var stream = new FileStream(_filePath, FileMode.OpenOrCreate))
            using (var writer = new StreamWriter(stream))
            {
                _serializer.Serialize(writer, _data.Values.ToList());
            }
        } 

        private int GetKeyValue(V value)
        {
            return (int)typeof(V).GetProperty(_primaryKey).GetValue(value);
        }

        private void SetKeyValue(V value, int keyValue)
        {
            typeof(V).GetProperty(_primaryKey).SetValue(value, keyValue);
        }
    }
}
