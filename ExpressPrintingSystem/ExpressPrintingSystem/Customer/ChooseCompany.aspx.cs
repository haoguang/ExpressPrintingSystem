using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Customer
{
    public partial class ChooseCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string companyID = DropDownList1.SelectedValue;

            Response.Redirect("~/booking.aspx?CompanyID=" + companyID);
            // Response.Redirect("PaymentForm.aspx?PickupAdd=" + txtPUA.Text + "&Destination=" + txtDA.Text + "&ReservationID=" + lblID.Text + "&taxiID=" + lblTaxi.Text);

        }
    }
}