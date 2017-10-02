using ExpressPrintingSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
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
            DisplayAppropriateAuthorizationMessage();
        }

        private void DisplayAppropriateAuthorizationMessage()
        {
            if (!Page.User.Identity.IsAuthenticated)
                return;

            string redirectUrl = FormsAuthentication.GetRedirectUrl(Page.User.Identity.Name, false);

            if (string.IsNullOrEmpty(redirectUrl))
                return;


            string authorizationDeniedMessage = null;
            if (redirectUrl.Contains(UserVerification.ROLE_CUSTOMER))
            {
                authorizationDeniedMessage = "Please login to continue.";
            }
            else if (redirectUrl.Contains(UserVerification.ROLE_ADMIN))
            {
                authorizationDeniedMessage = "Only the shop owner can access to the webpage.";
            }
            else if (redirectUrl.Contains(UserVerification.ROLE_STAFF))
            {
                authorizationDeniedMessage = "Staff only. Please login as a staff to continue.";
            }
            

            if(authorizationDeniedMessage != null)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('" + authorizationDeniedMessage + "')</script>");
            }
            else
            {   
                //when user access to log in page when user is already authenticated.
                Response.Redirect("masterPageTest.aspx");//main page
            }
            

        }




        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string roles;
            string username = txtname.Text.Trim();
            string password = txtPassword.Text.Trim();
            Session["SignInType"] = "Customer";

            if (UserVerification.verifyUser(username, password, "Customer"))
            {
                //These session values are just for demo purpose to show the user details on master page
               // roles = UserVerification.GetUserRoles(username);
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

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
            btnCustomer.CssClass = "btn btn-success";
            btnCompany.CssClass = "btn btn-default";
        }

        protected void btnCompany_Click(object sender, EventArgs e)
        {
            btnCustomer.CssClass = "btn btn-default";
            btnCompany.CssClass = "btn btn-danger";
        }
    }
}