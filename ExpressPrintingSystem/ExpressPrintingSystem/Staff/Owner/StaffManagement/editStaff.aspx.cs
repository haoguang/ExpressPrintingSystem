using ExpressPrintingSystem.Model;
using ExpressPrintingSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.StaffManagement
{
    public partial class editStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSubmit.UniqueID;
            if (!Page.IsPostBack)
                populateStaffToControls();
        }

        private void populateStaffToControls()
        {
            DataTable result = null;
            try
            {
                if (Request.QueryString["staffID"] != null)
                {
                    using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                    {
                        string strSelect = "select StaffName, StaffEmail, StaffNRIC, StaffDOB, StaffPhoneNo from CompanyStaff where StaffID = @staffID";
                        string encryptedText = (string)Request.QueryString["staffID"];
                        string staffID = ClassHashing.basicDecryption(encryptedText);
                        using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                        {
                            cmdSelect.Parameters.AddWithValue("@staffID", staffID);

                            using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                            {
                                result = new DataTable();
                                da.Fill(result);
                            }


                            lblStaffID.Text = staffID;
                            txtName.Text = (string)result.Rows[0]["StaffName"];
                            txtEmail.Text = (string)result.Rows[0]["StaffEmail"];
                            txtNRIC.Text = (string)result.Rows[0]["StaffNRIC"];
                            txtPhoneNo.Text = (string)result.Rows[0]["StaffPhoneNo"];
                            cldBOD.SelectedDate = Convert.ToDateTime(result.Rows[0]["StaffDOB"]);
                            cldBOD.VisibleDate = Convert.ToDateTime(result.Rows[0]["StaffDOB"]);

                        }

                    }
                }
                else
                {
                    lblError.Text = "The system could not found any record related to the staff.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string returnUrl = Request.QueryString["ReturnUrl"] as string;
            Response.Redirect(returnUrl);
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

                strInsert = "Update CompanyStaff SET StaffName = @staffName, StaffEmail = @staffEmail, StaffNRIC = @staffNRIC, StaffDOB = @staffDOB, StaffPhoneNo = @staffPhoneNo WHERE StaffID = @staffID";

                cmdInsert = new SqlCommand(strInsert, conPrintDB);
                cmdInsert.Parameters.AddWithValue("@staffName", txtName.Text);
                cmdInsert.Parameters.AddWithValue("@staffEmail", txtEmail.Text);
                cmdInsert.Parameters.AddWithValue("@staffNRIC", txtNRIC.Text);
                cmdInsert.Parameters.AddWithValue("@staffDOB", cldBOD.SelectedDate);
                cmdInsert.Parameters.AddWithValue("@staffPhoneNo", txtPhoneNo.Text);
                cmdInsert.Parameters.AddWithValue("@staffID", lblStaffID.Text);

                int n = cmdInsert.ExecuteNonQuery();
                if (n > 0)
                {
                    lblError.Text = "Successfully updated";
                    Response.Redirect("manageStaff.aspx?Message=Success");
                }
                else
                    lblError.Text = "Problem occur when updating the staff detail.";
                
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