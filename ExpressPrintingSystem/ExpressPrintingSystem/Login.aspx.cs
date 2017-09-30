using ExpressPrintingSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Customer
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
       

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string roles;
            string username = txtname.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (UserVerification.verifyUser(username, password))
            {
                //These session values are just for demo purpose to show the user details on master page
                roles = UserVerification.GetUserRoles(username);
                //Session["User"] = username;
                //Session["Roles"] = roles;

                //Let us now set the authentication cookie so that we can use that later.
                FormsAuthentication.SetAuthCookie(username, false);

                //Login successful lets put him to requested page
                string returnUrl = Request.QueryString["ReturnUrl"] as string;

                if (returnUrl != null)
                {
                    Response.Redirect(returnUrl);
                }
                else
                {
                    //no return URL specified so lets kick him to home page
                    Response.Redirect("masterPageTest.aspx");
                }
            }
            else
            {
                txtname.Text = "Login Failed";//temporary, for testing purpose only
            }
        }
    }
}