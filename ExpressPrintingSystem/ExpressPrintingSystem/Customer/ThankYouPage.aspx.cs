using ExpressPrintingSystem.Staff.Printing;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Customer
{
    public partial class ThankYouPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

                String Paypal = "Paypal";
                decimal amountpaypal = (decimal)(Session["amount"]);
                decimal totalamount = Convert.ToDecimal(amountpaypal);
            Model.Entities.Request request = (Model.Entities.Request)Session["request"];

            SqlConnection conTaxi;
                string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                conTaxi = new SqlConnection(connStr);
                conTaxi.Open();

                string strInsert;
                SqlCommand cmdInsert;


                strInsert = "Insert Into Payment (PaymentType, PaymentAmount, PaymentDateTime) Values (@PaymentType, @PaymentAmount, @PaymentDateTime);SELECT MAX(PaymentID) from Payment where PaymentAmount=@PaymentAmount";
                cmdInsert = new SqlCommand(strInsert, conTaxi);

             

                cmdInsert.Parameters.AddWithValue("@PaymentType", Paypal);
                cmdInsert.Parameters.AddWithValue("@PaymentAmount", totalamount);
                cmdInsert.Parameters.AddWithValue("@PaymentDateTime", DateTime.Now);
                var getPaymentID = cmdInsert.ExecuteScalar();

                if (getPaymentID != null)
                {

                             
                Response.Write("<script>alert('PayPal Successful');</script>");
            }
            else
                {
                    Response.Write("<script>alert('Upload Failed');</script>");
                }
            PrintingRequestHub.refreshTable();
        }

        
       
        protected void btnBack_Click(object sender, EventArgs e)
        {


            Response.Redirect("~/masterPageTest.aspx");
        }
    }
}