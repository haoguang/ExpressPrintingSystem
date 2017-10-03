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
    public partial class CompanySignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnCustomer.CssClass = "btn btn-default";
            btnCompany.CssClass = "btn btn-danger";

        }

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("~/SignUp.aspx");
           // btnCustomer.CssClass = "btn btn-success";
           // btnCompany.CssClass = "btn btn-default";
        }

        protected void btnCompany_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("~/Customer/CompanySignUp.aspx");
           // btnCustomer.CssClass = "btn btn-default";
          //  btnCompany.CssClass = "btn btn-danger";



        }

        protected void btnCompanySubmit_Click(object sender, EventArgs e)
        {
          
            //string abc = "Salt:" + Convert.ToBase64String(generatedSalt) + "\n";
            //abc = abc + "hashedPassword 1：" + Convert.ToBase64String(hashPassword) + "\n";
            //abc = abc + "hashedPassword 2：" + Convert.ToBase64String(ClassHashing.generateSaltedHash(TextBox3.Text, generatedSalt));
            //Button1.Text = abc;

            try
            {

                SqlConnection conPrint;
                string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                conPrint = new SqlConnection(connStr);
                conPrint.Open();

                string strInsert;
                SqlCommand cmdInsert;

                strInsert = "Insert Into Company (CompanyName, CompanyAddress, CompanyContactNo, CompanyEmail) Values ( @CompanyName, @CompanyAddress, @CompanyContactNo, @CompanyEmail)";


                cmdInsert = new SqlCommand(strInsert, conPrint);
                //cmdInsert.Parameters.AddWithValue("@CustomerID", TextBox6.Text); (not neccessary as database will handle with trigger)
                cmdInsert.Parameters.AddWithValue("@CompanyName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@CompanyAddress", txtAddress.Text);
                cmdInsert.Parameters.AddWithValue("@CompanyContactNo", txtContNo.Text);
                cmdInsert.Parameters.AddWithValue("@CompanyEmail", txtEmail.Text);
              

                int n = cmdInsert.ExecuteNonQuery();

                if (n > 0)
                {

                    Response.Write("<script LANGUAGE='JavaScript' >alert('Login Successful')</script>");
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('sign up failded')</script>");
                }


                conPrint.Close();
            }
            catch (SqlException ex)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Something gone wrong with the database.')</script>");
            }


        }
    }
}