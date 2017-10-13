using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class Document
    {
        private string documentID;
        private string documentName;
        private string documentType;
        private string fileIdInCloud;
        private string customerID;
        private int size;
        private int pageNumber;

        public Document(string documentID, string documentName, string documentType, string fileIdInCloud, string customerID, int size, int pageNumber)
        {
            this.documentID = documentID;
            this.documentName = documentName;
            this.documentType = documentType;
            this.fileIdInCloud = fileIdInCloud;
            this.customerID = customerID;
            this.size = size;
            this.pageNumber = pageNumber;
        }

        public Document( string documentName, string documentType, string fileIdInCloud, string customerID, int size, int pageNumber)
        {
            this.documentID = null;
            this.documentName = documentName;
            this.documentType = documentType;
            this.fileIdInCloud = fileIdInCloud;
            this.customerID = customerID;
            this.size = size;
            this.pageNumber = pageNumber;
        }

        public string DocumentID
        {
            get { return documentID; }
            set { documentID = value; }
        }

        public string DocumentName
        {
            get { return documentName; }
            set { documentName = value; }
        }

        public string DocumentType
        {
            get { return documentType; }
            set { documentType = value; }
        }

        public string FileIDInCloud
        {
            get { return fileIdInCloud; }
            set { fileIdInCloud = value; }
        }

        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }

    }
}