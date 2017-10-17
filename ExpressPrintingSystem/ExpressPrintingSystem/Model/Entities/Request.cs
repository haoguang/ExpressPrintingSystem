using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class Request
    {
        private string requestID;
        private DateTime requestDateTime;
        private DateTime dueDateTime;
        private Payment payment;
        private string companyID;
        private string customerID;
        private List<Requestlist> requestLists;

        public Request(string requestID, DateTime requestDateTime, DateTime dueDateTime, Payment payment, string companyID, string customerID, List<Requestlist> requestLists)
        {
            this.requestID = requestID;
            this.requestDateTime = requestDateTime;
            this.dueDateTime = dueDateTime;
            this.payment = payment;
            this.companyID = companyID;
            this.customerID = customerID;
            this.requestLists = requestLists;
        }

        public Request(DateTime requestDateTime, DateTime dueDateTime, Payment payment, string companyID, string customerID, List<Requestlist> requestLists)
        {
            this.requestID = null;
            this.requestDateTime = requestDateTime;
            this.dueDateTime = dueDateTime;
            this.payment = payment;
            this.companyID = companyID;
            this.customerID = customerID;
            this.requestLists = requestLists;
        }

        public Request(DateTime requestDateTime, DateTime dueDateTime, Payment payment, string companyID, string customerID)
        {
            this.requestID = null;
            this.requestDateTime = requestDateTime;
            this.dueDateTime = dueDateTime;
            this.payment = payment;
            this.companyID = companyID;
            this.customerID = customerID;
            this.requestLists = new List<Requestlist>();
        }

        public string RequestID
        {
            get { return requestID; }
            set { requestID = value; }
        }

        public DateTime DueDateTime
        {
            get { return DueDateTime; }
            set { dueDateTime = value; }
        }

        public DateTime RequestDateTime
        {
            get { return requestDateTime; }
            set { requestDateTime = value; }
        }

        public Payment Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        public string CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }

        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public List<Requestlist> RequestLists
        {
            get { return requestLists; }
            set { requestLists = value; }
        }
    }
}