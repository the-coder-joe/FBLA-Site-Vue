using FBLA_Site.Server.Models;
using System.Text.Json;

namespace FBLA_Site.Server.Utils
{
    public class JsonUtils<T>
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        private readonly string basePath = "./ApplicationData/";

        private string path;

        public JsonUtils (string filename)
        {
            path = basePath + filename + ".json";
        }

        public T? GetData()
        {
            T? data;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                data = JsonSerializer.Deserialize<T>(json, options);
            }

            return data;
        }

        public void SetData(T data)
        {
            using (StreamWriter File1 = new(path))
            {
                string newJson = JsonSerializer.Serialize(data, options);
                File1.Write(newJson);
            }
        }

    }
}
