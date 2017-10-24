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

namespace ExpressPrintingSystem.Staff.Printing
{
    public partial class ViewDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            populateDocumentControl();
            populateRequirementControl();
        }

        private void populateRequirementControl()
        {
            DataTable documentlistDataTable;

            if (Request.QueryString["requestlistid"] != null && Request.QueryString["documentID"] != null)
            {
                string requestlistID = ClassHashing.basicDecryption(Request.QueryString["requestlistid"]);
                string documentID = ClassHashing.basicDecryption(Request.QueryString["documentID"]);

                try
                {
                    using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                    {
                        string strSelect = "SELECT * FROM Documentlist WHERE RequestlistID = @requestlistID AND DocumentID = @documentID";

                        using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                        {

                            cmdSelect.Parameters.AddWithValue("@requestlistID", requestlistID);
                            cmdSelect.Parameters.AddWithValue("@documentID", documentID);

                            using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                            {
                                documentlistDataTable = new DataTable();
                                da.Fill(documentlistDataTable);
                            }

                        }

                        lblColor.Text = (string)documentlistDataTable.Rows[0]["DocumentColor"];
                        lblBothSide.Text = (string)documentlistDataTable.Rows[0]["DocumentBothSide"];
                        lblPaperType.Text = String.Format("{0} gsm" ,(int)documentlistDataTable.Rows[0]["DocumentPaperType"]);
                        lblQuantity.Text =  String.Format("{0}",(int)documentlistDataTable.Rows[0]["DocumentQuantity"]);
                        lblDescription.Text = (string)documentlistDataTable.Rows[0]["DocumentDescription"];



                    }

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

        private void populateDocumentControl()
        {

            DataTable documentDataTable;

            Document document;

            if(Request.QueryString["documentID"]!= null)
            {
                string documentID = ClassHashing.basicDecryption(Request.QueryString["documentID"]);
                
                try
                {
                    using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                    {
                        string strSelect = "SELECT * FROM Document WHERE DocumentID = @documentID";
                        
                        using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                        {
                            
                            cmdSelect.Parameters.AddWithValue("@documentID", documentID);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                            {
                                documentDataTable = new DataTable();
                                da.Fill(documentDataTable);
                            }

                            document = new Document((string)documentDataTable.Rows[0]["DocumentID"],(string)documentDataTable.Rows[0]["DocumentName"],(string)documentDataTable.Rows[0]["DocumentType"],(string)documentDataTable.Rows[0]["FileIDInCloud"],(string)documentDataTable.Rows[0]["CustomerID"],(int)documentDataTable.Rows[0]["Size"], (int)documentDataTable.Rows[0]["PageNumber"]);

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