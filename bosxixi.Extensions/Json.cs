using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace bosxixi.Extensions
{
    public static class Json
    {
        static public void SaveObjectToDiskAsJson(object obj, string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        static public T ReadJsonFileToObject<T>(T obj, string path)
        {
            string json = File.ReadAllText(path);
            return (T)JsonConvert.DeserializeObject(json, obj.GetType());
        }
    }
}
