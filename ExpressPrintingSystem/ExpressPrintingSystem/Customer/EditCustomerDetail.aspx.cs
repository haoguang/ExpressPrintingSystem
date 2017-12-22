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
    public partial class EditCustomerDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Request.Cookies["UserCookie"] != null)
                {

                    var Cookie = Request.Cookies["UserCookie"];

                    if (Cookie.Values["UserInfo"] != null)
                    {
                        string userString = ClassHashing.basicDecryption(Cookie.Values["UserInfo"].ToString());
                        User user = ExpressPrintingSystem.Model.Entities.User.toUserObject(userString);
                        ViewState["UserID"] = ClassHashing.basicEncryption(user.ID);

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

                            
                            txtName.Text = CustomerName;
                            txtEmail.Text = customeremail;
                            customerDOB.Text = Convert.ToString(customerdob);
                            txtPhoNo.Text = customerphoneno;
                            rbtContMet.SelectedValue = customercontmethod;


                        }


                        conTaxi.Close();
                    }
                }

            }




        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userID = ClassHashing.basicDecryption((string)ViewState["UserID"]);

            

            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strEdit;
            SqlCommand cmdAdd;

            strEdit = "Update Customer Set CustomerName = @customerName, CustomerEmail = @customerEmail, CustomerDOB = @customerDOB, CustomerPhoneNo = @customerPhoneNo, CustomerContactMethod = @customerContactMethod WHERE CustomerID = @customerID";

            cmdAdd = new SqlCommand(strEdit, conTaxi);
            cmdAdd.Parameters.AddWithValue("@customerName", txtName.Text);
            cmdAdd.Parameters.AddWithValue("@customerEmail", txtEmail.Text);
            cmdAdd.Parameters.AddWithValue("@customerDOB", Convert.ToDateTime(customerDOB.Text));
            cmdAdd.Parameters.AddWithValue("@customerPhoneNo", txtPhoNo.Text);
            cmdAdd.Parameters.AddWithValue("@customerContactMethod", rbtContMet.SelectedValue);
            cmdAdd.Parameters.AddWithValue("@customerID", userID);



            int m = cmdAdd.ExecuteNonQuery();
            if (m > 0)
            {
               
                Response.Write("<script>alert('Updated Successful');</script>");

                Response.Redirect("CustomerDetail.aspx");
            }
            else
            {
                Response.Write("<script>alert('Updated Failed');</script>");
            }


            conTaxi.Close();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/masterPageTest.aspx");
        }
    }
}