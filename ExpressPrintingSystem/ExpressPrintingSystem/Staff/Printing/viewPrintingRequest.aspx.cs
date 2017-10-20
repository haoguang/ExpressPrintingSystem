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
            List<ExpressPrintingSystem.Model.Entities.Request> requestList = getRequestList(Requestlist.STATUS_PENDING, Request.Cookies["CompanyID"].Value);
            lvRequestConfirmation.DataSource = requestList;
            lvRequestConfirmation.DataBind();

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
                            Requestlist newRequestlist = new Requestlist((string)requestResult.Rows[i]["RequestlistID"], (string)requestResult.Rows[i]["RequestItemID"], (string)requestResult.Rows[i]["RequestStatus"], (string)requestResult.Rows[i]["RequestType"],getDocumentList((string)requestResult.Rows[i]["RequestlistID"]));
                            List<Requestlist> requestlistArray = new List<Requestlist>();
                            requestlistArray.Add(newRequestlist);
                            Request request = new Model.Entities.Request((string)requestResult.Rows[i]["RequestID"],Convert.ToDateTime(requestResult.Rows[i]["RequestDateTime"]), Convert.ToDateTime(requestResult.Rows[i]["DueDateTime"]), null, (string)requestResult.Rows[i]["CompanyID"], (string)requestResult.Rows[i]["CustomerID"], requestlistArray);

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
                            Document document = new Document((string)documentResult.Rows[i]["DocumentID"], (string)documentResult.Rows[i]["DocumentName"], (string)documentResult.Rows[i]["DocumentType"], (string)documentResult.Rows[i]["FileIDInCloud"], (string)documentResult.Rows[i]["CustomerID"], (int)documentResult.Rows[i]["Size"], (int)documentResult.Rows[i]["PageNumber"]);
                            Documentlist newDocumentlist = new Documentlist(document, (int)documentResult.Rows[i]["Sequences"], (string)documentResult.Rows[i]["DocumentColor"], (string)documentResult.Rows[i]["DocumentBothSide"], (int)documentResult.Rows[i]["DocumentPaperType"], (int)documentResult.Rows[i]["DocumentQuantity"], (string)documentResult.Rows[i]["DocumentDescription"]);

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