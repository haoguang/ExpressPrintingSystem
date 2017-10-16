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
            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;


            strInsert = "Insert Into Payment (PaymentType, PaymentAmount, PaymentDateTime) Values (@PaymentType, @PaymentAmount, @PaymentDateTime)";
            cmdInsert = new SqlCommand(strInsert, conTaxi);



            cmdInsert.Parameters.AddWithValue("@PaymentType", RadioButtonList1.SelectedValue);
            cmdInsert.Parameters.AddWithValue("@PaymentAmount", txtpaymentTotal.Text );
            cmdInsert.Parameters.AddWithValue("@PaymentDateTime", DateTime.Today.ToShortDateString());
            


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