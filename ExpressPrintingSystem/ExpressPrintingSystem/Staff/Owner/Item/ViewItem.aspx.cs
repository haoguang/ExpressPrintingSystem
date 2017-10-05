using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.Item
{
    public partial class ViewItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["Message"] != null)
            {
                if("Success".Equals(Request.QueryString["Message"]))
                    lblMessage.Text = "Successfully Edited Item.";
            }
            txtSearch.Focus();
        }
        protected void gvItemList_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                //get selected index row
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = gvItemList.Rows[index];
                string itemID = selectedRow.Cells[0].Text;

                Response.Redirect("EditItem.aspx?ItemID=" + itemID);
            }
        }
    }
}