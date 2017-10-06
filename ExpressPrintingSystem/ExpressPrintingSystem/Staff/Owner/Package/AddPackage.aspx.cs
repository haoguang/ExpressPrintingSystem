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
        private const int QUANTITY_INDEX = 2; //the column index for txtQuantity
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

            //handle doubleclick for list box of ItemList
            if(Request.Params.Get("__EVENTTARGET") != "")
            {

                if(Request.Params.Get("__EVENTTARGET") == "DoubleClickEvent")
                {
                    addRowToGridView();
                }

            }

        }

        private bool validateItemPackage(string itemID, DataTable dt)
        {
            string a;
            for (int i=0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["itemID"].Equals(itemID))
                {
                    return false;
                }
            }

            return true;
        }

        private bool addRowToGridView()
        {
            string itemID = ItemList.Items[ItemList.SelectedIndex].Value;
            string itemName = ItemList.Items[ItemList.SelectedIndex].Text;

            if (ViewState["CurrentTable"] != null )
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (validateItemPackage(itemID, dtCurrentTable))
                {
                    setRowData(dtCurrentTable);

                    //add new row
                    if (dtCurrentTable.Rows[0]["itemName"].Equals("N/A"))
                    {

                        dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["itemID"] = itemID;
                        dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["itemName"] = itemName;
                        dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Column1"] = 1;

                    }
                    else
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["itemID"] = itemID;
                        drCurrentRow["itemName"] = itemName;
                        drCurrentRow["Column1"] = 1;
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvPackageItem.DataSource = dtCurrentTable;
                    gvPackageItem.DataBind();
                }
                else
                {
                    Response.Write("Item already exist in the package");
                }
                
                
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
            TextBox txtQuantity = (TextBox)gvPackageItem.Rows[gvPackageItem.Rows.Count - 1].Cells[QUANTITY_INDEX].FindControl("txtQuantity");
            txtQuantity.Focus();

            return true;
        }


        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("itemID", typeof(string)));
            dt.Columns.Add(new DataColumn("itemName", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));

            dr = dt.NewRow();
            dr["itemID"] = 0;
            dr["itemName"] = "N/A";
            dr["Column1"] = string.Empty;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            gvPackageItem.DataSource = dt;
            gvPackageItem.DataBind();
        }

        private void setRowData(DataTable dtCurrentTable)
        {
            //update any changes in the textbox
            if (dtCurrentTable.Rows.Count > 0)
            {
                TextBox tempTextbox;
                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    tempTextbox = (TextBox)gvPackageItem.Rows[i].Cells[QUANTITY_INDEX].FindControl("txtQuantity");

                    dtCurrentTable.Rows[i]["Column1"] = tempTextbox.Text;

                }
            }
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtQuantity = (TextBox)gvPackageItem.Rows[rowIndex].Cells[QUANTITY_INDEX].FindControl("txtQuantity");

                        txtQuantity.Text = dt.Rows[i]["Column1"].ToString();
                        
                        rowIndex++;
                    }
                }
            }
        }

        protected void gvPackageItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                setRowData(dt);

                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();    
                }
                else
                {
                    dt.Rows[rowIndex]["itemID"] = 0;
                    dt.Rows[rowIndex]["itemName"] = "N/A";
                    dt.Rows[rowIndex]["Column1"] = string.Empty;
                }
                ViewState["CurrentTable"] = dt;
                gvPackageItem.DataSource = dt;
                gvPackageItem.DataBind();
                SetPreviousData();
            }
        }
    }
}