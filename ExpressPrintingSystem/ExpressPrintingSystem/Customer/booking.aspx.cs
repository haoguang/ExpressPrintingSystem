using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace ExpressPrintingSystem.Customer
{
    public partial class booking : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

      

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection conTaxi;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conTaxi = new SqlConnection(connStr);
            conTaxi.Open();

            string strInsert;
            SqlCommand cmdInsert;

            strInsert = "Insert Into Document (DocumentName, DocumentType, CustomerID, Size) Values (@DocumentName, @DocumentType, @CustomerID, @Size)";
            cmdInsert = new SqlCommand(strInsert, conTaxi);

            if (FileUpload1 != null)
            {
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string contentType = FileUpload1.PostedFile.ContentType;
                int size = FileUpload1.PostedFile.ContentLength;
                cmdInsert.Parameters.AddWithValue("@DocumentName", filename);
                cmdInsert.Parameters.AddWithValue("@DocumentType", contentType);
                //cmdInsert.Parameters.AddWithValue("@DocumentURL", price.Text);
                cmdInsert.Parameters.AddWithValue("@CustomerID", txtcustomerID.Text);
                cmdInsert.Parameters.AddWithValue("@Size", size);

            }


            int n = cmdInsert.ExecuteNonQuery();

            if (n > 0)
                Response.Write("<script>alert('login successful');</script>");
            else
                Response.Write("<script>alert('failed');</script>");

            /*Close database connection*/

            conTaxi.Close();

        }
    }
}