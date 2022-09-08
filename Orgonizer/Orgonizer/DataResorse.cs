using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Orgonizer
{
    public class DataResorse
    {
        [JsonProperty("OrganizationLines")]
        public BindingList<Line> OrganizationLines = new BindingList<Line>();
        [JsonProperty("PhoneLines")]
        public BindingList<Line> PhoneLines = new BindingList<Line>();
        [JsonProperty("AdressLines")]
        public BindingList<Line> AdressLines = new BindingList<Line>();
        [JsonProperty("MeetingLines")]
        public BindingList<Line> MeetingLines = new BindingList<Line>();
        [JsonProperty("PeopleLines")]
        public BindingList<Line> PeopleLines = new BindingList<Line>();

    }
}
