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
using ExpressPrintingSystem.Model.Entities;

namespace ExpressPrintingSystem.Customer
{
    public partial class booking : System.Web.UI.Page
    {

        private const string CustomerID = "CU10010";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserCookie"] != null)
            {

                var Cookie = Request.Cookies["UserCookie"];

                if (Cookie.Values["UserInfo"] != null)
                {
                    string userString = ClassHashing.basicDecryption(Cookie.Values["UserInfo"].ToString());
                    User user = ExpressPrintingSystem.Model.Entities.User.toUserObject(userString);
                    txtcustomerID.Text = user.Name;
                }
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {


            //try
            //{
            if (FileUpload1.HasFile)

            {
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {

                        //upload to backblaze
                        String contentType = hpf.ContentType; //Type of file i.e. image/jpeg, audio/mpeg...
                        String getPath = Path.GetFileName(hpf.FileName);
                        hpf.SaveAs(Server.MapPath("~/File/") + getPath);//save to server side file
                        String fileName = hpf.FileName; //Desired name for the file
                        String filePath = Server.MapPath("~/File/") + getPath;//File path of desired upload
                        int size = FileUpload1.PostedFile.ContentLength;


                        string getFileIDInCloud = backblaze.UploadFile(contentType, filePath, fileName);
                        var numberOfPages = 0;
                        if (Path.GetExtension(hpf.FileName).Equals(".docx"))
                        {

                            // get the page number
                            var application = new Application();
                            var document = application.Documents.Open(filePath);//open document
                            numberOfPages = document.ComputeStatistics(WdStatistic.wdStatisticPages, false);
                            document.Close();///close document

                        }
                        else if (Path.GetExtension(hpf.FileName).Equals(".pdf")) {

                            FileStream fs = new FileStream( filePath, FileMode.Open, FileAccess.Read);
                            StreamReader r = new StreamReader(fs);
                            string pdfText = r.ReadToEnd();
                            Regex rx1 = new Regex(@"/Type\s*/Page[^s]");
                            MatchCollection matches = rx1.Matches(pdfText);
                            numberOfPages = Convert.ToInt32 (matches.Count.ToString());
                            fs.Close();
                        }

                        // upload to my sqldatabase
                        var uploadFileObject = (JObject)JsonConvert.DeserializeObject(getFileIDInCloud);
                        String FileIdInCloud = uploadFileObject["fileId"].Value<string>();//get file ID

                        UploadFileToDatabase(FileIdInCloud, numberOfPages, contentType, fileName, size, filePath);//pass to uploadfiledatabase
                        UploadRequestlistToDatabase();
                    }


                }
                Response.Redirect("~/Customer/Payment.aspx");
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


        strInsert = "Insert Into Document (DocumentName, DocumentType, FileIDInCloud, CustomerID, Size, PageNumber) Values (@DocumentName, @DocumentType, @FileIDInCloud, @CustomerID, @Size, @PageNumber);SELECT MAX(DocumentID) from Document where DocumentName=@DocumentName and DocumentType=@DocumentType";
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



                var getDocumentID = cmdInsert.ExecuteScalar();

                if (getDocumentID != null)
                {
                    string documentID = getDocumentID.ToString();

                    UploadDocumentDetailToDatabase(documentID);

                }


                int n = cmdInsert.ExecuteNonQuery();

                if (n > 0)
                {
                    Response.Write("<script>alert('Upload Successful');</script>");


                }
                else
                {
                    Response.Write("<script>alert('Upload Failed');</script>");
                }

                /*Close database connection*/


                conTaxi.Close();
            }
        }

        public void UploadDocumentDetailToDatabase(string DocumentIDid)
        {

            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;


            strInsert = "Insert Into Documentlist (RequestlistID, DocumentID, Sequences, DocumentColor, DocumentBothSide, DocumentPaperType, DocumentQuantity, DocumentDescription) Values (@RequestlistID, @DocumentID, @Sequences, @DocumentColor, @DocumentBothSide, @DocumentPaperType, @DocumentQuantity, @DocumentDescription)";
            cmdInsert = new SqlCommand(strInsert, conTaxi);


                cmdInsert.Parameters.AddWithValue("@RequestlistID", );
                cmdInsert.Parameters.AddWithValue("@DocumentID", DocumentIDid);
                cmdInsert.Parameters.AddWithValue("@Sequences", );
                cmdInsert.Parameters.AddWithValue("@DocumentColor", rbtDocumentColor.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@DocumentBothSide", rbtDocumentSide.SelectedValue);
                cmdInsert.Parameters.AddWithValue("@DocumentPaperType", ddlPaperType.SelectedItem);
                cmdInsert.Parameters.AddWithValue("@DocumentQuantity", txtDocumentQuantity.Text);
                cmdInsert.Parameters.AddWithValue("@DocumentDescription", txtDocumentDescription.Text);
       


            int n = cmdInsert.ExecuteNonQuery();

            if (n > 0)
            {
                Response.Write("<script>alert('Upload Successful');</script>");

              
            }
            else
            {
                Response.Write("<script>alert('Upload Failed');</script>");
            }

            /*Close database connection*/


            conTaxi.Close();
        }
        public void UploadRequestlistToDatabase()
        {

            string RequestStatus = "Pending";
            string RequestQuantity = "";
            string requestItemID = "";
            string requestID = "";

            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;


            strInsert = "Insert Into Documentlist (RequestID, RequestItemID, RequestQuantity, RequestStatus, RequestType) Values (@RequestID, @RequestItemID, @RequestQuantity, @RequestStatus, @RequestType)";
            cmdInsert = new SqlCommand(strInsert, conTaxi);


            cmdInsert.Parameters.AddWithValue("@RequestID", requestID);
            cmdInsert.Parameters.AddWithValue("@RequestItemID", requestItemID);
            cmdInsert.Parameters.AddWithValue("@RequestQuantity", RequestQuantity);
            cmdInsert.Parameters.AddWithValue("@RequestStatus", RequestStatus);
            cmdInsert.Parameters.AddWithValue("@RequestType", rbtRequestType.SelectedValue);



            int n = cmdInsert.ExecuteNonQuery();

            if (n > 0)
            {
                Response.Write("<script>alert('Upload Successful');</script>");

               
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





