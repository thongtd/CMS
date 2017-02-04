using System.Collections.Generic;
using Newtonsoft.Json;

namespace CMS.Dashboard.Models
{
    public class KendoGridFilterModel
    {
        public int page { get; set; }

        public int pageSize { get; set; }

        public int skip { get; set; }

        public int take { get; set; }

        [JsonProperty("filters")]
        public IList<Filters> Filters { get; set; }

        [JsonProperty("logic")]
        public string Logic { get; set; }
    }

    public class Filters
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("logic")]
        public string Logic { get; set; }
    }
}