using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Customer
{
    public partial class CompanySignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
           
              
                    Response.Redirect("SignUp.aspx");
         

            //btnCustomer.CssClass = "btn btn-success";
           // btnCompany.CssClass = "btn btn-default";
        }

        protected void btnCompany_Click(object sender, EventArgs e)
        {
            //btnCustomer.CssClass = "btn btn-default";
           // btnCompany.CssClass = "btn btn-danger";
           
                Response.Redirect("CompanySignUp.aspx");
            
           

        }
    }
}