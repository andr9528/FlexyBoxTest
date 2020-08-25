using System.IO;
using Newtonsoft.Json;

namespace FlexyBoxTest.Utility
{
    public class PersistanceService
    {
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
    }
}