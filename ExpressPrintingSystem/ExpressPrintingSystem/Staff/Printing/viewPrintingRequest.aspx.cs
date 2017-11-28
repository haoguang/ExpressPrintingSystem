using ExpressPrintingSystem.Model.Entities;
using ExpressPrintingSystem.Model.Messenging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Printing
{
    public partial class viewPrintingRequest : System.Web.UI.Page
    {
        private static SortAttribute sortAttribute;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindData();
            }

            string parameter = Request["__EVENTARGUMENT"];

            if(parameter != null)
            {
                if (parameter.Equals("ReloadTable"))
                    bindData();
                else if ("SortTable".Equals(parameter.Split(';')[0]))
                {

                    string[] parameters = parameter.Split(';');
                    SortAttribute newSort = new SortAttribute(parameters[1], SortAttribute.FLOW_ASC, parameters[2]);

                    if (sortAttribute != null)
                    {
                        sortAttribute = SortAttribute.processSorting(sortAttribute, newSort);
                    }
                    else
                        sortAttribute = newSort;

                    if (sortAttribute.Control.Equals("0"))
                    {
                        List<ExpressPrintingSystem.Model.Entities.Request> requestList = getRequestList(Requestlist.STATUS_PENDING, Requestlist.STATUS_PRINTED, Request.Cookies["CompanyID"].Value, sortAttribute.Header);
                        lvRequestConfirmation.DataSource = requestList;
                        lvRequestConfirmation.DataBind();
                    }
                    else if (sortAttribute.Control.Equals("1"))
                    {
                        List<ExpressPrintingSystem.Model.Entities.Request> requestList2 = getRequestList(Requestlist.STATUS_COLLECTED, Requestlist.STATUS_COMPLETED, Request.Cookies["CompanyID"].Value, sortAttribute.Header);
                        lvPickUpRequest.DataSource = requestList2;
                        lvPickUpRequest.DataBind();
                    }
                }
            }
            

        }

        private void bindData()
        {
            List<ExpressPrintingSystem.Model.Entities.Request> requestList = getRequestList(Requestlist.STATUS_PENDING,Requestlist.STATUS_PRINTED, Request.Cookies["CompanyID"].Value, "");
            lvRequestConfirmation.DataSource = requestList;
            lvRequestConfirmation.DataBind();

            List<ExpressPrintingSystem.Model.Entities.Request> requestList2 = getRequestList(Requestlist.STATUS_COLLECTED, Requestlist.STATUS_COMPLETED, Request.Cookies["CompanyID"].Value, "");
            lvPickUpRequest.DataSource = requestList2;
            lvPickUpRequest.DataBind();
        }

        private List<Request> getRequestList(string status, string status2, string companyID, string orderBy)
        {
            List<Request> requestList = new List<Request>();

            DataTable requestResult = null;


            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    //removed "rl.RequestStatus = @status AND" to get all data display at table for testing purpose 
                    string strSelect = "SELECT * FROM Requestlist rl, Request r WHERE rl.RequestID = r.RequestID AND (rl.RequestStatus = @status OR rl.RequestStatus = @status2) AND r.CompanyID = @companyID" + getOrderByString(orderBy);

                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        
                        cmdSelect.Parameters.AddWithValue("@status", status);
                        cmdSelect.Parameters.AddWithValue("@status2", status2);
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
                            newRequestlist.Package = Package.getPackage(newRequestlist.RequestItemID);
                            requestlistArray.Add(newRequestlist);
                            string paymentID = requestResult.Rows[i]["PaymentID"].ToString();
                            Request request = new Model.Entities.Request((string)requestResult.Rows[i]["RequestID"],Convert.ToDateTime(requestResult.Rows[i]["RequestDateTime"]), Convert.ToDateTime(requestResult.Rows[i]["DueDateTime"]), paymentID.Equals("")? null: new Payment(), (string)requestResult.Rows[i]["CompanyID"], (string)requestResult.Rows[i]["CustomerID"], requestlistArray);

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
                    string strSelect= "SELECT * FROM Documentlist dl, Document d WHERE dl.DocumentID = d.DocumentID AND RequestlistID = @requestlistID ORDER BY Sequences";

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

        public string getDocumentViewerUrl(object requestlistID, object documentID)
        {
            return String.Format("ViewDocument.aspx?documentID={0}&requestlistid={1}", HttpUtility.UrlEncode(ClassHashing.basicEncryption((string)documentID)), HttpUtility.UrlEncode(ClassHashing.basicEncryption((string)requestlistID)));
        }

        protected void lvRequestConfirmation_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string operation = e.CommandName;
            string requestlistID = (string)e.CommandArgument;
            if (operation.Equals(COMMAND_PRINT))
            {                
                Requestlist.updateRequestlistStatus(requestlistID, Requestlist.STATUS_PRINTED);               
            }else if (operation.Equals(COMMAND_COMPLETE)){
                Requestlist.updateRequestlistStatus(requestlistID, Requestlist.STATUS_COMPLETED);
                //send notification here;
                sendNotification(requestlistID);
            }
            else if (operation.Equals(COMMAND_PICKUP))
            {
                Requestlist.updateRequestlistStatus(requestlistID, Requestlist.STATUS_COLLECTED);
            }
            PrintingRequestHub.refreshTable();
        }

        private string getOrderByString(string order)
        {
            switch (order)
            {
                case "RequestID":
                    return " ORDER BY rl.RequestID " + sortAttribute.Flow;
                case "Type":
                    return " ORDER BY RequestType " + sortAttribute.Flow;
                case "DueTime":
                    return " ORDER BY DueDateTime " + sortAttribute.Flow;
                case "PaymentState":
                    return " ORDER BY PaymentID " + sortAttribute.Flow;
                default:
                    return " ORDER BY RequestStatus DESC, DueDateTime, RequestType DESC, RequestDateTime";
            }
        }

     public static string getButtonText(string status)
        {
            switch (status)
            {
                case Requestlist.STATUS_PENDING:
                    return "Pending";
                case Requestlist.STATUS_PRINTED:
                    return "Printed";
                case Requestlist.STATUS_COMPLETED:
                    return "Ready";
                default:
                    return "-";

            }
        }

        public static string getCommenName(string status)
        {
            switch (status)
            {
                case Requestlist.STATUS_PENDING:
                    return COMMAND_PRINT;
                case Requestlist.STATUS_PRINTED:
                    return COMMAND_COMPLETE;
                case Requestlist.STATUS_COMPLETED:
                    return COMMAND_PICKUP;
                default:
                    return "-";

            }
        }

        public static Color getLabelColor(string status)
        {
            switch (status)
            {
                case Requestlist.STATUS_PENDING:
                    return Color.FromArgb(255, 179, 255);
                case Requestlist.STATUS_PRINTED:
                    return Color.FromArgb(255, 179, 255);
                case Requestlist.STATUS_COMPLETED:
                    return Color.Yellow;
                case Requestlist.STATUS_COLLECTED:
                    return Color.Green;
                default:
                    return Color.Black;

            }
        }

        private void sendNotification(string requestlistID)
        {
            DataTable result = null;
            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = "Select CustomerEmail, CustomerPhoneNo, CustomerContactMethod, CompanyName From Customer c, Request r, Requestlist rl, Company cp WHERE rl.RequestID = r.RequestID AND r.CustomerID = c.CustomerID AND r.CompanyID = cp.CompanyID AND rl.RequestlistID = @requestlistID";
                    
                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        cmdSelect.Parameters.AddWithValue("@requestlistID", requestlistID);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            result = new DataTable();
                            da.Fill(result);
                        }

                        //return new User((string)result.Rows[0]["ID"], (string)result.Rows[0]["Name"], GetUserRoles(username, loginType), (string)result.Rows[0]["Email"]);
                        string contactNo = (string)result.Rows[0]["CustomerPhoneNo"];
                        string email = (string)result.Rows[0]["CustomerEmail"];
                        string contactMethod = (string)result.Rows[0]["CustomerContactMethod"];
                        string companyName = (string)result.Rows[0]["CompanyName"];

                        switch (contactMethod)
                        {
                            case "whatsapp":
                                WhatappClass.sendWhatsappWithURL(contactNo, "Your Document is Printed and Ready", Response);
                                break;
                            case "wechat":
                            case "E-mail":
                                string emailContent = EmailClass.populateNotificationEmail(companyName);// content of the email
                                EmailClass emailClass = new EmailClass(email, "Document is Printed and Ready", emailContent, true);

                                if (EmailClass.isCredentialed())
                                {
                                    EmailCredential credential = (EmailCredential)Session["EmailCredential"];
                                    emailClass.sendEmail(credential);
                                }
                                else
                                {
                                    Session["tempEmail"] = emailClass;
                                    Response.Redirect(ResolveUrl("~/Staff/VerifyEmail.aspx?ReturnURL=" + Request.Url.AbsoluteUri));
                                }
                                break;
                            case "SMS":
                                break;
                            default:
                                break;
                        }
                        
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public const string COMMAND_PRINT = "printTask";
        public const string COMMAND_COMPLETE = "completeTask";
        public const string COMMAND_PICKUP = "pickupTask";

        private class SortAttribute
        {
            private string header;
            private string flow;
            private string control;

            public SortAttribute(string header, string flow, string control)
            {
                this.header = header;
                this.flow = flow;
                this.control = control;
            }

            //identity how the header should be sorted
            public static SortAttribute processSorting(SortAttribute oldAttribute, SortAttribute newAttribute)
            {
                if(oldAttribute.Header.Equals(newAttribute.Header) && oldAttribute.Control.Equals(newAttribute.Control))
                {
                    if (oldAttribute.Flow.Equals(FLOW_ASC))
                        newAttribute.Flow = FLOW_DESC;
                    else
                        newAttribute.Flow=FLOW_ASC;
                }

                return newAttribute;
            }

            public string Header {
                get { return header; }
                set { header = value; }
            }
            public string Flow {
                get { return flow; }
                set { flow = value; }
            }
            public string Control {
                get { return control; }
                set { control = value; }
            }

            public static string FLOW_ASC = "ASC";
            public static string FLOW_DESC = "DESC";

        }
    }

   
}