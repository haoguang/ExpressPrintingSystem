﻿using ExpressPrintingSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace ExpressPrintingSystem.Customer
{
    public partial class booking : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

      

        protected void Button1_Click(object sender, EventArgs e)
        {
            //SqlConnection conTaxi;
            //string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            //conTaxi = new SqlConnection(connStr);
            //conTaxi.Open();

            //string strInsert;
            //SqlCommand cmdInsert;

            //strInsert = "Insert Into Document (DocumentName, DocumentType, CustomerID, Size) Values (@DocumentName, @DocumentType, @CustomerID, @Size)";
            //cmdInsert = new SqlCommand(strInsert, conTaxi);

            //if (FileUpload1 != null)
            //{
            //    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //    string contentType = FileUpload1.PostedFile.ContentType;
            //    int size = FileUpload1.PostedFile.ContentLength;

            //    cmdInsert.Parameters.AddWithValue("@DocumentName", filename);
            //    cmdInsert.Parameters.AddWithValue("@DocumentType", contentType);
            //    //cmdInsert.Parameters.AddWithValue("@DocumentURL", price.Text);
            //    cmdInsert.Parameters.AddWithValue("@CustomerID", txtcustomerID.Text);
            //    cmdInsert.Parameters.AddWithValue("@Size", size);

            //}


            //int n = cmdInsert.ExecuteNonQuery();

            //if (n > 0)
            //    Response.Write("<script>alert('login successful');</script>");
            //else
            //    Response.Write("<script>alert('failed');</script>");

            ///*Close database connection*/

            //conTaxi.Close();
            //Response.Redirect("Payment.aspx");

            String uploadUrl = "https://pod-000-1091-10.backblaze.com/b2api/v1/b2_upload_file/9892df591858d8b156ec0c15/c001_v0001091_t0022";
            String uploadAuthorizationToken = "3_20171010044654_c29d6f95ec2a61e8c17ad6ac_a4360d7db18de54c8f02118ba7ff9e4395890bb1_001_upld"; //Provided by b2_get_upload_url
            String contentType = FileUpload1.PostedFile.ContentType; //Type of file i.e. image/jpeg, audio/mpeg...
            String filePath = "C:\\Users\\lawre\\Desktop\\Resume1.docx"; //FileUpload1.PostedFile.FileName;//File path of desired upload
           // FileUpload1.SaveAs(filePath + Path.GetFileName(FileUpload1.FileName));
            String fileName = FileUpload1.FileName; //Desired name for the file
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
            Console.WriteLine(responseString);

        }
    }
}