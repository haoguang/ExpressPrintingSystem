using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem
{
    public partial class resetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPassword.Text.Equals(txtComfirmPassword.Text))
                {
                    byte[] generatedSalt = ClassHashing.generateSalt();
                    byte[] hashPassword = ClassHashing.generateSaltedHash(txtNewPassword.Text, generatedSalt);

                    //received customerID from forgetPassword.
                    string customerID = (string)(Session["CustomerID"]);

                    SqlConnection conPrint;
                    string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                    conPrint = new SqlConnection(connStr);
                    conPrint.Open();

                    string strInsert;
                    SqlCommand cmdInsert;


                    strInsert = "Update Customer SET CustomerPassword = @CustomerPassword, CustomerSalt = @CustomerSalt WHERE CustomerID = @customerID";


                    cmdInsert = new SqlCommand(strInsert, conPrint);
                    cmdInsert.Parameters.AddWithValue("@CustomerPassword", hashPassword);
                    cmdInsert.Parameters.AddWithValue("@CustomerSalt", generatedSalt);
                    cmdInsert.Parameters.AddWithValue("@customerID", customerID);

                    int n = cmdInsert.ExecuteNonQuery();

                    conPrint.Close();

                    Response.Write("<script LANGUAGE='JavaScript' >alert('Successful ResetPassword.')</script>");
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Comfirm Password invalid.')</script>");

                }
            }
            catch {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Please fill in the password.')</script>");
            }

            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}