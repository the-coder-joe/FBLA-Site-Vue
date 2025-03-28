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

        private readonly String postingPath = "./ApplicationData/postings.json";

        private String path;

        public JsonUtils (String data)
        {  
            switch(data)
            {
                case "postings":
                    path = postingPath;
                    break;
                default: 
                    throw new Exception("Invalid data type");
            }

        }

        public T? GetData()
        {
            T? data;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                data = JsonSerializer.Deserialize<T>(json);
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
