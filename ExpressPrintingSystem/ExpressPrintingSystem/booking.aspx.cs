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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserCookie"] != null)
            {

                var Cookie = Request.Cookies["UserCookie"];

                if (Cookie.Values["UserInfo"] != null)
                {
                    string userString = ClassHashing.basicDecryption(Cookie.Values["UserInfo"].ToString());
                    User user = ExpressPrintingSystem.Model.Entities.User.toUserObject(userString);
                    txtcustomerID.Text = user.ID;
                    ViewState["UserID"] = ClassHashing.basicEncryption(user.ID);
                }
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {


            //try
            //{
            //if (FileUpload1.HasFile)

            //{
            //    HttpFileCollection hfc = Request.Files;
            //    for (int i = 0; i < hfc.Count; i++)
            //    {
            //        HttpPostedFile hpf = hfc[i];
            //        if (hpf.ContentLength > 0)
            //        {

            //            //upload to backblaze
            //            String contentType = hpf.ContentType; //Type of file i.e. image/jpeg, audio/mpeg...
            //            String getPath = Path.GetFileName(hpf.FileName);
            //            hpf.SaveAs(Server.MapPath("~/File/") + getPath);//save to server side file
            //            String fileName = hpf.FileName; //Desired name for the file
            //            String filePath = Server.MapPath("~/File/") + getPath;//File path of desired upload
            //            int size = FileUpload1.PostedFile.ContentLength;


            //            string getFileIDInCloud = backblaze.UploadFile(contentType, filePath, fileName);
            //            var numberOfPages = 0;
            //            if (Path.GetExtension(hpf.FileName).Equals(".docx"))
            //            {

            //                // get the page number
            //                var application = new Application();
            //                var document = application.Documents.Open(filePath);//open document
            //                numberOfPages = document.ComputeStatistics(WdStatistic.wdStatisticPages, false);
            //                document.Close();///close document

            //            }
            //            else if (Path.GetExtension(hpf.FileName).Equals(".pdf"))
            //            {

            //                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            //                StreamReader r = new StreamReader(fs);
            //                string pdfText = r.ReadToEnd();
            //                Regex rx1 = new Regex(@"/Type\s*/Page[^s]");
            //                MatchCollection matches = rx1.Matches(pdfText);
            //                numberOfPages = Convert.ToInt32(matches.Count.ToString());
            //                fs.Close();
            //            }

            //            // upload to my sqldatabase
            //            var uploadFileObject = (JObject)JsonConvert.DeserializeObject(getFileIDInCloud);
            //            String FileIdInCloud = uploadFileObject["fileId"].Value<string>();//get file ID

            //            UploadFileToDatabase(FileIdInCloud, numberOfPages, contentType, fileName, size, filePath);//pass to uploadfiledatabase
            //            UploadRequestlistToDatabase();
            //        }


            //    }
            //    Response.Redirect("~/Customer/Payment.aspx");
            //}
            //else
            //{
            //    Response.Write("<script LANGUAGE='JavaScript' >alert('Please Upload a file First!!!')</script>");
            //}

            Request request = PopulateDataToObject();
            List<Documentlist> zxcdocumentList = createDocumentList();

            Session["request"] = request;
            Response.Redirect("~/Customer/Payment.aspx");

         // UploadFileToDatabase(request.RequestLists[0].DocumentList);
           

        }
        //catch (Exception ex) {
        //    throw new Exception(ex.ToString());
        //}





        public void UploadFileToDatabase(Model.Entities.Document newdocument)
        {


           

            //for (int i = 0; i < newdocument.Count; i++)
            //{
            //   string  dfd = newdocument[i] as string;
            //    if () 

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


                    cmdInsert.Parameters.AddWithValue("@DocumentName", newdocument.DocumentName);
                    cmdInsert.Parameters.AddWithValue("@DocumentType", newdocument.DocumentType);
                    cmdInsert.Parameters.AddWithValue("@FileIDInCloud", newdocument.FileIDInCloud);
                    cmdInsert.Parameters.AddWithValue("@CustomerID", newdocument.CustomerID);
                    cmdInsert.Parameters.AddWithValue("@Size", newdocument.Size);
                    cmdInsert.Parameters.AddWithValue("@PageNumber", newdocument.PageNumber);




                    var getDocumentID = cmdInsert.ExecuteScalar();

                    if (getDocumentID != null)
                    {

                        newdocument.DocumentID = getDocumentID.ToString();



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

        public void UploadDocumentDetailToDatabase(Model.Entities.Document databasedocument, Model.Entities.Documentlist databasedocumentlist, Model.Entities.Requestlist databaseRequestlist)
        {


         
           


            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;


            strInsert = "Insert Into Documentlist (RequestlistID, DocumentID, Sequences, DocumentColor, DocumentBothSide, DocumentPaperType, DocumentQuantity, DocumentDescription) Values (@RequestlistID, @DocumentID, @Sequences, @DocumentColor, @DocumentBothSide, @DocumentPaperType, @DocumentQuantity, @DocumentDescription)";
            cmdInsert = new SqlCommand(strInsert, conTaxi);


            cmdInsert.Parameters.AddWithValue("@RequestlistID", databaseRequestlist.RequestlistID);
            cmdInsert.Parameters.AddWithValue("@DocumentID", databasedocument.DocumentID);
            cmdInsert.Parameters.AddWithValue("@Sequences", databasedocumentlist.Sequences);
            cmdInsert.Parameters.AddWithValue("@DocumentColor", databasedocumentlist.DocumentColor);
            cmdInsert.Parameters.AddWithValue("@DocumentBothSide", databasedocumentlist.DocumentBothSide);
            cmdInsert.Parameters.AddWithValue("@DocumentPaperType", databasedocumentlist.DocumentPaperType);
            cmdInsert.Parameters.AddWithValue("@DocumentQuantity", databasedocumentlist.DocumentQuantity);
            cmdInsert.Parameters.AddWithValue("@DocumentDescription", databasedocumentlist.DocumentDescription);




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
        public void UploadRequestlistToDatabase(Model.Entities.Requestlist databaserequestlist, Model.Entities.Request request)
        {




            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;


            strInsert = "Insert Into Requestlist (RequestID, RequestItemID, RequestStatus, RequestType) Values (@RequestID, @RequestItemID, @RequestQuantity, @RequestStatus, @RequestType);SELECT MAX(RequestlistID) from Requestlist where RequestStatus=@RequestStatus";
            cmdInsert = new SqlCommand(strInsert, conTaxi);


            cmdInsert.Parameters.AddWithValue("@RequestID", request.RequestID);
            cmdInsert.Parameters.AddWithValue("@RequestItemID", databaserequestlist.RequestItemID);
            cmdInsert.Parameters.AddWithValue("@RequestStatus", databaserequestlist.RequestStatus);
            cmdInsert.Parameters.AddWithValue("@RequestType", databaserequestlist.RequestType);

            var getRequestlistID = cmdInsert.ExecuteScalar();

            if (getRequestlistID != null)
            {


                databaserequestlist.RequestlistID = getRequestlistID.ToString();



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


        private List<Documentlist> createDocumentList()
        {

            string userID = ClassHashing.basicDecryption((string)ViewState["UserID"]);

            //create document list and upload file to cloud
            List<Documentlist> documentList = new List<Documentlist>();


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
                        else if (Path.GetExtension(hpf.FileName).Equals(".pdf"))
                        {

                            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                            StreamReader r = new StreamReader(fs);
                            string pdfText = r.ReadToEnd();
                            Regex rx1 = new Regex(@"/Type\s*/Page[^s]");
                            MatchCollection matches = rx1.Matches(pdfText);
                            numberOfPages = Convert.ToInt32(matches.Count.ToString());
                            fs.Close();
                        }

                        // upload to my sqldatabase
                        var uploadFileObject = (JObject)JsonConvert.DeserializeObject(getFileIDInCloud);
                        String FileIdInCloud = uploadFileObject["fileId"].Value<string>();//get file ID

                        Model.Entities.Document newdocument = new Model.Entities.Document(fileName, contentType, FileIdInCloud, userID, size, numberOfPages);

                        int sequences = 0; ////remember to do it;  
                        string documentColor = rbtDocumentColor.SelectedValue;
                        string documentbothside = rbtDocumentSide.SelectedValue;
                        int documentpapertype = Convert.ToInt32(ddlPaperType.SelectedItem.ToString());
                        int documentquantity = Convert.ToInt32(txtDocumentQuantity.Text);
                        string documentdescription = txtDocumentDescription.Text;

                        documentList.Add(new Documentlist(newdocument, sequences, documentColor, documentbothside, documentpapertype, documentquantity, documentdescription));

                    }


                }
            }

            return documentList;

        }

        private Request PopulateDataToObject()
        {
            List<Documentlist> documentlist = createDocumentList();

            List<Requestlist> requestlist = new List<Requestlist>();

            Requestlist newRequestlist = new Requestlist(ddlPackage.SelectedValue, Requestlist.STATUS_PENDING, rbtRequestType.SelectedValue, documentlist);



            requestlist.Add(newRequestlist);

            string paymentID = null;
            string companyID = "";
            string CustomerID = "";
            DateTime currentDate = DateTime.Now;
            DateTime DueDate = Convert.ToDateTime(txtCalender.Text);

            Request request = new Model.Entities.Request(currentDate, DueDate, null, companyID, CustomerID, requestlist);
            return request;

        }


    }
}





