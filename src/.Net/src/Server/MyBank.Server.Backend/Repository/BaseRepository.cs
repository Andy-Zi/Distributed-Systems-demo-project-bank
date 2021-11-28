using MyBank.Server.Backend.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity;
using System.Linq;

namespace MyBank.Server.Backend.Repository
{
    public abstract class BaseRepository<TType>
        where TType : IEntity
    {

        private string filePath => Path.Combine(ApplicationEnvironment.DataDirectory, filename);
        private readonly string filename;

        public object LockObject = new object();
        public ConcurrentDictionary<string, TType> Entities { get; private set; } = new ConcurrentDictionary<string, TType>();

        [Dependency] public  ApplicationEnvironment ApplicationEnvironment { get; set; }
        [Dependency] public Serializer Serializer { get; set; }

        public BaseRepository(string filename)
        {
            this.filename = filename;
        }

        public void Save()
        {
            lock (LockObject)
            {
                Directory.CreateDirectory(ApplicationEnvironment.DataDirectory);
                using (StreamWriter file = new StreamWriter(filePath))
                using (JsonWriter writer = new JsonTextWriter(file))
                {
                   Serializer.Serialize(writer,Entities.Values.ToList<TType>());
                }
            }
        }

        public void Load()
        {
            lock (LockObject)
            {
                if(!File.Exists(filePath))
                    return;

                Entities.Clear();
                using (StreamReader file = File.OpenText(filePath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    var items = Serializer.Deserialize<List<TType>>(reader);
                    foreach(var item in items)
                    {
                        Entities.TryAdd(item.GetMappingKey(), item);
                    }
                }
            }
        }
    }
}
