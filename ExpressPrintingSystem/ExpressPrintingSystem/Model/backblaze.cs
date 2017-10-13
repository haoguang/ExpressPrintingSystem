using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private const string ACCOUNT_ID = "82f988816cc5";
        private const string APPLICATION_KEY = "001a56f5789027dcdd25f1e6810dbaa88ca5220f5b";
        private const string BUCKET_ID = "9892df591858d8b156ec0c15";
       

        public static string getAuthorization() {

            
            String accountId = ACCOUNT_ID; //B2 Cloud Storage Account ID
            String applicationKey = APPLICATION_KEY; //B2 Cloud Storage Application Key
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.backblazeb2.com/b2api/v1/b2_authorize_account");
            String credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(accountId + ":" + applicationKey));
            webRequest.Headers.Add("Authorization", "Basic " + credentials);
            webRequest.ContentType = "application/json; charset=utf-8";
            WebResponse response = (HttpWebResponse)webRequest.GetResponse();
            String json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();

            return json;
        }

        public static String getUploadUrl()
        {
            string authorizationString = getAuthorization(); // get any information for authorization;
            var authorizationObject = (JObject)JsonConvert.DeserializeObject(authorizationString);

            String apiUrl = authorizationObject["apiUrl"].Value<string>(); //Provided by b2_authorize_account 
            String accountAuthorizationToken = authorizationObject["authorizationToken"].Value<string>();//Provided by b2_authorize_account
            String bucketId = BUCKET_ID; //The unique bucket ID
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


        public static string UploadFile(string contentType, string filePath, string fileName) {

            String uploadUrlInfo = getUploadUrl();//get any information to upload file.
            var uploadUrlObject = (JObject)JsonConvert.DeserializeObject(uploadUrlInfo);

            String uploadUrl = uploadUrlObject["uploadUrl"].Value<string>();
            String uploadAuthorizationToken = uploadUrlObject["authorizationToken"].Value<string>(); //Provided by b2_get_upload_url
            String sha1Str = "SHA_1"; //Sha1 verification for the file

            // Read the file into memory and take a sha1 of the data.
            FileInfo fileInfo = new FileInfo(filePath);
            byte[] bytes = File.ReadAllBytes(filePath);
            SHA1 sha1 = SHA1.Create();
            // NOTE: Loss of precision. You may need to change this code if the file size is larger than 32-bits.
            byte[] hashData = sha1.ComputeHash(bytes, 0, (int)fileInfo.Length);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashData)
            {
                sb.Append(b.ToString("x2"));
            }
            sha1Str = sb.ToString();

            // Send over the wire
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uploadUrl);
            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization", uploadAuthorizationToken);
            webRequest.Headers.Add("X-Bz-File-Name", fileName);
            webRequest.Headers.Add("X-Bz-Content-Sha1", sha1Str);
            webRequest.ContentType = contentType;
            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
            WebResponse response = (HttpWebResponse)webRequest.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();

            return responseString;
        }

        public static byte[] downloadFileIntoBytes(string fileId)
        {
            string authorizationString = getAuthorization(); // get any information for authorization;
            var authorizationObject = (JObject)JsonConvert.DeserializeObject(authorizationString);

            string downloadUrl = authorizationObject["downloadUrl"].Value<string>(); // Provided by b2_authorize_account

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(downloadUrl + "/b2api/v1/b2_download_file_by_id");
            string body = "{\"fileId\":\"" + fileId + "\"}";
            var data = Encoding.UTF8.GetBytes(body);
            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization", authorizationObject["authorizationToken"].Value<string>());
            webRequest.ContentType = "application/json; charset=utf-8";
            webRequest.ContentLength = data.Length;
            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            WebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Stream responseStream = response.GetResponseStream();
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(responseStream))
            {
                fileBytes = br.ReadBytes(500000);
                br.Close();
            }
            responseStream.Close();
            response.Close();

            return fileBytes;
            
        }

        public static void downloadFileToFile(string fileDir, string fileId)
        {
            String downloadsFolder = @"FILE DIRECTORY HERE";
            Byte[] fileBytes = downloadFileIntoBytes(fileId);
            FileStream saveFile = new FileStream(downloadsFolder, FileMode.Create);
            BinaryWriter writeFile = new BinaryWriter(saveFile);
            try
            {
                writeFile.Write(fileBytes);
            }
            finally
            {
                saveFile.Close();
                writeFile.Close();
            }
        }
        

    }
}