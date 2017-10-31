using ExpressPrintingSystem.Model.Entities;
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
    public partial class CreditCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

     
        protected void btnCreditCardlink_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.Cookies["UserCookie"] != null)
            {

                var Cookie = Request.Cookies["UserCookie"];

                if (Cookie.Values["UserInfo"] != null)
                {
                    string userString = ClassHashing.basicDecryption(Cookie.Values["UserInfo"].ToString());
                    User user = ExpressPrintingSystem.Model.Entities.User.toUserObject(userString);


                    SqlConnection conTaxi;
                    string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                    conTaxi = new SqlConnection(connStr);
                    conTaxi.Open();

                    string strCheck;
                    SqlCommand cmdCheck;
                    strCheck = "Select CustomerEmail from Customer Where CustomerID=@CustomerID";
                    cmdCheck = new SqlCommand(strCheck, conTaxi);
                    cmdCheck.Parameters.AddWithValue("@CustomerID", user.ID);
                    SqlDataReader dtr;
                    dtr = cmdCheck.ExecuteReader();

                    if (dtr.Read())
                    {

                        string customeremail = dtr["CustomerEmail"].ToString();

                        Session["email"] = customeremail;

                    }


                    conTaxi.Close();
                }
            }


            Response.Redirect("Payment.aspx");
        }
    }
}