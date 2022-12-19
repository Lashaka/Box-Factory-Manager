using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Wpf_Class_Library_Logic.BoxOverall;
using Formatting = Newtonsoft.Json.Formatting;


namespace Class_Library___DBmanager
{
    public class DataBaseService
    {
        private static string _FilePath = @"DB files\json DB.json"; //set name, starts looking here anyway

        private string _jsonData = System.IO.File.ReadAllText(_FilePath); //read all text


        public DataBaseService() //checks if file exists, if not makes one for the first use
        {
            if (!File.Exists(_FilePath))
            {
                using (FileStream file = new FileStream(_FilePath, FileMode.Create))
                {
                    file.Close();
                }
            }
        }

        public void SaveData(object data) //save data to the json
        {
            string UpdatedList = ObjectToJson(data);
            WriteFile(UpdatedList);
        }

        private string ObjectToJson(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore
            });
        }
        private void WriteFile(string data)
        {
            using (StreamWriter writer = new StreamWriter(_FilePath))
            {
                writer.Write(data);
                writer.Dispose();
            }
        }

        public static SortedDictionary<string, Box_Storage> GetData()
        {
                string strConvert = File.ReadAllText(_FilePath);//change later to you't AbstractItem
                SortedDictionary<string, Box_Storage> data = JsonConvert.DeserializeObject<SortedDictionary<string, Box_Storage>>(strConvert, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                });
                return data;

        }

    }
}
