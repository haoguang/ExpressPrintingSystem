using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class Documentlist
    {
        private Document document;
        private int sequences;
        private string documentColor;
        private string documentBothSide;
        private string documentPaperType;
        private int documentQuantity;
        private string documentDescription;

        public Documentlist(Document document, int sequences, string documentColor, string documentBothSide, string documentPaperType, int documentQuantity, string documentDescription)
        {
            this.document = document;
            this.sequences = sequences;
            this.documentColor = documentColor;
            this.documentBothSide = documentBothSide;
            this.documentPaperType = documentPaperType;
            this.documentQuantity = documentQuantity;
            this.documentDescription = documentDescription;
        }

        public Document Document
        {
            get { return document; }
            set { document = value; }
        }

        public int Sequences
        {
            get { return sequences; }
            set { sequences = value; }
        }

        public string DocumentColor
        {
            get { return documentColor; }
            set { documentColor = value; }
        }

        public string DocumentBothSide
        {
            get { return documentBothSide; }
            set { documentBothSide = value; }
        }

        public string DocumentPaperType
        {
            get { return documentPaperType; }
            set { documentPaperType = value; }
        }

        public int DocumentQuantity
        {
            get { return documentQuantity; }
            set { documentQuantity = value; }
        }

        public string DocumentDescription
        {
            get { return documentDescription; }
            set { documentDescription = value;}
        }


    }
}