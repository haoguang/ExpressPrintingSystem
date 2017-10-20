using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ExpressPrintingSystem.Model.Messenging
{
    public class MessengerClass
    {
        private const string PAGE_ACCESS_TOKEN = "EAAEl88qBjxkBAHkkm6ZCk8NmjoXAjBDEOaGfopqfrTgSG4hyLrsCg7TK7jJGXZCcZCWRrDXu7HjQBoU4zIWoWtDwYOq1PfM1b5484JNOTbACNwwnZBQAmMM52nZC16VdaiVvIq52bA9ZAoWRgaclFDZCMbLOzJG6BOUpDJm6h7ZCRwZDZD";

        public static void test()
        {
            dynamic jsonObject = new JObject();

            //populate recipient detail
            dynamic recipient = new JObject();
            recipient.id = "100000841708248";

            //populate message detail
            dynamic message = new JObject();
            message.text = "your message here";

            jsonObject.recipient = recipient;
            jsonObject.message = message;

            string jsonString = JsonConvert.SerializeObject(jsonObject);//convert json object to string

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://graph.facebook.com/v2.6/me/messages?access_token=" + PAGE_ACCESS_TOKEN );
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonString);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse response = (HttpWebResponse)webRequest.GetResponse();
            String json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();
        }

    }
}