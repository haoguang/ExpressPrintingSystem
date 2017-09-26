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
            byte[] hashPassword = ClassHashing.generateSaltedHash(TextBox3.Text, generatedSalt);

           


            //string abc = "Salt:" + Convert.ToBase64String(generatedSalt) + "\n";
            //abc = abc + "hashedPassword 1：" + Convert.ToBase64String(hashPassword) + "\n";
            //abc = abc + "hashedPassword 2：" + Convert.ToBase64String(ClassHashing.generateSaltedHash(TextBox3.Text, generatedSalt));
            //Button1.Text = abc;



            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;

            strInsert = "Insert Into Customer (CustomerID, CustomerName, CustomerEmail, CustomerPassword, CustomerDOB, CustomerPhoneNo, CustomerContactMethod, CustomerSalt) Values (@CustomerID, @CustomerName, @CustomerEmail, @CustomerPassword, @CustomerDOB, @CustomerPhoneNo, @CustomerContactMethod, @CustomerSalt)";

            
            cmdInsert = new SqlCommand(strInsert, conTaxi);
            cmdInsert.Parameters.AddWithValue("@CustomerID", TextBox6.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerName", TextBox1.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerEmail", TextBox2.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerPassword", hashPassword);
            cmdInsert.Parameters.AddWithValue("@CustomerDOB", Calendar1.SelectedDate.ToShortDateString());
            cmdInsert.Parameters.AddWithValue("@CustomerPhoneNo", TextBox5.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerContactMethod", CheckBox1.Text);
            cmdInsert.Parameters.AddWithValue("@CustomerSalt", generatedSalt);

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
    
