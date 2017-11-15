using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.Item
{
    public partial class EditItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populateItemToControls();
            }
            
        }

        private void populateItemToControls()
        {
            DataTable result = null;
            try
            {
                if (Request.QueryString["ItemID"] != null) { 
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = "select ItemName, ItemPrice, ItemStockQuantity, ItemSupplier from Item where ItemID = @itemID";

                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        cmdSelect.Parameters.AddWithValue("@itemID", (string)Request.QueryString["ItemID"]);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            result = new DataTable();
                            da.Fill(result);
                        }

                            
                            lblItemID.Text = Request.QueryString["ItemID"];
                            txtName.Text = (string)result.Rows[0]["ItemName"];
                            txtPrice.Text = ((decimal)result.Rows[0]["ItemPrice"]).ToString();
                            txtQuantity.Text = ((int)result.Rows[0]["ItemStockQuantity"]).ToString();
                            txtSupplier.Text = (string)result.Rows[0]["ItemSupplier"];

                        }

                }
                }
                else
                {
                    lblError.Text = "Item might not exist in the database.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
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

                strInsert = "Update Item Set ItemName = @itemName, ItemPrice = @itemPrice, ItemStockQuantity = @itemStockQuantity, ItemSupplier = @itemSupplier where ItemID = @itemID ";

                cmdInsert = new SqlCommand(strInsert, conPrintDB);
                cmdInsert.Parameters.AddWithValue("@itemName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@itemPrice", Convert.ToDecimal(txtPrice.Text));
                cmdInsert.Parameters.AddWithValue("@itemStockQuantity", Convert.ToInt32(txtQuantity.Text));
                cmdInsert.Parameters.AddWithValue("@itemSupplier", txtSupplier.Text);
                cmdInsert.Parameters.AddWithValue("@itemID", lblItemID.Text);


                int n = cmdInsert.ExecuteNonQuery();

                Response.Redirect("ViewItem.aspx?Message=Success");

            }
            catch (Exception ex)
            {
                lblError.Text = "An error occured when adding item :" + ex.ToString();
            }


            conPrintDB.Close();
        }
    }
}