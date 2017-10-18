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

            redirect += "&return=" + ConfigurationManager.AppSettings["SuccessURL"].ToString();
            redirect += "&cancel_return=" + ConfigurationManager.AppSettings["FailedURL"].ToString();

            Response.Redirect(redirect);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            DateTime currentDate = DateTime.Now;
            Decimal totalPayment = Convert.ToDecimal(txtpaymentTotal.Text);

            Model.Entities.Payment newpayment = new Model.Entities.Payment(RadioButtonList1.SelectedValue, totalPayment, currentDate);
            Model.Entities.Request request = (Model.Entities.Request)Session["request"];
            request.Payment = newpayment;


            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;


            strInsert = "Insert Into Payment (PaymentType, PaymentAmount, PaymentDateTime) Values (@PaymentType, @PaymentAmount, @PaymentDateTime);SELECT MAX(PaymentID) from Payment where PaymentAmount=@PaymentAmount";
            cmdInsert = new SqlCommand(strInsert, conTaxi);

            Decimal totalamount = Convert.ToDecimal(txtpaymentTotal.Text);
            
            cmdInsert.Parameters.AddWithValue("@PaymentType", RadioButtonList1.SelectedValue);
            cmdInsert.Parameters.AddWithValue("@PaymentAmount", totalamount);
            cmdInsert.Parameters.AddWithValue("@PaymentDateTime", DateTime.Now);
            var getPaymentID = cmdInsert.ExecuteScalar();

            if (getPaymentID != null)
            {


                newpayment.PaymentID = getPaymentID.ToString();



            }


            int n = cmdInsert.ExecuteNonQuery();

            if (n > 0)
            {
                Response.Write("<script>alert('Upload Successful');</script>");

                forRetriveRequest(request);

            }
            else
            {
                Response.Write("<script>alert('Upload Failed');</script>");
            }

            /*Close database connection*/


            conTaxi.Close();
        }
        public void forRetriveRequest(Model.Entities.Request request) { 

            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;


            strInsert = "Insert Into Request(RequestDateTime, DueDateTime, PaymentID, CompanyID, CustomerID) Values (@RequestDateTime, @DueDateTime, @PaymentID, @CompanyID, @CustomerID)";
            cmdInsert = new SqlCommand(strInsert, conTaxi);


            cmdInsert.Parameters.AddWithValue("@RequestDateTime",request.RequestDateTime);
            cmdInsert.Parameters.AddWithValue("@DueDateTime", request.DueDateTime);
            cmdInsert.Parameters.AddWithValue("@PaymentID", request.Payment.PaymentID);
            cmdInsert.Parameters.AddWithValue("@CompanyID", request.CompanyID);
            cmdInsert.Parameters.AddWithValue("@CustomerID", request.CustomerID);

           


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