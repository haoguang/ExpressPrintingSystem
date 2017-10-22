using ExpressPrintingSystem.Model.Entities;
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
    public partial class CustomerDetail : System.Web.UI.Page
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
                    

                    SqlConnection conTaxi;
                    string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                    conTaxi = new SqlConnection(connStr);
                    conTaxi.Open();

                    string strCheck;
                    SqlCommand cmdCheck;
                    strCheck = "Select CustomerName, CustomerEmail, CustomerDOB, CustomerPhoneNo, CustomerContactMethod from Customer Where CustomerID=@CustomerID";
                    cmdCheck = new SqlCommand(strCheck, conTaxi);
                    cmdCheck.Parameters.AddWithValue("@CustomerID", user.ID);
                    SqlDataReader dtr;
                    dtr = cmdCheck.ExecuteReader();
                    
                        if (dtr.Read())
                        {
                            string CustomerName = dtr["CustomerName"].ToString();
                            string customeremail = dtr["CustomerEmail"].ToString();
                           
                            DateTime customerdob = Convert.ToDateTime(dtr["CustomerDOB"]);
                            string customerphoneno = dtr["CustomerPhoneNo"].ToString();
                            string customercontmethod = dtr["CustomerContactMethod"].ToString();

                            lblName.Text = CustomerName;
                            lblEmail.Text = customeremail;                         
                            lblDOB.Text = customerdob.ToShortDateString();
                            lblPhoneNumber.Text = customerphoneno;
                            lblContMethod.Text = customercontmethod;


                        }

                    
                    conTaxi.Close();
                }
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditCustomerDetail.aspx");
        }

       
    }
}