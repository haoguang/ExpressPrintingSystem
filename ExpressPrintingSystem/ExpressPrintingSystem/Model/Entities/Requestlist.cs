using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class Requestlist
    {
        private string requestlistID;
        private string requestItemID;
        private string requestStatus;
        private string requestType;
        private List<Documentlist> documentList;

        public Requestlist (string requestlistID, string requestItemID, string requestStatus, string requestType, List<Documentlist> documentList)
        {
            this.requestlistID = requestlistID;
            this.requestItemID = requestItemID;
            this.requestStatus = requestStatus;
            this.requestType = requestType;
            this.documentList = documentList;
        }

        public Requestlist(string requestItemID, string requestStatus, string requestType, List<Documentlist> documentList)
        {
            this.requestlistID = null;
            this.requestItemID = requestItemID;
            this.requestStatus = requestStatus;
            this.requestType = requestType;
            this.documentList = documentList;
        }

        public string RequestlistID
        {
            get { return requestlistID; }
            set { requestlistID = value; }
        }

        public string RequestItemID
        {
            get { return requestItemID; }
            set { requestItemID = value; }
        }

        public string RequestStatus
        {
            get { return requestStatus; }
            set { requestStatus = value; }
        }

        public string RequestType
        {
            get { return requestType; }
            set { requestType = value; }
        }

        public List<Documentlist> DocumentList
        {
            get { return documentList; }
            set { documentList = value; }
        }

        public static bool updateRequestlistStatus(string requestlistID, string status)
        {
            SqlConnection conPrint = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString);
            conPrint.Open();

            try
            {
                string strUpdate = "Update Requestlist SET RequestStatus = @requestStatus WHERE RequestlistID = @requestlistID";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, conPrint);

                cmdUpdate.Parameters.AddWithValue("requestStatus", status);
                cmdUpdate.Parameters.AddWithValue("requestlistID", requestlistID);

                int n = cmdUpdate.ExecuteNonQuery();

                if (n > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                conPrint.Close();
            }
            
        }

        public const string TYPE_URGENT = "Urgent";
        public const string TYPE_NORMAL = "Normal";
        public const string STATUS_PENDING = "Pending";
        public const string STATUS_PRINTED = "Printed";
        public const string STATUS_COMPLETED = "Completed";
        public const string STATUS_COLLECTED = "Collected";
        public const string STATUS_CANCELLED = "Cancelled"; 

    }
}