using ExpressPrintingSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.StaffManagement
{
    public partial class registerStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSubmit.UniqueID;
            chkPassSet_CheckedChanged(null, null);//to hide the text box by default
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection conPrintDB;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conPrintDB = new SqlConnection(connStr);
            conPrintDB.Open();
            try
            {
                string strInsert;
                SqlCommand cmdInsert;

                strInsert = "Insert Into CompanyStaff (StaffName, StaffEmail, StaffPassword, StaffNRIC, StaffDOB, StaffPhoneNo, StaffSalt, StaffRole, CompanyID) Values (@staffName, @staffEmail,@staffPassword, @staffNRIC, @staffDOB, @staffPhoneNo, @staffSalt, @staffRole, @companayID); SELECT MAX(StaffID) from CompanyStaff where StaffName=@staffName and StaffPhoneNo=@staffPhoneNo";

                byte[] generatedSalt = ClassHashing.generateSalt();
                byte[] hashPassword = {0,0};//empty password when checkbox for password setting is not checked

                if (chkPassSet.Checked)
                    hashPassword = ClassHashing.generateSaltedHash(txtPassword.Text, generatedSalt);
                string abc = Request.Cookies["CompanyID"].Value;
                cmdInsert = new SqlCommand(strInsert, conPrintDB);
                cmdInsert.Parameters.AddWithValue("@staffName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@staffEmail", txtEmail.Text);
                cmdInsert.Parameters.AddWithValue("@staffPassword", hashPassword);
                cmdInsert.Parameters.AddWithValue("@staffNRIC", txtNRIC.Text);
                cmdInsert.Parameters.AddWithValue("@staffDOB", cldBOD.SelectedDate.ToShortDateString());
                cmdInsert.Parameters.AddWithValue("@staffPhoneNo", txtPhoneNo.Text);
                cmdInsert.Parameters.AddWithValue("@staffSalt", generatedSalt);
                cmdInsert.Parameters.AddWithValue("@staffRole", UserVerification.ROLE_STAFF);
                cmdInsert.Parameters.AddWithValue("@companayID", Request.Cookies["CompanyID"].Value);

                var staffID = cmdInsert.ExecuteScalar();

                if (!chkPassSet.Checked)
                {
                    byte[] verificationCode = UserVerification.getVerificationCode(staffID + txtNRIC.Text, generatedSalt);
                    //then it will generate a url to activate the account and send it to the staff
                }

                lblError.Text = "Successfully added";
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occured when adding item :" + ex.ToString();
            }
            finally
            {
                conPrintDB.Close();
            }


            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string returnUrl = Request.QueryString["ReturnUrl"] as string;
            Response.Redirect(returnUrl);
        }

        protected void chkPassSet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassSet.Checked)
            {
                passRow.Visible = true;
                txtPassword.Enabled = true;
                txtConfirmPass.Enabled = true;             
            }
            else
            {
                passRow.Visible = false;
                txtPassword.Enabled = false;
                txtConfirmPass.Enabled = false;

            }
        }

        
    }
}