using System.Net;
using System;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web;

namespace DrupalConnect
{
    public static class StringExtension
    {
        public static string FormatX(this string format, params object[] args)
        {
            return String.Format(format, args);
        }
    }
    


    public class DrupalConnector
    {
        private NetworkCredential credential;
        private WebClient client;
        private string sessionId;

        public DrupalConnector()
        {
            this.credential = new NetworkCredential();
            this.client = new WebClient();
            client.Credentials = credential;
        }

        private string Domain { get { return "http://bibliopedia.org/crawled_data"; } }

        public void Credentials(string username, string password)
        {
            credential.UserName = username;
            credential.Password = password;
            client.Credentials = credential;
            client.UseDefaultCredentials = false;
        }

        public string Data(Uri uri, string method, string postData = null)
        {
            var request = WebRequest.Create(uri);
            request.Credentials = this.credential;

            request.ContentType = "application/json";
            request.Method = method;

            var encoding = new ASCIIEncoding();
            if (postData != null)
            {
                var encodedPostData = postData;//HttpUtility.UrlEncode(postData);
                byte[] byte1 = encoding.GetBytes(encodedPostData);
                request.ContentLength = encodedPostData.Length;
                var newStream = request.GetRequestStream();
                newStream.Write(byte1, 0, byte1.Length);
                newStream.Close();
            }

            var response = request.GetResponse();
            var buffer = new byte[2048];
            response.GetResponseStream().Read(buffer, 0, 2048);
            var responseText = new String(encoding.GetChars(buffer));

            return responseText;
        }

        public string Get(Uri uri)
        {
            return Data(uri, "GET");
        }

        public string Post(Uri uri, string postData)
        {
            return Data(uri, "POST", postData);
        }

        public string EstablishSession()
        {
            var uri = new Uri(Domain+"/system/connect");

            var response = Post(uri, "bot_establishing=true");
            response = response.Substring(6);
            //var serializer = new Newtonsoft.Json.JsonSerializer();
            //var reader = new StringReader(response);
            //var jsonReader = new JsonTextReader(reader);
            //var result = serializer.Deserialize(jsonReader);
            var result = JObject.Parse(response);
            var sessid = result["sessid"];
            var sessid_typed = sessid.ToObject<string>();
            return sessid_typed;
        }

        public string Login()
        {
            var q = "\"";
            var uri = new Uri(Domain + "/user/login");
            var postString = "{" + q + "name" + q + ":" + q + this.credential.UserName + q;
            postString += "," + q + "password" + q + ":" + q + this.credential.Password + q;
            postString += "}";
            var response = Post(uri, postString);
            return response;
        }

        public string GetNode(string nid)
        {
            Login();

            var uri = new Uri("http://bibliopedia.org/crawled_data/node/{0}.jsonp".FormatX(nid));
            return client.DownloadString(uri);
        }
    }
}