using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JsonTraining
{
    class Programs
    {

        static void Main(string[] args)
        {
            JsonConfig jc = new JsonConfig();
            jc.Load();
            jc.Save();
        }
    }

    class JsonConfig
    {
        JsonSerializerSettings m_Setting = new JsonSerializerSettings();
        string name = "";

        public List<Sample> Configurations { get; set; }

        public JsonConfig()
        {
            m_Setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            name = @"D:\workspace\test\C#\JsonTraining\sample.json";

            Configurations = new List<Sample>();
        }

        public class Sample
        {
            public Sample() { }
            public Sample(string name, int foo, int bar)
            {
                Name = name;
                Foo = foo;
                Bar = bar;
            }
            public string Name { get; set; }
            public int Foo { get; set; }
            public int Bar { get; set; }
        }

        public void Load()
        {
            using (var stream = new StreamReader(this.name))
            using (var jsonStream = new JsonTextReader(stream))
            {
                var serializer = JsonSerializer.Create(this.m_Setting);
                var obj = serializer.Deserialize<JsonConfig>(jsonStream);
                obj.Configurations.ForEach(o => Console.WriteLine(o.Name));
                Console.WriteLine(obj);
            }
        }
        public void Save()
        {
            string name = @"D:\workspace\test\C#\JsonTraining\output.json";
            this.setSample();
            using (var streamWriter = new StreamWriter(name))
            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                var serializer = JsonSerializer.Create(m_Setting);
                serializer.Serialize(jsonTextWriter, this);
            }
        }

        public void setSample()
        {
            this.Configurations.Add(new Sample("a", 1, 2));
            this.Configurations.Add(new Sample("b", 4, 6));
            this.Configurations.Add(new Sample("c", 5, 3));
        }
    }
}
