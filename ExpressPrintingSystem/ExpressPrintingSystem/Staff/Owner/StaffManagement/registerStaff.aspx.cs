using ExpressPrintingSystem.Model;
using ExpressPrintingSystem.Model.Messenging;
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
        private const string DOMAIN_NAME = "http://localhost:53859";
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
                cmdInsert = new SqlCommand(strInsert, conPrintDB);
                cmdInsert.Parameters.AddWithValue("@staffName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@staffEmail", txtEmail.Text);
                cmdInsert.Parameters.AddWithValue("@staffPassword", hashPassword);
                cmdInsert.Parameters.AddWithValue("@staffNRIC", txtNRIC.Text);
                cmdInsert.Parameters.AddWithValue("@staffDOB", cldBOD.SelectedDate);
                cmdInsert.Parameters.AddWithValue("@staffPhoneNo", txtPhoneNo.Text);
                cmdInsert.Parameters.AddWithValue("@staffSalt", generatedSalt);
                cmdInsert.Parameters.AddWithValue("@staffRole", UserVerification.ROLE_STAFF);
                cmdInsert.Parameters.AddWithValue("@companayID", Request.Cookies["CompanyID"].Value);

                var staffID = cmdInsert.ExecuteScalar();

                if (!chkPassSet.Checked)
                {
                    string strSelect = "SELECT CompanyName FROM Company WHERE CompanyID = @companyID";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB);
                    cmdSelect.Parameters.AddWithValue("@companyID", Request.Cookies["CompanyID"].Value);
                    var companyName = cmdSelect.ExecuteScalar();

                    string verificationCode = UserVerification.getVerificationCode(staffID + txtNRIC.Text, generatedSalt);
                    //then it will generate a url to activate the account and send it to the staff
                    string verificationLink = DOMAIN_NAME + Page.ResolveUrl("~/StaffAccountActivation.aspx?VC=" + HttpUtility.UrlEncode(verificationCode));
                    string emailContent = EmailClass.populateActivationEmail((string)companyName, verificationLink);// content of the email
                    EmailClass emailClass = new EmailClass(txtEmail.Text, "Staff Account Activation", emailContent, true);

                    if (EmailClass.isCredentialed())
                    {
                        EmailCredential credential = (EmailCredential)Session["EmailCredential"];
                         emailClass.sendEmail(credential);
                    }
                    else
                    {
                        Session["tempEmail"] = emailClass;
                        Response.Redirect(ResolveUrl("~/Staff/VerifyEmail.aspx?ReturnURL="+ Request.Url.AbsoluteUri));
                    }


                }

                lblError.Text = "Successfully added";
                
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occured when register staff :" + ex.ToString();
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