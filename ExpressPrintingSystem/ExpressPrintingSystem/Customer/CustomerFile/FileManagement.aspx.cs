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

namespace ExpressPrintingSystem.Customer.CustomerFile
{
    public partial class FileManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private List<Documentlist> getDocumentList(string requestlistID)
        {
            List<Documentlist> documentList = new List<Documentlist>();

            DataTable documentResult = null;


            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = "SELECT * FROM Documentlist dl, Document d WHERE dl.DocumentID = d.DocumentID AND RequestlistID = @requestlistID ORDER BY Sequences";

                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {

                        cmdSelect.Parameters.AddWithValue("@requestlistID", requestlistID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            documentResult = new DataTable();
                            da.Fill(documentResult);
                        }

                        for (int i = 0; i < documentResult.Rows.Count; i++)
                        {
                            Document document = new Document((string)documentResult.Rows[i]["DocumentID"], (string)documentResult.Rows[i]["DocumentName"], (string)documentResult.Rows[i]["DocumentType"], (string)documentResult.Rows[i]["FileIDInCloud"], (string)documentResult.Rows[i]["CustomerID"], (int)documentResult.Rows[i]["Size"], (int)documentResult.Rows[i]["PageNumber"]);
                            Documentlist newDocumentlist = new Documentlist(document, (int)documentResult.Rows[i]["Sequences"], (string)documentResult.Rows[i]["DocumentColor"], (string)documentResult.Rows[i]["DocumentBothSide"], (int)documentResult.Rows[i]["DocumentPaperType"], (int)documentResult.Rows[i]["DocumentQuantity"], documentResult.Rows[i]["DocumentDescription"].ToString());
                            newDocumentlist.RequestlistID = requestlistID;

                            documentList.Add(newDocumentlist);
                        }

                    }
                }
                return documentList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}