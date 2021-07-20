using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace utility
{
    public class JsonSerialize
    {
        public static void SerializeIEnumerable(string path)
        {
            //CommandTable setting;
            var commands = new List<Table>();

            if (File.Exists(path))
            {
                var f = File.ReadAllText(path, Encoding.UTF8);
                commands = JsonConvert.DeserializeObject<List<Table>>(f);
            }

            Console.WriteLine(commands);
        }


        public static void DeserializeJsonFile()
        {
            string path = @"json\SmartViewStringConverterSetting.json";
            SmartViewStringConverterSetting json;
            try
            {
                json = JsonConvert.DeserializeObject<SmartViewStringConverterSetting>(File.ReadAllText(path, Encoding.UTF8));
            }
            catch
            {
                // ファイルが無いときは各プロパティを Null として初期化
                json = new SmartViewStringConverterSetting();
            }

            Console.WriteLine(json);
            Console.WriteLine(json.IsTest);
        }

        public static void SerializeToJsonFile()
        {
            string path = @"json\SmartViewStringConverterSetting.json";
            SmartViewStringConverterSetting json;
            try
            {
                json = JsonConvert.DeserializeObject<SmartViewStringConverterSetting>(File.ReadAllText(path, Encoding.UTF8));
            }
            catch
            {
                // ファイルが無いときは各プロパティを Null として初期化
                json = new SmartViewStringConverterSetting();
            }

            string output = @"json\output.json";
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented
            };
            using (StreamWriter sw = new StreamWriter(output, false, Encoding.UTF8))
            {
                serializer.Serialize(sw, json);
            }
        }

        public static void DeserializeEscapeSequence()
        {
            var test = "{\"command\": \"\\\"test test\\\"\"}";
            var _test = JsonConvert.DeserializeObject(test);

            Console.WriteLine(_test);

        }
    }

    public class Table
    {
        /// <summary>
        /// External用情報の取得
        /// </summary>
        /// <param name="name"></param>
        /// <param name="argument"></param>
        /// <param name="executePath"></param>
        [JsonConstructor]
        public Table(
            [JsonProperty("Name")] string name,
            [JsonProperty("ExecutePath")] string executePath,
            [JsonProperty("Argument")] IEnumerable<string> argument,
            [JsonProperty("Tag")] string tag)
        {
            this.Name = name;
            this.ExecutePath = executePath;
            this.Argument = argument;
            this.Tag = tag;
        }

        /// <summary>
        /// 実行コマンド名
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// コマンドプロンプトのファイル名前に挿入する文字列
        /// </summary>
        //public ObservableCollection<string> ArgsHeader { get; set; }
        [JsonProperty("Argument")]
        public IEnumerable<string> Argument { get; set; }

        /// <summary>
        /// コマンドプロンプトを実行するディレクトリ
        /// </summary>
        [JsonProperty("ExecutePath")]
        public string ExecutePath { get; set; }

        /// <summary>
        /// フリーワード
        /// </summary>
        [JsonProperty("Tag")]
        public string Tag { get; set; }
    }


    public class SmartViewStringConverterSetting
    {
        [JsonProperty("LearningServerShareDirRoot")]
        public StringConverterSetting LearningServerShareDirRoot { get; set; }


        [JsonProperty("IsTest")]
        public bool IsTest { get; set; }

        [JsonConstructor]
        public SmartViewStringConverterSetting(
            [JsonProperty("LearningServerShareDirRoot")] StringConverterSetting learningServerShareDirRoot,
            [JsonProperty("IsTest")] bool? isTest)
        {
            this.LearningServerShareDirRoot = learningServerShareDirRoot;
            // Set false if IsTest is not defined in file.
            this.IsTest = isTest ?? false;
        }

        public SmartViewStringConverterSetting() { }

        public override string ToString()
        {
            return $"LearningServerShareDirRoot: {LearningServerShareDirRoot}";
        }
    }

    public class ConfigBase
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonConstructor]
        public ConfigBase(
             [JsonProperty("from")] string from,
             [JsonProperty("to")] string to)
        {
            this.From = from;
            this.To = to;
        }

        public ConfigBase() { }

        public override string ToString()
        {
            return $"From: {From}, To: {To}";
        }
    }

    public class StringConverterSetting : ConfigBase { }

}
