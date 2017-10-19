using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ExpressPrintingSystem
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection conPrint;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conPrint = new SqlConnection(connStr);
            conPrint.Open();

            string strInsert;
            SqlCommand cmdInsert;

            strInsert = "select CustomerID, CustomerPassword from Customer where CustomerEmail = @customeremail";


            cmdInsert = new SqlCommand(strInsert, conPrint);
            //cmdInsert.Parameters.AddWithValue("@CustomerID", TextBox6.Text); (not neccessary as database will handle with trigger)
            //cmdInsert.Parameters.AddWithValue("@CustomerID", txtEmail.Text);
            //cmdInsert.Parameters.AddWithValue("@CustomerEmail", txtEmail.Text);
            cmdInsert.Parameters.AddWithValue("@customeremail", TextBox1.Text);
            SqlDataReader dtr;
            dtr = cmdInsert.ExecuteReader();

            
            
            if (dtr.Read())
            {
                string customerID = dtr["CustomerID"].ToString();
                string password = dtr["CustomerPassword"].ToString();

                MessageBox.Show("Sign up done. Redirecting to homepage.", "Congratulation !!",
               MessageBoxButtons.OK,
               MessageBoxIcon.Exclamation,
               MessageBoxDefaultButton.Button1);
                Response.Redirect("VerificationAttribute.aspx");

            }
            else
            {
                MessageBox.Show("Sign up failed.", "Congratulation !!",
               MessageBoxButtons.OK,
               MessageBoxIcon.Exclamation,
               MessageBoxDefaultButton.Button1);
                Response.Write("<script LANGUAGE='JavaScript' >alert('sign up failded')</script>");
            }
            conPrint.Close();
        }
    }
}