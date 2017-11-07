using ExpressPrintingSystem.Model.Messenging;
using System;
using System.Collections.Generic;
using System.Linq;
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


        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            ddlEmailProvider.SelectedValue = EmailClass.getProviderName(txtEmail.Text);
        }
    }
}