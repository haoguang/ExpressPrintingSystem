using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class Requestlist
    {
        private string requestlistID;
        private string requestItemID;
        private int requestQuantity;
        private string requestStatus;
        private string requestType;
        private List<Documentlist> documentList;

        public Requestlist (string requestlistID, string requestItemID, int requestQuantity, string requestStatus, string requestType, List<Documentlist> documentList)
        {
            this.requestlistID = requestlistID;
            this.requestItemID = requestItemID;
            this.requestQuantity = requestQuantity;
            this.requestStatus = requestStatus;
            this.requestType = requestType;
            this.documentList = documentList;
        }

        public Requestlist(string requestItemID, int requestQuantity, string requestStatus, string requestType, List<Documentlist> documentList)
        {
            this.requestlistID = null;
            this.requestItemID = requestItemID;
            this.requestQuantity = requestQuantity;
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

        public const string TYPE_URGENT = "Urgent";
        public const string TYPE_NORMAL = "Normal";
        public const string STATUS_PENDING = "Pending";
        public const string STATUS_PRINTED = "Printed";
        public const string STATUS_COMPLETED = "Completed";

    }
}