using Newtonsoft.Json;
using System.Xml.Serialization;

namespace TXTRedactor
{
    public class saver
    {
        public static void save(string format, string path_new, List<open_text> listOrd)
        {
            switch (format)
            {
                case "xml":
                    SaveXML(path_new, listOrd);
                    break;
                case "json":
                    SaveJson(path_new, listOrd);
                    break;
                case "txt":
                    SaveText(path_new, listOrd);
                    break;
            }
        }
        public static void SaveText(string path_new, List<open_text> listOrd)
        {
            foreach (var line in listOrd)
            {
                File.AppendAllText(path_new, line.date + "\n");
                File.AppendAllText(path_new, line.text + "\n");
                File.AppendAllText(path_new, line.amount + "\n");
            }
        }
        public static void SaveXML(string path_new, List<open_text> listOrd)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<open_text>));
            using (FileStream fs = new FileStream(path_new, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, listOrd);
            }
        }
        public static void SaveJson(string path_new, List<open_text> listOrd)
        {
            string json = JsonConvert.SerializeObject(listOrd);
            File.WriteAllText(path_new, json);
        }
    }
}
