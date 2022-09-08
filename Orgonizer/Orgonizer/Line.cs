using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orgonizer
{
    public class Line
    {
        public Line() { }
        public Line(string data)
        {
            Data = data;
            Date = DateTime.Now;
        }
        private Line(string data, DateTime date) : this(data) => Date = date;
        [JsonProperty("Date")]
        public DateTime Date { get; private set; }
        [JsonProperty("Data")]
        public string Data { get; set; }     
    }
}
