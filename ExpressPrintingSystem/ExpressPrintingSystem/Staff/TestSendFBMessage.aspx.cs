using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExpressPrintingSystem.Model.Messenging;

namespace ExpressPrintingSystem.Staff
{
    public partial class TestSendFBMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MessengerClass.test();
        }
    }
}