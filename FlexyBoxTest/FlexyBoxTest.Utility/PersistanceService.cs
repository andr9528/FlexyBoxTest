using System.IO;
using Newtonsoft.Json;

namespace FlexyBoxTest.Utility
{
    public class PersistanceService
    {
        // Task 3.3
        /// <summary>
        /// Creates the file on the specified path, if it doesn't already exist.
        /// Takes the input and converts it to a Json string,
        /// Writes the Json string to the file, overwriting any prior text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public void SaveToJson<T>(string path, T data)
        {
            if (!File.Exists(path))
            {
                var dis = File.Create(path);
                dis.Dispose();
            }

            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, json);
        }

        public string GetJsonStringFromFile(string path)
        {
            var output = File.ReadAllText(path);
            return output;
        }
    }
}