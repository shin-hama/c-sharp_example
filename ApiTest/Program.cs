using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace ApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverUrl = "http://localhost:49299/CommandService";

            ExecuteCommand(serverUrl);
        }

        static void GetProcess(string api)
        {
            string name = "testName";
            string uri = $"{api}/Processes/{name}";

            // Check no member with name
            // return {"process":"","status":"OK"}
            string response = GetResponse($"{uri}", "", "GET");
            Console.WriteLine(response);

            var res = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            Console.WriteLine(string.IsNullOrWhiteSpace(res["process"]));

            ExecuteCommand(api);

            // Check no member with name
            // return {"process":"{\"Name\":\"testName\",\"IsAsync\":true}","status":"OK"}
            response = GetResponse($"{uri}", "", "GET");
            Console.WriteLine(response);
            Console.WriteLine(JsonConvert.DeserializeObject(response));
        }

        static void ExecuteCommand(string api, string name = "")
        {
            string uri = $"{api}/Command/Execute";

            int num = 1;
            Task[] results = new Task[num];
            for (int i = 0; i < num; i++)
            {
                var command = $"python -m tkinter & echo {i} > D:/workspace/test_{i}.txt";
                string body = JsonConvert.SerializeObject(new
                {
                    Command = command,
                    Name = name,
                    IsAsync = true,
                });
                results[i] = Task.Run(() =>
                {
                    string response = GetResponse($"{uri}", body, "POST");
                    Console.WriteLine(response);
                    Console.WriteLine("Finish: " + i);
                });
            }

            Task.WaitAll(results);
        }

        static void UploadFile(string api)
        {
            string uri = $"{api}/File/Upload";
            string path = @"D:\workspace\test\ApiTest\Flag\ab\counter.txt";

            string response = GetResponse($"{uri}?path={path}", "uploaded", "POST");
            Console.WriteLine(response);
        }

        static void GetFile(string api)
        {
            string uri = $"{api}/File/Download";

            string path = @"D:\workspace\test\counter.zip";

            string response = GetResponse($"{uri}?path={path}", "", "GET");
            Console.WriteLine(response);
        }

        static void GetDirectories(string api)
        {
            string uri = $@"{api}\Directories";
            string root = @"D:\workspace\test\ApiTest\Flag\ab.test";

            string response = GetResponse($"{uri}?root={root}", "", "GET");
            JObject test = JObject.Parse(response);
            Console.WriteLine(test);
            Console.WriteLine(test.ContainsKey("dirs") & test["dirs"] is IEnumerable);
            foreach (string item in test["dirs"])
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="api"></param>
        static void MakeDirectory(string api)
        {
            string uri = $@"{api}\Directories";
            string dirPath = @"D:\workspace\test\ApiTest\Flag\ab.test";
            var jsonParameter = JsonConvert.SerializeObject(new
            {
                Path = dirPath,
            });

            string response = GetResponse(uri, jsonParameter, "POST");
            Console.WriteLine(response);

            Dictionary<string, string> jsonRes = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiUrl">Endpoint with query</param>
        /// <param name="jsonParameter">Body</param>
        /// <param name="method">GET / POST</param>
        /// <returns></returns>
        static string GetResponse(string apiUrl, string jsonParameter, string method)
        {
            string response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = method;
                request.ContentType = "application/json;";
                // カスタムヘッダーが必要の場合(認証トークンとか)
                request.Headers.Add("custom-api-param", "value");
                if (!string.IsNullOrWhiteSpace(jsonParameter))
                {
                    request.ContentLength = jsonParameter.Length;

                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(jsonParameter);
                    }
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();

                // HttpStatusCodeの判断が必要なら
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("API呼び出しに失敗しました。");
                }

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                // ex) response["status"]:"success"
            }
            catch (WebException wex)
            {
                // 200以外の場合
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            response = reader.ReadToEnd();
                        }
                    }
                }
            }

            return response;
        }
    }
}
