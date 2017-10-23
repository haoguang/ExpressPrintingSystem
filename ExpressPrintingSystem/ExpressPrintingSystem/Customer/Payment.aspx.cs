using ExpressPrintingSystem.Staff.Printing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Customer
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string Server_URL = "https://www.sandbox.paypal.com/cgi-bin/webscr?";

            //Assigning Cmd Path as Statically to Parameter
            string cmd = "_xclick";

            //Assigning business Id as Statically to Parameter
            string business = "Expressprintingsystem2017@gmail.com";// Enter your business account here 

            //Assigning item name as Statically to Parameter
            string item_name = "Item 1";

            //Passing Amount as Statically to parameter 
            double amount = 100.00;//Convert.ToDouble(txtpaymentTotal.Text);

            //Passing Currency as Statically to parameter
            string currency_code = "MYR";

            string redirect = "";

            //Pass your Server_Url,cmd,business,item_name,amount,currency_code variable.        
            redirect += Server_URL;
            redirect += "cmd=" + cmd;
            redirect += "&business=" + business;
            redirect += "&first_name=" + "Name";
            redirect += "&item_name=" + item_name;
            redirect += "&amount=" + amount;
            redirect += "&quantity=1";
            redirect += "&currency_code=" + currency_code;

            string successURL = ConfigurationManager.AppSettings["SuccessURL"].ToString();
            redirect += "&return=" + successURL;
            redirect += "&cancel_return=" + ConfigurationManager.AppSettings["FailedURL"].ToString();


            Session["amount"] = amount;
            Response.Redirect(redirect);


            
           
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            DateTime currentDate = DateTime.Now;
            Decimal totalPayment = Convert.ToDecimal(txtpaymentTotal.Text);

            Model.Entities.Payment newpayment = new Model.Entities.Payment(RadioButtonList1.SelectedValue, totalPayment, currentDate);
            Model.Entities.Request request = (Model.Entities.Request)Session["request"];
            request.Payment = newpayment;

            try
            {
                SqlConnection conTaxi;
                string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                conTaxi = new SqlConnection(connStr);
                conTaxi.Open();

                string strInsert;
                SqlCommand cmdInsert;


                strInsert = "Insert Into Payment (PaymentType, PaymentAmount, PaymentDateTime) Values (@PaymentType, @PaymentAmount, @PaymentDateTime);SELECT MAX(PaymentID) from Payment where PaymentAmount=@PaymentAmount";
                cmdInsert = new SqlCommand(strInsert, conTaxi);

                Decimal totalamount = Convert.ToDecimal(txtpaymentTotal.Text);

                cmdInsert.Parameters.AddWithValue("@PaymentType",request.Payment.PaymentType);
                cmdInsert.Parameters.AddWithValue("@PaymentAmount", request.Payment.PaymentAmount);
                cmdInsert.Parameters.AddWithValue("@PaymentDateTime", request.Payment.PaymentDateTime);
                var getPaymentID = cmdInsert.ExecuteScalar();

                if (getPaymentID != null)
                {
                    request.Payment.PaymentID = (string)getPaymentID;
                    insertNewRequest(request);
                    Response.Write("<script>alert('Upload Successful');</script>");

                }
                else
                {
                    Response.Write("<script>alert('Upload Failed');</script>");
                }

                /*Close database connection*/


                conTaxi.Close();
            }
            catch (Exception ex){
                Response.Write("<script>alert('Upload Failed');</script>");
            }
        }

        private void insertNewRequest(Model.Entities.Request request)
        {
            SqlConnection conPrintDB;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conPrintDB = new SqlConnection(connStr);
            conPrintDB.Open();

            string strInsert;
            SqlCommand cmdInsert;

            //request
            strInsert = "Insert Into Request (RequestDateTime, DueDateTime, PaymentID, CompanyID, CustomerID) Values (@requestDateTime, @dueDateTime, @paymentID, @companyID, @customerID);SELECT MAX(RequestID) from Request where CustomerID=@customerID AND RequestDateTime = @requestDateTime;";
            cmdInsert = new SqlCommand(strInsert, conPrintDB);

            cmdInsert.Parameters.AddWithValue("@requestDateTime", DateTime.Now);
            cmdInsert.Parameters.AddWithValue("@dueDateTime", request.DueDateTime);
            cmdInsert.Parameters.AddWithValue("@paymentID", request.Payment.PaymentID);
            cmdInsert.Parameters.AddWithValue("@companyID", request.CompanyID);
            cmdInsert.Parameters.AddWithValue("@customerID", request.CustomerID);

            var requestID = cmdInsert.ExecuteScalar();

            request.RequestID = (string)requestID;

            //Requestlist
            strInsert = "Insert Into Requestlist (RequestID, RequestItemID, RequestStatus, RequestType) Values (@RequestID, @RequestItemID, @RequestStatus, @RequestType);SELECT MAX(RequestlistID) from Requestlist where RequestID=@RequestID AND RequestItemID=@RequestItemID";
            cmdInsert = new SqlCommand(strInsert, conPrintDB);


            cmdInsert.Parameters.AddWithValue("@RequestID", request.RequestID);
            cmdInsert.Parameters.AddWithValue("@RequestItemID", request.RequestLists[0].RequestItemID);
            cmdInsert.Parameters.AddWithValue("@RequestStatus", request.RequestLists[0].RequestStatus);
            cmdInsert.Parameters.AddWithValue("@RequestType", request.RequestLists[0].RequestType);

            var requestlistID = cmdInsert.ExecuteScalar();

            request.RequestLists[0].RequestlistID = (string)requestlistID;

            //documentlist
            strInsert = "Insert Into Documentlist (RequestlistID, DocumentID, Sequences, DocumentColor, DocumentBothSide, DocumentPaperType, DocumentQuantity, DocumentDescription) Values (@RequestlistID, @DocumentID, @Sequences, @DocumentColor, @DocumentBothSide, @DocumentPaperType, @DocumentQuantity, @DocumentDescription)";
            cmdInsert = new SqlCommand(strInsert, conPrintDB);

            foreach (Model.Entities.Documentlist documentlist in request.RequestLists[0].DocumentList) {
                insertDocument(documentlist.Document, conPrintDB);//insert document contain in documentlist

                cmdInsert.Parameters.Clear();//clear parameter before loop
                cmdInsert.Parameters.AddWithValue("@RequestlistID", request.RequestLists[0].RequestlistID);
                cmdInsert.Parameters.AddWithValue("@DocumentID", documentlist.Document.DocumentID);
                cmdInsert.Parameters.AddWithValue("@Sequences", documentlist.Sequences);
                cmdInsert.Parameters.AddWithValue("@DocumentColor", documentlist.DocumentColor);
                cmdInsert.Parameters.AddWithValue("@DocumentBothSide", documentlist.DocumentBothSide);
                cmdInsert.Parameters.AddWithValue("@DocumentPaperType", documentlist.DocumentPaperType);
                cmdInsert.Parameters.AddWithValue("@DocumentQuantity", documentlist.DocumentQuantity);
                cmdInsert.Parameters.AddWithValue("@DocumentDescription", documentlist.DocumentDescription);
                cmdInsert.ExecuteNonQuery();
            }

            conPrintDB.Close();
            PrintingRequestHub.refreshTable();
            


        }

        private void insertDocument(Model.Entities.Document document ,SqlConnection condocument) {


            
            string strInsert = "Insert Into Document (DocumentName, DocumentType, FileIDInCloud, CustomerID, Size, PageNumber) Values (@DocumentName, @DocumentType, @FileIDInCloud, @CustomerID, @Size, @PageNumber);SELECT MAX(DocumentID) from Document where DocumentName=@DocumentName and DocumentType=@DocumentType";
            SqlCommand cmdInsert = new SqlCommand(strInsert, condocument);

                cmdInsert.Parameters.AddWithValue("@DocumentName", document.DocumentName);
                cmdInsert.Parameters.AddWithValue("@DocumentType", document.DocumentType);
                cmdInsert.Parameters.AddWithValue("@FileIDInCloud", document.FileIDInCloud);
                cmdInsert.Parameters.AddWithValue("@CustomerID", document.CustomerID);
                cmdInsert.Parameters.AddWithValue("@Size", document.Size);
                cmdInsert.Parameters.AddWithValue("@PageNumber", document.PageNumber);

                var getDocumentID = cmdInsert.ExecuteScalar();

                document.DocumentID = getDocumentID.ToString();


            }

        //public void forRetriveRequest(Model.Entities.Request request) { 

            //    SqlConnection conTaxi;
            //    string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            //    conTaxi = new SqlConnection(connStr);
            //    conTaxi.Open();

            //    string strInsert;
            //    SqlCommand cmdInsert;


            //    strInsert = "Insert Into Request(RequestDateTime, DueDateTime, PaymentID, CompanyID, CustomerID) Values (@RequestDateTime, @DueDateTime, @PaymentID, @CompanyID, @CustomerID)";
            //    cmdInsert = new SqlCommand(strInsert, conTaxi);


            //    cmdInsert.Parameters.AddWithValue("@RequestDateTime",request.RequestDateTime);
            //    cmdInsert.Parameters.AddWithValue("@DueDateTime", request.DueDateTime);
            //    cmdInsert.Parameters.AddWithValue("@PaymentID", request.Payment.PaymentID);
            //    cmdInsert.Parameters.AddWithValue("@CompanyID", request.CompanyID);
            //    cmdInsert.Parameters.AddWithValue("@CustomerID", request.CustomerID);




            //    int n = cmdInsert.ExecuteNonQuery();

            //    if (n > 0)
            //    {
            //        Response.Write("<script>alert('Upload Successful');</script>");


            //    }
            //    else
            //    {
            //        Response.Write("<script>alert('Upload Failed');</script>");
            //    }

            //    /*Close database connection*/


            //    conTaxi.Close();

            //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}