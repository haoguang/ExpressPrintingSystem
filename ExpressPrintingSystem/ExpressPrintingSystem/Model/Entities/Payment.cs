using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class Payment
    {
        private string paymentID;
        private string paymentType;
        private decimal paymentAmount;
        private DateTime paymentDateTime;

        public Payment(string paymentID, string paymentType, decimal paymentAmount, DateTime paymentDateTime)
        {
            this.paymentID = paymentID;
            this.paymentType = paymentType;
            this.paymentAmount = paymentAmount;
            this.paymentDateTime = paymentDateTime;

        }

        public Payment( string paymentType, decimal paymentAmount, DateTime paymentDateTime)
        {
            this.paymentID = null;
            this.paymentType = paymentType;
            this.paymentAmount = paymentAmount;
            this.paymentDateTime = paymentDateTime;
        }

        public string PaymentID
        {
            get { return paymentID; }
            set { paymentID = value; }
        }

        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }

        public decimal PaymentAmount
        {
            get { return paymentAmount; }
            set { paymentAmount = value; }
        }

        public DateTime PaymentDateTime
        {
            get { return paymentDateTime; }
            set { paymentDateTime = value; }
        }

        public const string TYPE_CASH = "Cash";
        public const string TYPE_CARD = "Card";

    }
}