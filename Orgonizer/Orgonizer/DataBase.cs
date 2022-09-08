using System.IO;
using Newtonsoft.Json;

namespace Orgonizer
{
    public class DataBase
    {
        public static DataResorse DataResorse { get; set; }
        private static string path = Path.Combine(Directory.GetCurrentDirectory(), "base.json") ;
        public static void Load()
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using(StreamReader sr = new StreamReader(fs))
                    {
                        DataResorse = JsonConvert.DeserializeObject<DataResorse>(sr.ReadToEnd());
                    }                
                }
            }    
            else
                DataResorse = new DataResorse();
        }
        public static void Save()
        {
            if (File.Exists(path))
                File.Delete(path);
            var json = JsonConvert.SerializeObject(DataResorse);
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw= new StreamWriter(fs))
                    sw.WriteLine(json);
            }
        }
    }
}
