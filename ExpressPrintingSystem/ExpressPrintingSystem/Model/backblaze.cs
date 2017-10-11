using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ExpressPrintingSystem.Model
{
    public class backblaze

    {
        private const string Account_ID = "82f988816cc5";
        private const string applicationKey = "001a56f5789027dcdd25f1e6810dbaa88ca5220f5b";
       

        public static string getAuthorization() {

            
            String accountId = "82f988816cc5"; //B2 Cloud Storage Account ID
            String applicationKey = "001a56f5789027dcdd25f1e6810dbaa88ca5220f5b"; //B2 Cloud Storage Application Key
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.backblazeb2.com/b2api/v1/b2_authorize_account");
            String credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(accountId + ":" + applicationKey));
            webRequest.Headers.Add("Authorization", "Basic " + credentials);
            webRequest.ContentType = "application/json; charset=utf-8";
            WebResponse response = (HttpWebResponse)webRequest.GetResponse();
            String json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();
            Console.WriteLine(json);

            //String json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return json;
        }

        public static String getUploadUrl()
        {
            String apiUrl = "https://api001.backblazeb2.com"; //Provided by b2_authorize_account 
            String accountAuthorizationToken = "3_20171010032419_a9f8f62842c9abf40ac614a5_9bfae7c6fea3170634d8e43ee249f3e887323f77_001_acct";//Provided by b2_authorize_account
            String bucketId = "9892df591858d8b156ec0c15"; //The unique bucket ID
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(apiUrl + "/b2api/v1/b2_get_upload_url");
            string body = "{\"bucketId\":\"" + bucketId + "\"}";
            var data = Encoding.UTF8.GetBytes(body);
            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization", accountAuthorizationToken);
            webRequest.ContentType = "application/json; charset=utf-8";
            webRequest.ContentLength = data.Length;
            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            WebResponse response = (HttpWebResponse)webRequest.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();

            return responseString;

        }


        

    }
}