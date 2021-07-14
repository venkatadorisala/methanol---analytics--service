using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JM.Integration.Methanol.Common.DBModel
{
    public class ReactorPeakTempKPIDataSet
    {

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("name")]
        public string Name;
    }
}
