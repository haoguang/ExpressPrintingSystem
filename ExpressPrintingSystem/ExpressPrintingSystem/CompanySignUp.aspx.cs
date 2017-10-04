using ExpressPrintingSystem.Model;
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

            Response.Redirect("~/CompanySignUp.aspx");
            // btnCustomer.CssClass = "btn btn-default";
            //  btnCompany.CssClass = "btn btn-danger";



        }


        protected void btnCompanySubmit_Click1(object sender, EventArgs e)
        {


            byte[] generatedSalt = ClassHashing.generateSalt();
            byte[] hashPassword = ClassHashing.generateSaltedHash(txtStaffPassword.Text, generatedSalt);


            //try
            //{



                SqlConnection conPrint;
                SqlConnection conOwner;

                string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                conPrint = new SqlConnection(connStr);
                conOwner = new SqlConnection(connStr);

                conPrint.Open();

                string strInsert;
                SqlCommand cmdInsert;
                strInsert = "Insert Into Company (CompanyName, CompanyAddress, CompanyContactNo, CompanyEmail) Values ( @CompanyName, @CompanyAddress, @CompanyContactNo, @CompanyEmail);SELECT MAX(CompanyID) from Company where CompanyName=@CompanyName and CompanyContactNo=@CompanyContactNo ";


                cmdInsert = new SqlCommand(strInsert, conPrint);
                //cmdInsert.Parameters.AddWithValue("@CustomerID", TextBox6.Text); (not neccessary as database will handle with trigger)
                cmdInsert.Parameters.AddWithValue("@CompanyName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@CompanyAddress", txtAddress.Text);
                cmdInsert.Parameters.AddWithValue("@CompanyContactNo", txtContNo.Text);
                cmdInsert.Parameters.AddWithValue("@CompanyEmail", txtEmail.Text);
                var getCompanyId = cmdInsert.ExecuteScalar();


            if (getCompanyId != null)
                {
               
                
               
                conOwner.Open();

                    string strOwnerInsert;
                    SqlCommand cmdCompanyInsert;
                    strOwnerInsert = "Insert Into CompanyStaff (StaffName, StaffEmail, StaffPassword, StaffNRIC, StaffDOB, StaffPhoneNo, StaffSalt, StaffRole, CompanyID) Values ( @StaffName, @StaffEmail, @StaffPassword, @StaffNRIC, @StaffDOB, @StaffPhoneNo, @StaffSalt, @StaffRole, @CompanyID)";

                    cmdCompanyInsert = new SqlCommand(strOwnerInsert, conOwner);
                    cmdCompanyInsert.Parameters.AddWithValue("@StaffName", txtStaffName.Text);
                    cmdCompanyInsert.Parameters.AddWithValue("@StaffEmail", txtStaffEmail.Text);
                    cmdCompanyInsert.Parameters.AddWithValue("@StaffPassword", hashPassword);
                    cmdCompanyInsert.Parameters.AddWithValue("@StaffNRIC", txtStaffNRIC.Text);
                    cmdCompanyInsert.Parameters.AddWithValue("@StaffDOB", Calendar1.SelectedDate.ToShortDateString());
                    cmdCompanyInsert.Parameters.AddWithValue("@StaffPhoneNo", txtStaffPhoneNumber.Text);
                    cmdCompanyInsert.Parameters.AddWithValue("@StaffSalt", generatedSalt);
                    cmdCompanyInsert.Parameters.AddWithValue("@StaffRole", UserVerification.ROLE_ADMIN);
                    cmdCompanyInsert.Parameters.AddWithValue("@CompanyID", getCompanyId);
                    cmdCompanyInsert.ExecuteNonQuery();

                conOwner.Close();

                Response.Write("<script LANGUAGE='JavaScript' >alert('Login Successful')</script>");
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('sign up failded')</script>");
            }







            conPrint.Close();
            
            //}
            //catch (SqlException ex)
            //{
            //    Response.Write("<script LANGUAGE='JavaScript' >alert('Something gone wrong with the database.')</script>");
            //}


        

    
        }

        protected void btnCompanyCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}