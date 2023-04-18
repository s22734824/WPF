using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace huef
{
    class TxtWiter
    {
        static Dictionary<string, string> AllList;
        static bool First;
        static Action AddAllObjectToList;

        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                WriteAllObjectToFile();
            }
        }

        private string ItemName;
        string value;
        public TxtWiter(string Name,string Initvalue)
        {
            AddAllObjectToList += AddObjToDic;
            if (!First)
            {
                if (File.Exists("TxtWiter.txt"))
                {
                    AllList = new Dictionary<string, string>();
                    var fileStream = new FileStream("TxtWiter.txt", FileMode.Open, FileAccess.Read);
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            var rec = line.Split('=');
                            AllList.Add(rec[0], rec[1]);
                        }
                    }
                }
                First = true;
            }
            if (AllList.ContainsKey(Name))
            {
                value = AllList[Name];
            }
            else
            {
                value = Initvalue;
            }
            ItemName = Name;
        }
        public void WriteAllObjectToFile()
        {
            AllList = new Dictionary<string, string>();
            AddAllObjectToList();
            using (TextWriter writer = File.CreateText("TxtWiter.txt"))
            {
                foreach (KeyValuePair<string, string> Item in AllList)
                {
                    writer.WriteLine(Item.Key + "=" + Item.Value);
                }
            }
        }
        public void AddObjToDic()
        {
            AllList.Add(ItemName, value);
        }
    }
}
