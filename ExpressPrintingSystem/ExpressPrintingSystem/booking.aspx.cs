using ExpressPrintingSystem.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;



namespace ExpressPrintingSystem.Customer
{
    public partial class booking : System.Web.UI.Page
    {

        private const string CustomerID = "CU10010";

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Cookies["UserCookie"] != null)
            //{

            //    var Cookie = Request.Cookies["UserCookie"];

            //    if (Cookie.Values["UserInfo"] != null)
            //    {
            //        string userString = ClassHashing.basicDecryption(Cookie.Values["UserInfo"].ToString());
            //        User user = ExpressPrintingSystem.Model.Entities.User.toUserObject(userString);
            //        txtcustomerID.Text = user.Name;
            //    }
            //}
        }



        protected void Button1_Click(object sender, EventArgs e)
        {


            //try
            //{
            if (FileUpload1.HasFile)

            {
                //upload to backblaze
                String contentType = FileUpload1.PostedFile.ContentType; //Type of file i.e. image/jpeg, audio/mpeg...
                String getPath = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/File/") + getPath);//save to server side file
                String fileName = FileUpload1.FileName; //Desired name for the file
                String filePath = Server.MapPath("~/File/") + getPath;//File path of desired upload
                int size = FileUpload1.PostedFile.ContentLength;


                string getFileIDInCloud = backblaze.UploadFile(contentType, filePath, fileName);

               


                // get the page number
                
                var application = new Application();
                var document = application.Documents.Open(filePath);//open document
               
                var numberOfPages = document.ComputeStatistics(WdStatistic.wdStatisticPages, false);
                document.Close();///close document

                // upload to my sqldatabase
                var uploadFileObject = (JObject)JsonConvert.DeserializeObject(getFileIDInCloud);
                String FileIdInCloud = uploadFileObject["fileId"].Value<string>();//get file ID

                UploadFileToDatabase(FileIdInCloud, numberOfPages, contentType, fileName, size, filePath);//pass to uploadfiledatabase
                


            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Please Upload a file First!!!')</script>");
            }
        }
        //catch (Exception ex) {
        //    throw new Exception(ex.ToString());
        //}



    

    public void UploadFileToDatabase(string fileid, int pageNumber, string contentType, string filename, int size, string path)
    {




        SqlConnection conTaxi;
        string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
        conTaxi = new SqlConnection(connStr);
        conTaxi.Open();

        string strInsert;
        SqlCommand cmdInsert;


        strInsert = "Insert Into Document (DocumentName, DocumentType, FileIDInCloud, CustomerID, Size, PageNumber) Values (@DocumentName, @DocumentType, @FileIDInCloud, @CustomerID, @Size, @PageNumber)";
        cmdInsert = new SqlCommand(strInsert, conTaxi);

        if (FileUpload1 != null)
        {


            cmdInsert.Parameters.AddWithValue("@DocumentName", filename);
            cmdInsert.Parameters.AddWithValue("@DocumentType", contentType);
            cmdInsert.Parameters.AddWithValue("@FileIDInCloud", fileid);
            cmdInsert.Parameters.AddWithValue("@CustomerID", CustomerID);
            cmdInsert.Parameters.AddWithValue("@Size", size);
            cmdInsert.Parameters.AddWithValue("@PageNumber", pageNumber);
                if (File.Exists(path))
                {
                    File.Delete(path);

                }

            }


            int n = cmdInsert.ExecuteNonQuery();

        if (n > 0)
        {
            Response.Write("<script>alert('Upload Successful');</script>");
             
            Response.Redirect("~/Customer/Payment.aspx");
        }
        else
        {
            Response.Write("<script>alert('Upload Failed');</script>");
        }

            /*Close database connection*/


            conTaxi.Close();

        }
}
}





