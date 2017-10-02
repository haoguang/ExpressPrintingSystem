using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Customer
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
            
            btnCustomer.CssClass = "btn btn-success";
            btnCompany.CssClass = "btn btn-default";
            // Response.Redirect("SignUp.aspx");
        }

        protected void btnCompany_Click(object sender, EventArgs e)
        {

           
            btnCustomer.CssClass = "btn btn-default";
            btnCompany.CssClass = "btn btn-danger";
        }
    }
}