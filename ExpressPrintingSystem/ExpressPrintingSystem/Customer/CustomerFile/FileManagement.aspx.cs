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
            if (Request.Cookies["UserCookie"] != null)
            {

                var Cookie = Request.Cookies["UserCookie"];

                if (Cookie.Values["UserInfo"] != null)
                {
                    string userString = ClassHashing.basicDecryption(Cookie.Values["UserInfo"].ToString());
                    User user = ExpressPrintingSystem.Model.Entities.User.toUserObject(userString);
                  
                    ViewState["UserID"] = ClassHashing.basicEncryption(user.ID);
                    bindData();
                }
            }

          
        }

        private void bindData()
        {
            List<ExpressPrintingSystem.Model.Entities.Document> document = getDocumentList();
            lvPackageItems.DataSource = document;
            lvPackageItems.DataBind();

           
        }

        private List<Document> getDocumentList()
        {
            string userID = ClassHashing.basicDecryption((string)ViewState["UserID"]);

           List<Document> listdocument = new List<Document>();

            DataTable documentResult;


            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = "SELECT * from Document WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {

                        cmdSelect.Parameters.AddWithValue("@CustomerID", userID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            documentResult = new DataTable();
                            da.Fill(documentResult);
                        }

                        for (int i = 0; i < documentResult.Rows.Count; i++)
                        {
                            Document document = new Document((string)documentResult.Rows[i]["DocumentID"], (string)documentResult.Rows[i]["DocumentName"], (string)documentResult.Rows[i]["DocumentType"], (string)documentResult.Rows[i]["FileIDInCloud"], (string)documentResult.Rows[i]["CustomerID"], (int)documentResult.Rows[i]["Size"], (int)documentResult.Rows[i]["PageNumber"]);
                            //Documentlist newDocumentlist = new Documentlist(document, (int)documentResult.Rows[i]["Sequences"], (string)documentResult.Rows[i]["DocumentColor"], (string)documentResult.Rows[i]["DocumentBothSide"], (int)documentResult.Rows[i]["DocumentPaperType"], (int)documentResult.Rows[i]["DocumentQuantity"], documentResult.Rows[i]["DocumentDescription"].ToString());
                            //newDocumentlist.RequestlistID = requestlistID;

                            //documentList.Add(newDocumentlist);
                            listdocument.Add(document);
                        }

                    }
                }
               return listdocument;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}