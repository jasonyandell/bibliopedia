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
        private static string NOT_INITIALIZED = Guid.NewGuid().ToString();

        private NetworkCredential credential;
        private WebClient client;
        private string _sessionId;
        private string _session_name;

        public DrupalConnector()
        {
            this.credential = new NetworkCredential();
            this.client = new WebClient();
            client.Credentials = credential;
            _sessionId = NOT_INITIALIZED;
            _session_name = NOT_INITIALIZED;
        }

        private string Domain { get { return "http://bibliopedia.org/crawled_data"; } }

        public void Credentials(string username, string password)
        {
            credential.UserName = username;
            credential.Password = password;
            client.Credentials = credential;
            client.UseDefaultCredentials = false;
        }

        public JObject Data(Uri uri, string method, string postData = null)
        {
            var request = HttpWebRequest.Create(uri);
            request.Credentials = this.credential;

            request.ContentType = "application/json";
            request.Method = method;
            if (_sessionId != NOT_INITIALIZED)
            {
                request.Headers.Add("sessid",_sessionId);
            }

            var encoding = new ASCIIEncoding();
            if (postData != null)
            {
                var encodedPostData = postData;
                byte[] byte1 = encoding.GetBytes(encodedPostData);
                request.ContentLength = encodedPostData.Length;
                var newStream = request.GetRequestStream();
                newStream.Write(byte1, 0, byte1.Length);
                newStream.Close();
            }

            //var response = request.GetResponse();
            //var responseStream = response.GetResponseStream();

            var response = request.GetResponse();
            byte[] buffer;
            var responseStream = response.GetResponseStream();
            if (responseStream == null) throw new HttpRequestValidationException("Request generated null response");

            var thisBlock = "";
            var responseText = "";
            buffer = new byte[2048];
            while (responseStream.Read(buffer, 0, 2048) > 0)
            {
                thisBlock = new String(encoding.GetChars(buffer));
                responseText += thisBlock;
                buffer = new byte[2048];
            }
            responseText = responseText.Substring(responseText.IndexOf('{'));

            var jobj = JObject.Parse(responseText);

            return jobj;
        }

        public JObject Get(Uri uri)
        {
            return Data(uri, "GET");
        }

        public JObject Post(Uri uri, string postData)
        {
            return Data(uri, "POST", postData);
        }

        public string EstablishSession()
        {
            var uri = new Uri(Domain+"/system/connect");

            var result = Post(uri, "bot_establishing=true");
            var sessid = result["sessid"];
            var sessid_typed = sessid.ToObject<string>();
            return sessid_typed;
        }

        public string Login()
        {
            if (_sessionId != NOT_INITIALIZED) return _sessionId;

            var q = "\"";
            var uri = new Uri(Domain + "/user/login");
            var postString = "{" + q + "username" + q + ":" + q + this.credential.UserName + q;
            postString += "," + q + "password" + q + ":" + q + this.credential.Password + q;
            postString += "}";
            var result = Post(uri, postString);

            this._sessionId = result["sessid"].ToString();
            this._session_name = result["session_name"].ToString();

            return _sessionId;
        }

        public JObject GetNode(string nid)
        {
            Login();

            var uri = new Uri("http://bibliopedia.org/crawled_data/node/{0}.jsonp".FormatX(nid));
            var result = Get(uri);
            return result;
        }
    }
}