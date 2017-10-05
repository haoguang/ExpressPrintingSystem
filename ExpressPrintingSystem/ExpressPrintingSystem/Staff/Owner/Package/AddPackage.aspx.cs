using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.Package
{
    public partial class AddPackage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRow();
            }

            ddlType.DataSource = ExpressPrintingSystem.Model.Entities.Package.PACKAGE_TYPE_LIST;
            ddlType.DataBind();

            cblSupport.DataSource = ExpressPrintingSystem.Model.Entities.Package.documentType;
            cblSupport.DataBind();

            if(Request.Params.Get("__EVENTTARGET") != "")
            {

                if(Request.Params.Get("__EVENTTARGET") == "DoubleClickEvent")
                {
                    addRowToGridView();
                }

            }

        }

        private bool addRowToGridView()
        {
            string itemID = ItemList.Items[ItemList.SelectedIndex].Value;
            string itemName = ItemList.Items[ItemList.SelectedIndex].Text;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows[0]["itemName"].Equals("N/A"))
                {
                    dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["itemID"] = itemID;
                    dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["itemName"] = itemName;
                    dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["quantity"] = 1;
                }
                else
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["itemID"] = itemID;
                    drCurrentRow["itemName"] = itemName;
                    drCurrentRow["quantity"] = 1;
                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
 
                ViewState["CurrentTable"] = dtCurrentTable;

                gvPackageItem.DataSource = dtCurrentTable;
                gvPackageItem.DataBind();
                
            }
            else
            {
                Response.Write("ViewState is null");
            }

            return true;
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("itemID", typeof(string)));
            dt.Columns.Add(new DataColumn("itemName", typeof(string)));
            dt.Columns.Add(new DataColumn("quantity", typeof(int)));
            dr = dt.NewRow();

            dr["itemID"] = string.Empty;
            dr["itemName"] = "N/A";
            dr["quantity"] = 0;
            dt.Rows.Add(dr);


            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            gvPackageItem.DataSource = dt;
            gvPackageItem.DataBind();
        }


    }
}