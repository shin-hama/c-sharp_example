using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JsonTraining
{
    public class JacModel
    {
        [JsonProperty("$TypeName")]
        public string TypeName { get; set; }

        [JsonProperty("Items")]
        public List<Object> Items { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonConstructor]
        public JacModel() { }
    }
}
