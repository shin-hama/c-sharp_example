using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace ApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverUrl = "http://localhost:49299/CommandService";

            UploadFile(serverUrl);
        }

        static void UploadFile(string api)
        {
            string uri = $"{api}/File/Upload";
            string path = @"C:\Users\Public\Documents\JEOL\AutomationCenter\LamellaDetection\ModelTree\Flag\ab\counter.txt";

            string response = GetResponse($"{uri}?path={path}", "uploaded", "POST");
        }

        static void GetFile(string api)
        {
            string uri = $"{api}/File/Download";

            string path = @"C:\Users\Public\Documents\JEOL\AutomationCenter\LamellaDetection\ModelTree\Flag\ab\counter.txt";

            string response = GetResponse($"{uri}?path={path}", "", "GET");
            Console.WriteLine(response);
        }

        static void GetDirectories(string uri)
        {
            string root = @"C:\Users\Public\Documents\JEOL\AutomationCenter\LamellaDetection\ModelTree";

            string response = GetResponse($"{uri}?root={root}", "", "GET");
            JObject test = JObject.Parse(response);
            Console.WriteLine(test);
            Console.WriteLine(test.ContainsKey("dirs") & test["dirs"] is IEnumerable);
            foreach (string item in test["dirs"])
            {
                Console.WriteLine(item);
            }
        }

        static void MakeDirectory(string uri)
        {
            string dirPath = @"C:\Users\Public\Documents\JEOL\AutomationCenter\LamellaDetection\ModelTree\Flag\test";
            var jsonParameter = JsonConvert.SerializeObject(new
            {
                path = dirPath,
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
