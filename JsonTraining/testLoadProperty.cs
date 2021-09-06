using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JsonTraining
{
    public class testLoadProperty
    {
        [JsonProperty("Hoge")]
        public string Hoge { get; set; }

        [JsonProperty("Fuga", DefaultValueHandling = DefaultValueHandling.Populate)]
        public string Fuga { get; set; }

        [JsonConstructor]
        public testLoadProperty(
            [JsonProperty("hoge")] string hoge,
            [JsonProperty("fuga")] string fuga
        )
        {
            this.Hoge = hoge;
            this.Fuga = fuga ?? "default";
        }
    }
}
