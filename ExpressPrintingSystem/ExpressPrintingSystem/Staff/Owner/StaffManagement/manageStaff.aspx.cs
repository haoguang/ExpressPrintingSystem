using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.StaffManagement
{
    public partial class manageStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Message"] != null)
            {
                if ("Success".Equals(Request.QueryString["Message"]))
                    lblMessage.Text = "Successfully Updated staff.";
            }
            txtSearch.Focus();
        }

        protected void gvStaffList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                //get selected index row
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = gvStaffList.Rows[index];
                string staffID = selectedRow.Cells[0].Text;

                Response.Redirect("editStaff.aspx?staffID=" + HttpUtility.UrlEncode(ClassHashing.basicEncryption(staffID)));
            }
        }


    }
}