using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload
{
    class Program
    {

        static void Main(string[] args)
        {
            string startURL = "https://private-anon-5967d52845-widenv2.apiary-mock.com/v2/uploads/chunks/start";
            string requestURL = "https://private-anon-5967d52845-widenv2.apiary-mock.com/v2/uploads/chunks/upload";
            string completeURL = "https://private-anon-5967d52845-widenv2.apiary-mock.com/v2/uploads/chunks/complete";
            string apiKey = "abc123456";
            string fileName = "C:/Users/Admin/Desktop/car.jpg";
            var sessionId = string.Empty;
            var tagId = string.Empty;
            var fileId = string.Empty;
            byte[] file = File.ReadAllBytes(fileName);
            string userAgent = "Nate";

            try
            {
                //start
                sessionId = Http.GetResponse(startURL);
                Console.WriteLine("session id = " + sessionId);
                Console.ReadLine();

                //upload
                Dictionary<string, object> postParameters = new Dictionary<string, object>();
                postParameters.Add("fileToUpload", new FormUpload.FileParameter(file));
                postParameters.Add("session_id", sessionId);
                postParameters.Add("chunk_number", 1);
                

                HttpWebResponse webResponse = FormUpload.MultipartFormPost(requestURL, userAgent, postParameters, "Authorization", "Bearer " + apiKey);

                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                var returnResponseText = responseReader.ReadToEnd();
                var tagIds = JsonConvert.DeserializeObject<upload>(returnResponseText);
                tagId = tagIds.tag;
                Console.WriteLine("tagId = " + tagId);
                Console.ReadLine();

                //complete
                fileId = Http.CompleteResponse(completeURL, sessionId, tagId);
                Console.WriteLine("file id = " + fileId);
                Console.ReadLine();

            }
            catch (Exception exp)
            {
                Console.WriteLine("error " + exp.Message);
                Console.WriteLine("details " + exp.InnerException);
                Console.ReadLine();
            }
        }
    }
}
