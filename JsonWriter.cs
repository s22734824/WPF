using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json; 
namespace huef
{
    class SaveModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    class JsonWriter
    {
        static Action FlushList;
        static bool First;
        List<SaveModel> T;
        SaveModel Model;
        
        public void SetValue(string value)
        {
            
        }
        public string GetValue()
        {
            return Model.Value;
        }

        public JsonWriter(string Name, string InitValue)
        {
            if (!First)
            {
                if (File.Exists("config.json"))
                {
                    
                }
            }



            //Model = new SaveModel() {Name = Name,Value = Value };
        }
        private void WriteAllObjectToFile()
        {
            
        }
        private void AddObjectToList()
        {

        }



    }
}
