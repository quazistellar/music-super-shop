using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
        public static class Serializer
        {
            public static void Serialize<T>(T data, string file)
            {
                string json = JsonConvert.SerializeObject(data);
                File.WriteAllText(file, json);
            }

            public static T Deserialize<T>(string filePath)
            {
                string jsonString;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    jsonString = reader.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<T>(jsonString);
            }
    }
}
