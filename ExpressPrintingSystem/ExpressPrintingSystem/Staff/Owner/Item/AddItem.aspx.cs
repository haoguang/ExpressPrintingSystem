using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ExpressPrintingSystem.Staff.Owner.Item
{
    public partial class AddItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            //set default button
            this.Form.DefaultButton = this.btnSubmit.UniqueID;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection conPrintDB;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conPrintDB = new SqlConnection(connStr);
            conPrintDB.Open();
            try
            {
                string strInsert;
                SqlCommand cmdInsert;

                strInsert = "Insert Into Item (ItemName, ItemPrice, ItemStockQuantity, ItemSupplier, CompanyID) Values (@itemName, @itemPrice, @itemStockQuantity, @itemSupplier, @companyID)";

                cmdInsert = new SqlCommand(strInsert, conPrintDB);
                cmdInsert.Parameters.AddWithValue("@itemName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@itemPrice", Convert.ToDecimal(txtPrice.Text) );
                cmdInsert.Parameters.AddWithValue("@itemStockQuantity", Convert.ToInt32(txtQuantity.Text));
                cmdInsert.Parameters.AddWithValue("@itemSupplier", txtSupplier.Text);
                cmdInsert.Parameters.AddWithValue("@companyID", Request.Cookies["CompanyID"].Value);


                int n = cmdInsert.ExecuteNonQuery();

                Response.Redirect("ViewItem.aspx");

            }
            catch (Exception ex)
            {
                lblError.Text = "An error occured when adding item :" + ex.ToString();
            }
            finally
            {
                conPrintDB.Close();
            }


            
        }
    }
}