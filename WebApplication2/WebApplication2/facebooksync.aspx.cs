using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Facebook;

namespace WebApplication2
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Dynamic;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Script.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public partial class facebooksync : System.Web.UI.Page
    {
        private const string app_secret = "dc479d12-32d3-4f12-a0ff-9bb37190e347";
        private const string api_key = "d334c1e7-d2f9-44b8-9390-9f64d53885d3";

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Form["signed_request"] != null)
            //{
            //    RegisterUser();
            //}
        }

        private void RegisterUser()
        {
            var app_secret = "0ee4a171fd7a298dd30792e0cc7774aa";
            var fb = new FacebookClient();
            dynamic signedRequest = fb.ParseSignedRequest(app_secret, this.Request.Params["signed_request"]);
            var name = signedRequest["registration"]["name"];
            var password = signedRequest["registration"]["password"];
            var email = signedRequest["registration"]["email"];
            var birthday = signedRequest["registration"]["birthday"];
            string gender = signedRequest["registration"]["gender"];
            gender = gender.Substring(0, 1).ToUpper();
            var country = 1;

            var url =
                string.Format(
                    "http://localhost:1794/gamecenteruserservice.svc/RegisterUser/?username={0}&password={1}&nickname={2}&email={3}&countryId={4}&Gender={5}&dateOfBirth={6}",
                    name,
                    password,
                    name,
                    email,
                    country,
                    gender,
                    birthday);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "POST";
                request.Timeout = 9000000;
                request.ContentLength = 0;
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string vals = reader.ReadToEnd();
                TextBox8.Text = vals;
            }

        }

        private void CheckAuthorization()
        {
            var app_id = "1402968979937332";
            var app_secret = "0ee4a171fd7a298dd30792e0cc7774aa";
            var scope = "publish_stream,manage_pages,offline_access,email";

            var fb = new FacebookClient();
            dynamic signedRequest = fb.ParseSignedRequest(app_secret, this.Request.Params["signed_request"]);

            if (Request["code"] == null)
            {
                Response.Redirect(
                    string.Format(
                        "https://graph.facebook.com/oauth/authorize?client id={0}&redirect_uri={1}&scope={2}",
                        app_id,
                        Request.Url.AbsoluteUri,
                        scope));
            }

            else
            {
                var tokens = new Dictionary<string, string>();
                var url =
                    string.Format(
                        "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                        app_id,
                        Request.Url.AbsoluteUri,
                        scope,
                        Request["code"].ToString(),
                        app_secret);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string vals = reader.ReadToEnd();

                    foreach (var val in vals.Split('&'))
                    {
                        tokens.Add(
                            val.Substring(0, val.IndexOf("=")),
                            val.Substring(val.IndexOf("=") + 1, val.Length - val.IndexOf("=") - 1));
                    }
                }

                string access_token = tokens["access_token"];
                var client = new FacebookClient(access_token);
                client.AccessToken = access_token;
                dynamic me = client.Get("me?fields=id,email");
                string name = me.name;
                string email = me.email;

                //client.Post("/me/feed", new { message = "No que no?.." });
            }
        }

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        protected void Button1_Click(object sender, EventArgs e)
        {
            //var url = string.Format("http://localhost:1794/GameCenterService.svc/RegisterUser/?countryId={0}&dateOfBirth={1}&email={2}&gender={3}&nickname={4}&password={5}&username={6}",
            //    txtCountry.Text, txtBirthday.Text, txtEmail.Text, txtGender.Text, txtNickname.Text, txtPassword.Text, txtUsername.Text);

            var url =
                string.Format(
                    "http://localhost:1794/DeveloperCenter.svc/RegisterUser/?companyId={0}&email={1}&password={2}&roleId={3}&username={4}",
                    txtCountry.Text,
                    txtEmail.Text,
                    txtPassword.Text,
                    txtGender.Text,
                    txtUsername.Text);
            //var message = url + api_key;
            //var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(app_secret));
            //hmacsha1.Initialize();
            //var hash = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(message));
            //var signature = Convert.ToBase64String(hash);

            //var cred = Encoding.UTF8.GetBytes("dc479d12-32d3-4f12-a0ff-9bb37190e347");
            //var encodeCred = Convert.ToBase64String(cred);

            var request = WebRequest.Create(url) as HttpWebRequest;
            if (request != null)
            {
                //request.Headers[HttpRequestHeader.Authorization] = encodeCred;
                //request.Headers.Add("signature", signature);
                request.Method = "POST";
                request.Timeout = 9000000;
                request.ContentLength = 0;
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                var vals = reader.ReadToEnd();
                TextBox8.Text = vals;
            }
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:2203/service1.svc/uploadimage";
            var byteImage = FileUpload1.FileBytes;
            var name = FileUpload1.FileName;
            var bs64image = Convert.ToBase64String(byteImage);
            var request = WebRequest.Create(url) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "POST";
                request.Timeout = 9000000;
                request.ContentType = "application/json";
                request.Accept = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"Files\":\"" + bs64image + "\", \"FileName\":\"" + name + "\" }";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
            }





            //var url = string.Format("http://localhost:4666/ElijaWebServices.svc/ImageResize/?url=https://dl.dropboxusercontent.com/s/8164l16kchtb2c4/DSC_2049.jpg");
            //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //if (request != null)
            //{
            //    request.Headers.Add("Token", "23581844-e951-47ae-bbe0-1ecb743da65c");
            //    request.Method = "GET";
            //    request.Timeout = 9000000;
            //    request.ContentType = "application/json";
            //    request.Accept = "application/json";
            //}

            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    StreamReader reader = new StreamReader(response.GetResponseStream());
            //    string vals = reader.ReadToEnd();

            //    JObject json = JObject.Parse(vals);

            //    string vals2 = (string)json["Content"];
            //    var bytearray = Convert.FromBase64String(vals2);

            //    //string vals2 = (string)json["UpdateProfileResult"]["Content"];
            //    //var bytearray = JsonConvert.DeserializeObject<byte[]>(vals2);

            //    Response.Buffer = true;
            //    Response.Charset = string.Empty;
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.ContentType = "image/jpg";
            //    Response.AddHeader("content-disposition", "attachment;filename="
            //    + "archivo_respaldo" + ".jpg");
            //    Response.BinaryWrite(bytearray);
            //    Response.Flush();
            //    Response.End();
            //    TextBox8.Text = vals;
            //}
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //var url = "http://localhost:1794/gamecenterservice.svc/UserLogin/?email=jccm_899@hotmail.com&password=VWF)Z=uZ-)?/";

            var url = "http://localhost:1794/developercenter.svc/UserLogin/?email=dev@mail.com&password=1234567890";
            var baseString = url + "69164b6e-9a4e-4bc9-b8bb-ed7fa8961589";
            var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes("150ab7ff-29b8-43a7-a908-e1bb94df5723"));
            var hash = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(baseString));
            var signature = Convert.ToBase64String(hash);

            var cred = Encoding.UTF8.GetBytes("150ab7ff-29b8-43a7-a908-e1bb94df5723");

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "POST";
                request.Headers.Add("signature", signature);
                request.Headers[HttpRequestHeader.Authorization] = Convert.ToBase64String(cred);
                request.Timeout = 9000000;
                request.ContentLength = 0;
            }



            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string vals = reader.ReadToEnd();
                JObject json = JObject.Parse(vals);
                string vals2 = (string)json["UserLoginResult"]["Content"];
                TextBox8.Text = vals2;
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:1794/gamecenteruserservice.svc/UserLogout";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "POST";
                request.Headers.Add("token", "61f5ec9b-8c83-450d-8b31-4ed0f088ea1f");
                request.Timeout = 9000000;
                request.ContentLength = 0;
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            //var url = "http://mgp-services.interalia.net/gamecenteruserservice.svc/getfriends";
            //var url = "http://localhost:1794/gamecenterservice.svc/getfriends/";
            var url = "http://mgp-services.interalia.net/gamecenterservice.svc/GetAllGames/";
            var message = url + api_key;
            var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(app_secret));
            var hash = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(message));
            var signature = Convert.ToBase64String(hash);

            var cred = Encoding.UTF8.GetBytes("dc479d12-32d3-4f12-a0ff-9bb37190e347");
            var credencial = Convert.ToBase64String(cred);
            

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "GET";
                request.Headers[HttpRequestHeader.Authorization] = Convert.ToBase64String(cred);
                request.Headers.Add("token", "1e684bdf-8d05-4f43-a678-ac66e0569252");
                request.Headers.Add("signature", signature);
                request.Timeout = 9000000;
                request.ContentLength = 0;
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string vals = reader.ReadToEnd();
                JObject json = JObject.Parse(vals);
                string vals2 = (string)json["SearchPeopleResult"]["Content"];
                TextBox8.Text = vals2;
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            //var url = "http://localhost:1794/gamecenterservice.svc/PasswordRecovery/?email=jccm_899@hotmail.com";
            var url = "http://localhost:1794/developercenter.svc/PasswordRecovery/?email=alexvr@hotmail.com";
            var message = url + "69164b6e-9a4e-4bc9-b8bb-ed7fa8961589";
            var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes("150ab7ff-29b8-43a7-a908-e1bb94df5723"));
            hmacsha1.Initialize();
            var hash = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(message));
            var signature = Convert.ToBase64String(hash);

            var cred = Encoding.UTF8.GetBytes("150ab7ff-29b8-43a7-a908-e1bb94df5723");

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "GET";
                request.Headers.Add("signature", signature);
                request.Headers[HttpRequestHeader.Authorization] = Convert.ToBase64String(cred);
                request.Timeout = 9000000;
                request.ContentLength = 0;
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string vals = reader.ReadToEnd();
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            var url =
                "http://localhost:1794/developercenter.svc/AddNewsletter/?description=Test Newsletter&publishEndDate=2013-11-29T16:20:00&publishStartDate=2013-11-26T16:20:00&title=Test";
            var message = url + "69164b6e-9a4e-4bc9-b8bb-ed7fa8961589";
            var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes("150ab7ff-29b8-43a7-a908-e1bb94df5723"));
            hmacsha1.Initialize();
            var hash = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(message));
            var signature = Convert.ToBase64String(hash);

            var cred = Encoding.UTF8.GetBytes("150ab7ff-29b8-43a7-a908-e1bb94df5723");

            byte[] contenido = null;
            if(FileUpload1.HasFile)
            {
                contenido = FileUpload1.FileBytes;
            }
            //var serializer = new JavaScriptSerializer();
            //serializer.MaxJsonLength = int.MaxValue;
            //var jsonRequestString = serializer.Serialize(contenido);
            //var bytes = Encoding.UTF8.GetBytes(jsonRequestString);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "POST";
                request.Timeout = 9000000;
                request.Headers.Add("signature", signature);
                request.Accept = "application/json";
                request.Headers.Add("token", "386DE602-0169-4B3F-879B-20B4F2C8B4E0");
                request.Headers[HttpRequestHeader.Authorization] = Convert.ToBase64String(cred);
                string content = Convert.ToBase64String(contenido);
                if (contenido != null)
                {
                    request.ContentLength = contenido.Length;
                    request.ContentType = "application/octet-stream";
                    var postStream = request.GetRequestStream();
                    postStream.Write(contenido, 0, contenido.Length);
                    postStream.Close();
                }
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string vals = reader.ReadToEnd();
                TextBox8.Text = vals;
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:1794/developercenter.svc/GetCurrentNewsletters/";
            var message = url + "69164b6e-9a4e-4bc9-b8bb-ed7fa8961589";
            var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes("150ab7ff-29b8-43a7-a908-e1bb94df5723"));
            hmacsha1.Initialize();
            var hash = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(message));
            var signature = Convert.ToBase64String(hash);

            var cred = Encoding.UTF8.GetBytes("150ab7ff-29b8-43a7-a908-e1bb94df5723");

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request != null)
            {
                request.Method = "GET";
                request.Headers.Add("signature", signature);
                request.Headers[HttpRequestHeader.Authorization] = Convert.ToBase64String(cred);
                request.Headers.Add("token", "3BD4FCDD-4EC1-4F4F-93DA-660C97C6B645");
                request.Timeout = 9000000;
                request.ContentLength = 0;
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string vals = reader.ReadToEnd();
                TextBox8.Text = vals;
            }
        }
    }
}