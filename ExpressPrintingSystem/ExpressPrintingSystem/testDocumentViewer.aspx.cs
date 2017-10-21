using ExpressPrintingSystem.Model;
using GleamTech.DocumentUltimate.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem
{
    public partial class testDocumentViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnButton_Click(object sender, EventArgs e)
        {
            string fileid = "4_z9892df591858d8b156ec0c15_f10522cd8c6f52eef_d20171013_m141048_c001_v0001013_t0039";
            documentViewer.DocumentSource = new DocumentSource(
    new DocumentInfo("unadsfd", "InputFile.docx"), backblaze.downloadFileIntoBytes(fileid));
        }
    }
}