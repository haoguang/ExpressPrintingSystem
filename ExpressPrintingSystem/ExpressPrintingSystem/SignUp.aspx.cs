using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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



            byte[] generatedSalt = ClassHashing.generateSalt();
            byte[] hashPassword = ClassHashing.generateSaltedHash(txtPassword.Text, generatedSalt);




            //string abc = "Salt:" + Convert.ToBase64String(generatedSalt) + "\n";
            //abc = abc + "hashedPassword 1：" + Convert.ToBase64String(hashPassword) + "\n";
            //abc = abc + "hashedPassword 2：" + Convert.ToBase64String(ClassHashing.generateSaltedHash(TextBox3.Text, generatedSalt));
            //Button1.Text = abc;

            try { 

            SqlConnection conPrint;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conPrint = new SqlConnection(connStr);
            conPrint.Open();
            
            string strInsert;
            SqlCommand cmdInsert;

            strInsert = "Insert Into Customer (CustomerName, CustomerEmail, CustomerPassword, CustomerDOB, CustomerPhoneNo, CustomerContactMethod, CustomerSalt) Values ( @CustomerName, @CustomerEmail, @CustomerPassword, @CustomerDOB, @CustomerPhoneNo, @CustomerContactMethod, @CustomerSalt)";

            
            cmdInsert = new SqlCommand(strInsert, conPrint);
            //cmdInsert.Parameters.AddWithValue("@CustomerID", TextBox6.Text); (not neccessary as database will handle with trigger)
            cmdInsert.Parameters.AddWithValue("@CustomerName", txtName.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerEmail", txtEmail.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerPassword", hashPassword);
            cmdInsert.Parameters.AddWithValue("@CustomerDOB", Calendar1.SelectedDate.ToShortDateString());
            cmdInsert.Parameters.AddWithValue("@CustomerPhoneNo", txtPhoneNumber.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerContactMethod", rblMethod.SelectedValue);
            cmdInsert.Parameters.AddWithValue("@CustomerSalt", generatedSalt);

            int n = cmdInsert.ExecuteNonQuery();

            if (n > 0)
            {

                Response.Write("<script LANGUAGE='JavaScript' >alert('Login Successful')</script>");
                Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('sign up failded')</script>");
            }


            conPrint.Close();
            }
            catch (SqlException ex) {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Something gone wrong with the database.')</script>");
            }




        }

       

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
           
            //btnCustomer.CssClass = "btn btn-success";
            //btnCompany.CssClass = "btn btn-default";
            Response.Redirect("SignUp.aspx");
        }

        protected void btnCompany_Click(object sender, EventArgs e)
        {
            
            //btnCustomer.CssClass = "btn btn-default";
            //btnCompany.CssClass = "btn btn-danger";
            Response.Redirect("Customer/CompanySignUp.aspx");
        }
    }

}
    
