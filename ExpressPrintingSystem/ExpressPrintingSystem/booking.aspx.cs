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


                    string detail = "Information" + "<br/>";
                    detail += "----------------------" + "<br />";
                    detail += "Color page" + "<br/>";
                    detail += "-----------" + "<br/>";
                    detail += "Package 1 -" + "RM1.30 for Binding, Plastic cover and RM0.20 for color page" + "<br/>";
                    detail += "Package 2 -" + "Without Binding and Plastic cover and RM0.20 for color page" + "<br/>";
                    detail += "Non-Color" + "<br/>";
                    detail += "-----------" + "<br/>";
                    detail += "Package 3 -" + "RM1.30 for binding, Plastic cover and RM0.10 for non-color page" + "<br/>";
                    detail += "Package 4 -" + "Without binding, Plastic cover and RM0.10 for non-color page" + "<br/>";
        
                    Label11.Text = detail;

                }
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {

            Request request = PopulateDataToObject();

            this.package();
            Session["request"] = request;
            Response.Redirect("~/Customer/CreditCard.aspx");


        }

        private List<Documentlist> createDocumentList()
        {

            string userID = ClassHashing.basicDecryption((string)ViewState["UserID"]);

            //create document list and upload file to cloud
            List<Documentlist> documentList = new List<Documentlist>();
            int totalpage = 0;
            int count = 0;
            int normalCount = 0;

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

                       
                           lblgetallfilename.Text += String.Format("{0} , ", fileName);

                        string getallfilename = lblgetallfilename.Text;
                        Session["allfilename"] = getallfilename;

                        Session["pathfile"] = filePath;

                        string getFileIDInCloud = backblaze.UploadFile(contentType, filePath, fileName);
                        int numberOfPages = 0;
                        if (Path.GetExtension(hpf.FileName).Equals(".docx"))
                        {

                            // get the page number
                            var application = new Application();
                            var document = application.Documents.Open(filePath);//open document
                            numberOfPages = document.ComputeStatistics(WdStatistic.wdStatisticPages, false);
                            document.Close();///close document

                            //get the count of file
                            count = hfc.Count;
                            Session["countthepackageitem"] = count;

                            FileInfo file = new FileInfo(filePath);
                            if (file.Exists)//check file exsit or not
                            {
                                file.Delete();

                                

                            }

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

                            //get the count of file
                            count = hfc.Count;
                            Session["countthepackageitem"] = count;

                            FileInfo file = new FileInfo(filePath);
                            if (file.Exists)//check file exsit or not
                            {
                                file.Delete();

                            }

                        }
                        else if (Path.GetExtension(hpf.FileName).Equals(".png") || Path.GetExtension(hpf.FileName).Equals(".PNG") || Path.GetExtension(hpf.FileName).Equals(".jpg"))
                        {
                            count = hfc.Count - 1;
                            Session["countthepackageitem"] = count;
                            numberOfPages = 1;

                            FileInfo file = new FileInfo(filePath);
                            if (file.Exists)//check file exsit or not
                            {
                                file.Delete();

                            }
                        }


                        //calculate the total page in multiple file 
                       
                            totalpage += numberOfPages;
                            Session["totalpage"] = totalpage;

                        normalCount = hfc.Count;
                        Session["normalcount"] = normalCount;


                         // upload to my sqldatabase
                        var uploadFileObject = (JObject)JsonConvert.DeserializeObject(getFileIDInCloud);
                        String FileIdInCloud = uploadFileObject["fileId"].Value<string>();//get file ID

                       

                        Model.Entities.Document newdocument = new Model.Entities.Document(fileName, contentType, FileIdInCloud, userID, size, numberOfPages);

                        int sequences = 0; ////remember to do it;  
                        string documentColor = rbtDocumentColor.SelectedValue;
                        string documentbothside = rbtDocumentSide.SelectedValue;
                        int documentpapertype = Convert.ToInt32(ddlPaperType.SelectedValue);
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

            string companyID = Request.QueryString["CompanyID"];
            string CustomerID = ClassHashing.basicDecryption((string)ViewState["UserID"]);
            DateTime currentDate = DateTime.Now;
            string dateString =  DropDownList4.SelectedItem + "/" + DropDownList5.SelectedItem + "/" + DropDownList6.SelectedItem;

            string timer = DropDownList1.SelectedItem + ":" + DropDownList2.SelectedItem + " " + DropDownList3.SelectedItem;

            DateTime DueDate = Convert.ToDateTime(dateString + " " + timer);

            if (DueDate <= currentDate)
            {

                Response.Write("<script>alert('please select the date and time is more than current Date');</script>");

                
            }
           

            Request request = new Model.Entities.Request(currentDate, DueDate, null, companyID, CustomerID, requestlist);

            return request;

        }

        public void package() {

           


            SqlConnection conPrint;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conPrint = new SqlConnection(connStr);
            conPrint.Open();

            string strInsert;
            SqlCommand cmdInsert;

            strInsert = "select PackageID, PackageName, PackagePrice, PackageSupport, PackageType, PrintingPricePerPaper, CompanyID  from Package where PackageID = @PackageID";


            cmdInsert = new SqlCommand(strInsert, conPrint);

            cmdInsert.Parameters.AddWithValue("@PackageID", ddlPackage.SelectedValue);
            SqlDataReader dtr;
            dtr = cmdInsert.ExecuteReader();



            if (dtr.Read())
            {
                string pID = dtr["PackageID"].ToString();
                string pName = dtr["PackageName"].ToString();
                decimal pPrice =  Convert.ToDecimal(dtr["PackagePrice"].ToString());
                string pSupport = dtr["PackageSupport"].ToString();
                string pType = dtr["PackageType"].ToString();
                decimal pPerPrice = Convert.ToDecimal( dtr["PrintingPricePerPaper"].ToString());
                string pCustomerID = dtr["CompanyID"].ToString();


                Package package = new Package(pID, pName, pPrice, pSupport, pType, pPerPrice);

                Session["package"] = package;
            }
            else
            {

                Response.Write("<script LANGUAGE='JavaScript' >alert('Email invalid')</script>");
            }
            conPrint.Close();


           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            

                Response.Redirect("~/masterPageTest.aspx");
            
        }
    }
}





