using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace FileUpload
{
    public class Http
    {
        public static string GetResponse(string URL)
        {
            string value = string.Empty;

            HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;
            request.Method = "POST";

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var sessonId = JsonConvert.DeserializeObject<start>(result);
                value = sessonId.session_id;
            }
            httpResponse.Close();
            return value;
        }

        public static string CompleteResponse(string URL, string sessionId, string tagId)
        {
            string fileId = string.Empty;
            HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";

            complete input = new complete();
            input.session_id = sessionId;
            input.tags = new[] { tagId };
            string jsonData = JsonConvert.SerializeObject(input);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                result.Replace("nUPloading", "");
                var response = JsonConvert.DeserializeObject<completed>(result);
                fileId = response.file_id;
            }
            httpResponse.Close();
            return fileId;
        }
    }
}