using ExpressPrintingSystem.Model;
using ExpressPrintingSystem.Model.Entities;
using GleamTech.DocumentUltimate.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Customer.CustomerFile
{
    public partial class ViewFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateDocumentControl();
        }


        private void populateDocumentControl()
        {

            DataTable documentDataTable;

            Document document;

            if (Request.QueryString["documentID"] != null)
            {
                // string documentID = ClassHashing.basicDecryption(Request.QueryString["documentID"]);
                string documentID = Request.QueryString["documentID"];

                try
                {
                    using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                    {
                        string strSelect = "SELECT * FROM Document WHERE DocumentID = @DocumentID";

                        using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                        {

                            cmdSelect.Parameters.AddWithValue("@DocumentID", documentID);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                            {
                                documentDataTable = new DataTable();
                                da.Fill(documentDataTable);
                            }

                            document = new Document((string)documentDataTable.Rows[0]["DocumentID"], (string)documentDataTable.Rows[0]["DocumentName"], (string)documentDataTable.Rows[0]["DocumentType"], (string)documentDataTable.Rows[0]["FileIDInCloud"], (string)documentDataTable.Rows[0]["CustomerID"], (int)documentDataTable.Rows[0]["Size"], (int)documentDataTable.Rows[0]["PageNumber"]);

                        }
                    }
                    documentViewer.DocumentSource = new DocumentSource(
                    new DocumentInfo(document.FileIDInCloud, document.DocumentName), backblaze.downloadFileIntoBytes(document.FileIDInCloud));

                }
                catch (Exception ex)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "There is a problem occur when processing the document. Please try again later";
                }


            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "System cannot find any document. Please retry.";
            }

        }

    }



}