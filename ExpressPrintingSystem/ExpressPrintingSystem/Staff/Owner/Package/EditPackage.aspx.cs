﻿using ExpressPrintingSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.Package
{
    public partial class EditPackage : System.Web.UI.Page
    {
        private const int QUANTITY_INDEX = 2; //the column index for txtQuantity
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSubmit.UniqueID;
            if (!Page.IsPostBack)
            {
                SetInitialRow();
                

                ddlType.DataSource = ExpressPrintingSystem.Model.Entities.Package.PACKAGE_TYPE_LIST;
                ddlType.DataBind();

                cblSupport.DataSource = ExpressPrintingSystem.Model.Entities.Package.documentType;
                cblSupport.DataBind();

                retrieveItemList();
                populateDataToControls();
                ddlType_SelectedIndexChanged(null, null);
            }

            refreshGridView(); //refresh table

            //handle doubleclick for list box of ItemList
            if (Request.Params.Get("__EVENTTARGET") != "")
            {

                if (Request.Params.Get("__EVENTTARGET") == "DoubleClickEvent")
                {
                    addRowToGridView();
                }

            }

            disableEmptyTable();//if table has no item disable it
        }

        private void populateDataToControls()
        {
            if (Request.QueryString["PackageID"] != null)
            {
                string packageid = ClassHashing.basicDecryption(Request.QueryString["PackageID"]);

                DataTable packageResult = null;
                DataTable itemsResult = null;

                try
                {
                    using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                    {
                        string strSelect = "SELECT * FROM Package WHERE PackageID = @packageid";

                        using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                        {
                            cmdSelect.Parameters.AddWithValue("@packageid", packageid);

                            using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                            {
                                packageResult = new DataTable();
                                da.Fill(packageResult);
                            }

                        }

                        if (packageResult != null)
                        {

                                itemsResult = null;

                                strSelect = "SELECT i.ItemID, i.ItemName, i.ItemPrice, i.ItemStockQuantity, i.ItemSupplier, p.Quantity FROM Item i, PackageItem p WHERE i.ItemID = p.ItemID AND p.PackageID = @packageID";

                                using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                                {
                                    cmdSelect.Parameters.Clear();
                                    cmdSelect.Parameters.AddWithValue("@packageID", packageid);

                                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                                    {
                                        itemsResult = new DataTable();
                                        da.Fill(itemsResult);
                                    }
                                }
                            Model.Entities.Package package;
                                List<PackageItems> packageItems = new List<PackageItems>();

                            if (itemsResult != null)
                            {
                                for (int j = 0; j < itemsResult.Rows.Count; j++)
                                {
                                    packageItems.Add(new PackageItems(new Model.Entities.Item((string)itemsResult.Rows[j]["ItemID"], (string)itemsResult.Rows[j]["ItemName"], (decimal)itemsResult.Rows[j]["ItemPrice"],
                                        (int)itemsResult.Rows[j]["ItemStockQuantity"], (string)itemsResult.Rows[j]["ItemSupplier"]), (int)itemsResult.Rows[j]["Quantity"]));
                                }

                                package = new Model.Entities.Package((string)packageResult.Rows[0]["PackageID"], (string)packageResult.Rows[0]["PackageName"], (decimal)packageResult.Rows[0]["PackagePrice"], (string)packageResult.Rows[0]["PackageSupport"], (string)packageResult.Rows[0]["PackageType"], (decimal)packageResult.Rows[0]["PrintingPricePerPaper"], packageItems);
                                }
                                else
                                {
                                    package = new Model.Entities.Package((string)packageResult.Rows[0]["PackageID"], (string)packageResult.Rows[0]["PackageName"], (decimal)packageResult.Rows[0]["PackagePrice"], (string)packageResult.Rows[0]["PackageSupport"], (string)packageResult.Rows[0]["PackageType"], (decimal)packageResult.Rows[0]["PrintingPricePerPaper"]);
                                }

                            lblPackageID.Text = package.PackageID;
                            txtName.Text = package.PackageName;
                            txtPrice.Text = String.Format("{0:0.00}", package.PackagePrice);
                            ddlType.SelectedValue = package.PackageType;

                            if (ddlType.SelectedValue.Equals(Model.Entities.Package.TYPE_PRINTING))
                            {
                                string[] documentSupport = package.PackageSupport.Split(';');

                                foreach(string document in documentSupport)
                                {
                                    for(int i=0; i<cblSupport.Items.Count; i++)
                                    {
                                        if (cblSupport.Items[i].ToString().Equals(document))
                                        {
                                            cblSupport.Items[i].Selected = true;
                                        }
                                    }
                                }

                                txtPricePerPaper.Text = String.Format("{0:0.00}", package.PrintingPrice);
                            }

                            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                            DataRow drCurrentRow = null;

                            foreach(PackageItems packageitem in package.PackageItems)
                            {
                                //add new row
                                if (dtCurrentTable.Rows[0]["itemName"].Equals("N/A"))
                                {

                                    dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["itemID"] = packageitem.Item.ItemID;
                                    dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["itemName"] = packageitem.Item.ItemName;
                                    dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Column1"] = packageitem.Quantity;

                                }
                                else
                                {
                                    drCurrentRow = dtCurrentTable.NewRow();
                                    drCurrentRow["itemID"] = packageitem.Item.ItemID;
                                    drCurrentRow["itemName"] = packageitem.Item.ItemName;
                                    drCurrentRow["Column1"] = packageitem.Quantity;
                                    dtCurrentTable.Rows.Add(drCurrentRow);
                                }
                            }
                            ViewState["CurrentTable"] = dtCurrentTable;

                            gvPackageItem.DataSource = dtCurrentTable;
                            gvPackageItem.DataBind();
                            SetPreviousData();

                        }

                        

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            else
            {
                lblError.Text = "Invalid PackageID. Please reselect your package to edit.";
            }
        }

        private bool validateItemPackage(string itemID, DataTable dt)
        {
            string a;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["itemID"].Equals(itemID))
                {
                    return false;
                }
            }

            return true;
        }

        private void refreshGridView()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                setRowData(dtCurrentTable);
                ViewState["CurrentTable"] = dtCurrentTable;
                gvPackageItem.DataSource = dtCurrentTable;
                gvPackageItem.DataBind();
                SetPreviousData();
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
                setRowData(dtCurrentTable);
                if (validateItemPackage(itemID, dtCurrentTable))
                {

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
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Item already exist in the package.')</script>");
                }


            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('ViewState is null')</script>");
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

        private void disableEmptyTable()
        {
            if (gvPackageItem.Rows[0].Cells[1].Text.Equals("N/A"))
            {
                TextBox txtQuantity = (TextBox)gvPackageItem.Rows[0].Cells[QUANTITY_INDEX].FindControl("txtQuantity");
                txtQuantity.Text = "1";
                txtQuantity.Enabled = false;
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
                    lblEstimatedPrice.Text = "0";
                }
                ViewState["CurrentTable"] = dt;
                gvPackageItem.DataSource = dt;
                gvPackageItem.DataBind();
                SetPreviousData();
                disableEmptyTable();
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue.Equals(ExpressPrintingSystem.Model.Entities.Package.TYPE_PRINTING))
            {
                trPricePerPaper.Visible = true;
                trSupport.Visible = true;
                txtPricePerPaper.Enabled = true;
                txtPricePerPaper.CausesValidation = true;
                cblSupport.Enabled = true;
                cblSupport.CausesValidation = true;
            }
            else
            {
                trPricePerPaper.Visible = false;
                trSupport.Visible = false;
                txtPricePerPaper.Enabled = false;
                txtPricePerPaper.CausesValidation = false;
                cblSupport.Enabled = false;
                cblSupport.CausesValidation = false;
            }
        }

        protected void gvPackageItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            updateLabel();
        }

        private void retrieveItemList()
        {
            DataTable result = null;
            using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
            {
                string strSelect = "Select * From Item";

                List<ExpressPrintingSystem.Model.Entities.Item> itemList = new List<ExpressPrintingSystem.Model.Entities.Item>();


                using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                {

                    using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                    {
                        result = new DataTable();
                        da.Fill(result);
                    }

                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        itemList.Add(new Model.Entities.Item((string)result.Rows[i]["ItemID"], (string)result.Rows[i]["ItemName"], (decimal)result.Rows[i]["ItemPrice"], (int)result.Rows[i]["ItemStockQuantity"], (string)result.Rows[i]["ItemSupplier"]));
                    }

                    Session["itemList"] = itemList;
                }
            }
        }

        protected void gvPackageItem_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            updateLabel();
        }

        private void updateLabel()
        {
            //calculate and update estimated price label
            if (Session["itemList"] == null)
            {
                retrieveItemList();
            }

            List<ExpressPrintingSystem.Model.Entities.Item> itemList = (List<ExpressPrintingSystem.Model.Entities.Item>)Session["itemList"];

            decimal estimatedPrice = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (!((string)dt.Rows[0]["itemID"]).Equals("0"))
                {
                    ExpressPrintingSystem.Model.Entities.Item tempItem = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tempItem = null;

                        string itemID = (string)dt.Rows[i]["itemID"];

                        for (int j = 0; j < itemList.Count; j++)
                        {
                            if (itemID.Equals(itemList[j].ItemID))
                            {
                                tempItem = itemList[j];
                                break;
                            }
                        }

                        if (tempItem != null)
                        {
                            estimatedPrice += tempItem.ItemPrice * Convert.ToInt32(((string)dt.Rows[i]["Column1"]).Equals("") ? "0" : (string)dt.Rows[i]["Column1"]);
                        }

                    }

                    lblEstimatedPrice.Text = String.Format("{0:0.00}", estimatedPrice);
                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("ViewPackage.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            refreshGridView();

            try
            {
                string packageID = lblPackageID.Text;
                SqlConnection conPrint;
                string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                conPrint = new SqlConnection(connStr);
                conPrint.Open();

                string strInsert;
                SqlCommand cmdInsert;

                strInsert = "UPDATE Package SET PackageName=@packageName, PackagePrice=@packagePrice, PackageSupport=@packageSupport, PackageType=@packageType, PrintingPricePerPaper=@printingPricePerPaper WHERE PackageID = @packageID";


                cmdInsert = new SqlCommand(strInsert, conPrint);

                cmdInsert.Parameters.AddWithValue("@packageID", packageID);
                cmdInsert.Parameters.AddWithValue("@packageName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@packagePrice", txtPrice.Text);

                string packageSupport = "";
                decimal printingPaperPrice;

                if (ddlType.SelectedValue.Equals(ExpressPrintingSystem.Model.Entities.Package.TYPE_PRINTING))
                {
                    foreach (ListItem item in cblSupport.Items)
                        if (item.Selected) packageSupport += item.Value + ";";

                    printingPaperPrice = Convert.ToDecimal(txtPricePerPaper.Text);
                }
                else
                {
                    packageSupport = "";
                    printingPaperPrice = 0;
                }

                cmdInsert.Parameters.AddWithValue("@packageType", ddlType.SelectedValue);

                cmdInsert.Parameters.AddWithValue("@packageSupport", packageSupport);
                cmdInsert.Parameters.AddWithValue("@printingPricePerPaper", printingPaperPrice);

                cmdInsert.ExecuteScalar();

                if (packageID != null && ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];

                    string strDeleteRows = "DELETE FROM PackageItem WHERE PackageID = @packageID";
                    SqlCommand cmdDelete = new SqlCommand(strDeleteRows, conPrint);
                    cmdDelete.Parameters.AddWithValue("@packageID", packageID);
                    cmdDelete.ExecuteNonQuery();

                    string strInsertPackageItem = "Insert into PackageItem (PackageID, ItemID, Quantity) values (@packageID, @itemID, @quantity)";
                    SqlCommand cmdPackageItemInsert = new SqlCommand(strInsertPackageItem, conPrint);

                    if (!dt.Rows[0]["itemName"].Equals("N/A"))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            cmdPackageItemInsert.Parameters.Clear();
                            cmdPackageItemInsert.Parameters.AddWithValue("@packageID", packageID);
                            cmdPackageItemInsert.Parameters.AddWithValue("@itemID", (string)dt.Rows[i]["itemID"]);
                            cmdPackageItemInsert.Parameters.AddWithValue("@quantity", Convert.ToInt32((string)dt.Rows[i]["Column1"]));
                            cmdPackageItemInsert.ExecuteNonQuery();
                        }
                    }
                        
                }

                conPrint.Close();

                Response.Redirect("ViewPackage.aspx");
            }
            catch (SqlException ex)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Something gone wrong with the database. Please contact the administrator for the problem.')</script>");
            }
        }
    }
}