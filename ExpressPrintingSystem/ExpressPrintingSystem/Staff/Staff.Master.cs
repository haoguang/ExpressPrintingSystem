using ExpressPrintingSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff
{
    public partial class Staff : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            verifyUser();
        }

        private void verifyUser()
        {
            if(Session["UserInfo"] != null)
            {
                User user = (User)Session["UserInfo"];
                initUserMenu();
                userInfoControl.Text = "Welcome, " + user.Name;
                userInfoControl.NavigateUrl = "";

            }
            else
            {
                FormsAuthentication.SignOut();
                userInfoControl.Text = "Sign In";
                userInfoControl.NavigateUrl = "";
            }
        }

        private void initUserMenu()
        {
            
            //init Profile Button
            HtmlGenericControl li = new HtmlGenericControl("li");
            userMenu.Controls.Add(li);
            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("href", "#");//profile url
            anchor.Attributes.Add("runat", "server");
            anchor.Attributes.Add("onserverclick", "LogOut_Click");
            anchor.InnerText = "Profile";
            li.Controls.Add(anchor);

            //init Logout Button
            li = new HtmlGenericControl("li");
            userMenu.Controls.Add(li);
            LinkButton btn_Logout = new LinkButton();
            btn_Logout.Click += LogOut_Click;
            btn_Logout.ID = "btnLogOut";
            btn_Logout.Text = "Log Out";
            btn_Logout.Attributes.Add("runat", "server");
            li.Controls.Add(btn_Logout);


        }

        protected void LogOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}