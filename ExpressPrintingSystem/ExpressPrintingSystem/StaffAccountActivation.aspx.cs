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
    public partial class StaffAccountActivation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["VC"] != null)
                    {
                        string verificationCode = Request.QueryString["VC"];
                        string staffID = UserVerification.activateStaff(verificationCode);

                        if (staffID != null)
                        {
                            showPassForm();
                            messageHolder.Visible = false;
                            ViewState["StaffID"] = ClassHashing.basicEncryption(staffID);
                        }
                        else
                        {
                            lblMessage.Text = "The link is not valid. Please make sure you get the right link.";
                            messageHolder.Visible = true;
                            hidePassForm();
                        }

                    }
                    else
                    {
                        lblMessage.Text = "The link is not valid. Please make sure you get the right link.";
                        messageHolder.Visible = true;
                        hidePassForm();
                    }
                } catch(Exception ex)
                {
                    lblMessage.Text = "The link is not valid. Please make sure you get the right link.";
                    messageHolder.Visible = true;
                    hidePassForm();
                }
                
            }
            
        }

        private void hidePassForm()
        {
            passwordForm.Visible = false;
            txtPassword.Enabled = false;
            txtConfirmPassword.Enabled = false;
        }

        private void showPassForm()
        {
            passwordForm.Visible = true;
            txtPassword.Enabled = true;
            txtConfirmPassword.Enabled = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(ViewState["StaffID"] != null)
            {
                SqlConnection conPrintDB;
                string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                conPrintDB = new SqlConnection(connStr);
                conPrintDB.Open();
                try
                {
                    string strUpdate;
                    SqlCommand cmdUpdate;

                    strUpdate = "Update CompanyStaff SET StaffPassword = @password ,StaffSalt = @salt WHERE StaffID = @staffID";

                    byte[] generatedSalt = ClassHashing.generateSalt();
                    byte[] hashPassword = ClassHashing.generateSaltedHash(txtPassword.Text, generatedSalt);


                    cmdUpdate = new SqlCommand(strUpdate, conPrintDB);
                    cmdUpdate.Parameters.AddWithValue("@password", hashPassword);
                    cmdUpdate.Parameters.AddWithValue("@salt", generatedSalt);
                    cmdUpdate.Parameters.AddWithValue("@staffID", ClassHashing.basicDecryption((string)ViewState["StaffID"]));


                    int n = cmdUpdate.ExecuteNonQuery();
                    if (n > 0)
                    {
                        Response.Write("<script LANGUAGE='JavaScript' >alert('Successfully activated your account. Redirecting to login page.')</script>");
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Unable to update account inforation. Please try again later.";
                        messageHolder.Visible = true;
                    }
                    

                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occured when setting the password to the account :" + ex.ToString();
                    messageHolder.Visible = true;
                }
                finally
                {
                    conPrintDB.Close();
                }
            }
            else
            {
                lblMessage.Text = "Unable to retrieve account inforation. Please try again later.";
                messageHolder.Visible = true;
            }
            
        }
    }
}