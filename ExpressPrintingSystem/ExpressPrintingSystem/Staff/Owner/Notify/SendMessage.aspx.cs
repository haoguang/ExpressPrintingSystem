using ExpressPrintingSystem.Model.Messenging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.Notify
{
    public partial class SendMessage : System.Web.UI.Page
    {
        private const int OPERATOR_INDEX = 1; //column index for dropdownlist
        private const int VALUE_INDEX = 2; //column index for textbox value

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //set default button
                this.Form.DefaultButton = this.btnSubmit.UniqueID;

                SetInitialRow();

                rblCustomerRange.SelectedValue = "All";
                rblCustomerRange_SelectedIndexChanged(null, null);

            }

            refreshGridView();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTitle.Text;
                string content = txtMessage.Text;
                List<string> customerEmails = getCustomerList();
                string emailContent = content;
                EmailClass emailClass = new EmailClass(customerEmails, title, emailContent, false);// change false to true

                if (EmailClass.isCredentialed())
                {
                    EmailCredential credential = (EmailCredential)Session["EmailCredential"];
                    emailClass.sendEmail(credential);
                }
                else
                {
                    Session["tempEmail"] = emailClass;
                    Response.Redirect(ResolveUrl("~/Staff/VerifyEmail.aspx?ReturnURL=" + Request.Url.AbsoluteUri));
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Error occur when sending email: "+ex.Message+"')</script>");
            }
            
        }

        private bool validateParameter(string parameter, DataTable dt)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ParameterName"].Equals(parameter))
                {                  
                    return false;
                }
            }

            return true;
        }

        private List<string> getCustomerList()
        {
            List<string> customerEmails = new List<string>();
            DataTable result = null;
            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect;
                    string parameter = getParameterString();

                    if (ddlParameter.SelectedValue.Equals("Custom"))
                    {
                        if (parameter != null)
                            strSelect = "SELECT CustomerEmail FROM Customer WHERE " + getParameterString();
                        else
                            throw new Exception("Parameter might be empty.");
                    }
                    else
                    {
                        strSelect = "SELECT CustomerEmail FROM Customer";
                    }
                    

                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {

                        //add parameter for keyword

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            result = new DataTable();
                            da.Fill(result);
                        }

                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            customerEmails.Add((string)result.Rows[i]["CustomerEmail"]);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }

            return customerEmails;
        }

        private string getParameterString()
        {
            if(ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                string query = "";

                for(int i=0; i< dtCurrentTable.Rows.Count; i++)
                {
                    if (i != 0)
                        query += " AND ";
                    
                    switch ((string)dtCurrentTable.Rows[i]["ParameterName"])
                    {
                        case "Birthday Month":
                            query += "MONTH(CustomerDOB) " + dtCurrentTable.Rows[i]["Column1"].ToString() + " " + dtCurrentTable.Rows[i]["Column2"].ToString();
                            break;
                        case "Age":
                            query += "datediff(yy, CustomerDOB, GetDate()) " + dtCurrentTable.Rows[i]["Column1"].ToString() + " " + dtCurrentTable.Rows[i]["Column2"].ToString();
                            break;
                        default:
                            return null;
                    }
                }

                return query;


            }
            else
            {
                return null;
            }
        }

        private void refreshGridView()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                setRowData(dtCurrentTable);
                ViewState["CurrentTable"] = dtCurrentTable;
                gvParameter.DataSource = dtCurrentTable;
                gvParameter.DataBind();
                SetPreviousData();
            }
        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("ParameterName", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));

            dr = dt.NewRow();
            dr["ParameterName"] = "None";
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            gvParameter.DataSource = dt;
            gvParameter.DataBind();
        }

        private void setRowData(DataTable dtCurrentTable)
        {
            //update any changes in the textbox
            if (dtCurrentTable.Rows.Count > 0)
            {
                DropDownList tempDropDownList;
                TextBox tempTextBox;
                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    tempDropDownList = (DropDownList)gvParameter.Rows[i].Cells[OPERATOR_INDEX].FindControl("ddlOperator");
                    tempTextBox = (TextBox)gvParameter.Rows[i].Cells[VALUE_INDEX].FindControl("txtValue");

                    dtCurrentTable.Rows[i]["Column1"] = tempDropDownList.SelectedValue;
                    dtCurrentTable.Rows[i]["Column2"] = tempTextBox.Text;

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
                        DropDownList ddlOperator = (DropDownList)gvParameter.Rows[rowIndex].Cells[OPERATOR_INDEX].FindControl("ddlOperator");
                        TextBox txtValue = (TextBox)gvParameter.Rows[rowIndex].Cells[VALUE_INDEX].FindControl("txtValue");

                        ddlOperator.SelectedValue = dt.Rows[i]["Column1"].ToString();
                        txtValue.Text = dt.Rows[i]["Column2"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        private bool addRowToGridView()
        {
            string parameter = ddlParameter.SelectedValue;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                setRowData(dtCurrentTable);
                if (validateParameter(parameter, dtCurrentTable))
                {

                    //add new row
                    if (dtCurrentTable.Rows[0]["ParameterName"].Equals("None"))
                    {

                        dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["ParameterName"] = parameter;
                        dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Column1"] = "<";
                        dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Column2"] = "";

                    }
                    else
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["ParameterName"] = parameter;
                        drCurrentRow["Column1"] = "<";
                        drCurrentRow["Column2"] = "";
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvParameter.DataSource = dtCurrentTable;
                    gvParameter.DataBind();
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Parameter already exist in the table.')</script>");
                }


            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('ViewState is null')</script>");
            }
            SetPreviousData();
            TextBox txtValue = (TextBox)gvParameter.Rows[gvParameter.Rows.Count - 1].Cells[VALUE_INDEX].FindControl("txtValue");
            txtValue.Focus();

            return true;
        }

        protected void gvParameter_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                    dt.Rows[rowIndex]["ParameterName"] = "None";
                    dt.Rows[rowIndex]["Column1"] = string.Empty;
                    dt.Rows[rowIndex]["Column2"] = string.Empty;
                }
                ViewState["CurrentTable"] = dt;
                gvParameter.DataSource = dt;
                gvParameter.DataBind();
                SetPreviousData();
            }
        }


        protected void rblCustomerRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblCustomerRange.SelectedValue.Equals("Custom"))
            {
                customRangeControl.Visible = true;
                ddlParameter.Enabled = true;
                gvParameter.Enabled = true;
                btnAddParameter.Enabled = true;
            }
            else
            {
                customRangeControl.Visible = false;
                ddlParameter.Enabled = false;
                gvParameter.Enabled = false;
                btnAddParameter.Enabled = false;
            }
        }

        protected void btnAddParameter_Click(object sender, EventArgs e)
        {
            addRowToGridView();
        }
    }
}