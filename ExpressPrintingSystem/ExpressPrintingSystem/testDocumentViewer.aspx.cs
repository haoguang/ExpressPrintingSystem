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
            string fileid = "4_z9892df591858d8b156ec0c15_f1129b2d1c12e5559_d20171012_m161810_c001_v0001038_t0049";
            documentViewer.DocumentSource = new DocumentSource(
    new DocumentInfo("uniqueId", "InputFile.docx"), backblaze.downloadFileIntoBytes(fileid));
        }
    }
}