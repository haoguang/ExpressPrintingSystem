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
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;

            strInsert = "Insert Into Customer (CustomerName, CustomerEmail, CustomerPassword, CustomerDOB, CustomerPhoneNo, CustomerContactMethod) Values (@CustomerName, @CustomerEmail, @CustomerPassword, @CustomerDOB, @CustomerPhoneNo, @CustomerContactMethod)";

            cmdInsert = new SqlCommand(strInsert, conTaxi);

            cmdInsert.Parameters.AddWithValue("@CustomerName", TextBox1.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerEmail", TextBox2.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerPassword", TextBox3.Text);
           
            cmdInsert.Parameters.AddWithValue("@CustomerDOB", Calendar1.GetHashCode());
            cmdInsert.Parameters.AddWithValue("@CustomerPhoneNo", TextBox5.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerContactMethod", CheckBox1.Checked.ToString());

            int n = cmdInsert.ExecuteNonQuery();

            if (n > 0)
            {

                Response.Write("<script LANGUAGE='JavaScript' >alert('Login Successful')</script>");
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('sign up failded')</script>");
            }


            conTaxi.Close();
        }
    }

}
    
