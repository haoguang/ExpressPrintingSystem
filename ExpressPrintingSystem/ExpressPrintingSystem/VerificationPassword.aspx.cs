using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem
{
    public partial class VerificationPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string verificationcode = (string)(Session["VERIFICATIONCODE"]);

                if (TextBox1.Text.Equals(verificationcode))
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Verification Successful')</script>");
                    Response.Redirect("resetPassword.aspx");

                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Verification code invalid')</script>");
                }
            }
            catch {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Please fill in the password.')</script>");
            }
           


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}