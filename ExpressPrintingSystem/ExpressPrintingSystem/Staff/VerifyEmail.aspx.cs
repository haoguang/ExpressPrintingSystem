using ExpressPrintingSystem.Model.Messenging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff
{
    public partial class VerifyEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Form.DefaultButton = this.btnSubmit.UniqueID;
                ddlEmailProvider.Items.Insert(0, new ListItem("Please select", ""));
                ddlEmailProvider.Items[0].Selected = true;
                ddlEmailProvider.Items[0].Attributes["disabled"] = "disabled";
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            int abc = ddlEmailProvider.SelectedIndex;

            if(Session["tempEmail"] != null)
            {
                EmailClass emailClass = (EmailClass)Session["tempEmail"];

                try
                {
                    EmailCredential emailCredential = new EmailCredential(new NetworkCredential(email, password), EmailClass.getStmpClient(ddlEmailProvider.SelectedValue));
                    emailClass.sendEmail(emailCredential);
                    EmailClass.generateCredential(email, password, EmailClass.getStmpClient(ddlEmailProvider.SelectedValue));
                    
                    string url = Request.QueryString["ReturnUrl"];
                    Response.Write(" <script language = 'javascript'> window.alert('Email has been sent. Redirecting previous page.'); window.location = '"+url+"';</script>");
                    
                }
                catch (SmtpFailedRecipientsException stmpRecipientsException)
                {
                    lblError.Text = "Email failed to send to " + stmpRecipientsException.FailedRecipient;
                }
                catch (SmtpException smtpException)
                {
                    lblError.Text = "The email or password might typed wrong or " + smtpException.Message;
                }
            }
            else
            {
                lblError.Text = "There is no email to be send, credential is not created.";
            }
             

        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            ddlEmailProvider.SelectedValue = EmailClass.getProviderName(txtEmail.Text);
        }
    }
}