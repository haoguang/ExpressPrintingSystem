using ExpressPrintingSystem.Model;
using ExpressPrintingSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FormsAuthenticationExtensions;
using System.Data.SqlClient;

namespace ExpressPrintingSystem.Customer
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSubmit.UniqueID;
            DisplayAppropriateAuthorizationMessage();

            if(Request.Cookies["me"] != null)
            {
                txtname.Text = ClassHashing.basicDecryption((string)Request.Cookies["me"].Value);
                CheckBox1.Checked = true;
            }
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

        private void rememberMe()
        {
            if (CheckBox1.Checked)
            {
                var rememberUser = new HttpCookie("me");
                rememberUser.Value = ClassHashing.basicEncryption(txtname.Text.Trim());
                Response.Cookies.Add(rememberUser);
            }
            else
            {
                if(Request.Cookies["me"] != null)
                {
                    Request.Cookies["me"].Expires = DateTime.Now.AddDays(-1);
                }
            }
        }

        private void setCompanyCookie(string toggleOption, string id)
        {
            if (toggleOption.Equals(UserVerification.ROLE_STAFF))
            {
                SqlConnection conPrint;


                string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                conPrint = new SqlConnection(connStr);

                conPrint.Open();

                string strInsert;
                SqlCommand cmdInsert;
                strInsert = "SELECT CompanyID from CompanyStaff where StaffID = @staffID";


                cmdInsert = new SqlCommand(strInsert, conPrint);
                cmdInsert.Parameters.AddWithValue("@staffID", id);

                var CompanyId = cmdInsert.ExecuteScalar();

                var company = new HttpCookie("CompanyID");
                company.Value = CompanyId.ToString();
                company.Expires = DateTime.Now.AddMinutes(480);
                Response.Cookies.Add(company);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string roles;
            string username = txtname.Text.Trim();
            string password = txtPassword.Text.Trim();
            string toggleOption = toggleBtn_optionSelected();

            var myCookie = new HttpCookie("UserCookie");//instantiate an new cookie and give it a name
            myCookie.Values.Add("SignInType", ClassHashing.basicEncryption(toggleOption));//populate it with key, value pairs
            myCookie.Expires = DateTime.Now.AddMinutes(481);

            if (UserVerification.verifyUser(username, password, toggleOption))
            {
                //These session values are just for demo purpose to show the user details on master page
                roles = UserVerification.GetUserRoles(username, toggleOption);

                User user = UserVerification.getUserBasicInfo(username, toggleOption);
                myCookie.Values.Add("UserInfo", ClassHashing.basicEncryption(ExpressPrintingSystem.Model.Entities.User.toCompactString(user)));
                Response.Cookies.Add(myCookie);
                setCompanyCookie(toggleOption, user.ID);
                //Let us now set the authentication cookie so that we can use that later.
                FormsAuthentication.SetAuthCookie(username, false);
                //Login successful lets put him to requested page
                string returnUrl = Request.QueryString["ReturnUrl"] as string;

                rememberMe();

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
            else if(UserVerification.isActivatedUser(username, toggleOption))
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Your account is not activated, please check your social media for account activation link.')</script>");
            }
            else
            {
                txtname.Text = "Login Failed";//temporary, for testing purpose only
            }

            Response.Cookies.Add(myCookie);
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

        private string toggleBtn_optionSelected()
        {
            if (btnCustomer.CssClass.Equals("btn btn-default"))
                return UserVerification.ROLE_STAFF;
            else
                return UserVerification.ROLE_CUSTOMER;
        }
    }
}