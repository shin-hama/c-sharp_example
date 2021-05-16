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
}