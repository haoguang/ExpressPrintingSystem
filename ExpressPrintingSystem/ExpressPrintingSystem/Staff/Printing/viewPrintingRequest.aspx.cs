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

namespace ExpressPrintingSystem.Staff.Printing
{
    public partial class viewPrintingRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private List<Request> getRequestList(string status, string companyID)
        {
            List<Request> requestList = new List<Request>();

            DataTable requestResult = null;


            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = "SELECT * FROM Requestlist rl, Request r WHERE rl.RequestID = r.RequestID AND rl.RequestStatus = @status AND r.CompanyID = @companyID";

                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        
                        cmdSelect.Parameters.AddWithValue("@status", status);
                        cmdSelect.Parameters.AddWithValue("@companyID", companyID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            requestResult = new DataTable();
                            da.Fill(requestResult);
                        }

                        for(int i=0; i< requestResult.Rows.Count; i++)
                        {
                            Requestlist newRequestlist = new Requestlist((string)requestResult.Rows[i]["rl.RequestlistID"], (string)requestResult.Rows[i]["rl.RequestItemID"], (string)requestResult.Rows[i]["rl.RequestStatus"], (string)requestResult.Rows[i]["rl.RequestType"],getDocumentList((string)requestResult.Rows[i]["rl.RequestlistID"]));
                            List<Requestlist> requestlistArray = new List<Requestlist>();
                            requestlistArray.Add(newRequestlist);
                            Request request = new Model.Entities.Request((string)requestResult.Rows[i]["r.RequestID"],Convert.ToDateTime(requestResult.Rows[i]["r.RequestDateTime"]), Convert.ToDateTime(requestResult.Rows[i]["r.DueDateTime"]), null, (string)requestResult.Rows[i]["r.CompanyID"], (string)requestResult.Rows[i]["r.CustomerID"], requestlistArray);

                            requestList.Add(request);
                        }

                      

                    }
                }
                return requestList;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private List<Documentlist> getDocumentList(string requestlistID)
        {
            List<Documentlist> documentList = new List<Documentlist>();

            DataTable documentResult = null;


            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect= "SELECT * FROM Documentlist dl, Document d WHERE dl.DocumentID = d.DocumentID AND RequestlistID = @requestlistID";

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
                            Document document = new Document((string)documentResult.Rows[i]["d.DocumentID"], (string)documentResult.Rows[i]["d.DocumentName"], (string)documentResult.Rows[i]["d.DocumentType"], (string)documentResult.Rows[i]["d.FileIDInCloud"], (string)documentResult.Rows[i]["d.CustomerID"], (int)documentResult.Rows[i]["d.Size"], (int)documentResult.Rows[i]["d.PageNumber"]);
                            Documentlist newDocumentlist = new Documentlist(document, (int)documentResult.Rows[i]["dl.Sequences"], (string)documentResult.Rows[i]["dl.DocumentColor"], (string)documentResult.Rows[i]["dl.DocumentBothSide"], (string)documentResult.Rows[i]["dl.DocumentPaperType"], (int)documentResult.Rows[i]["dl.DocumentQuantity"], (string)documentResult.Rows[i]["dl.DocumentDescription"]);

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