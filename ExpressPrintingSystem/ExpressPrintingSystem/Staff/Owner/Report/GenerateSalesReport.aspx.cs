using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.Report
{
    public partial class GenerateSalesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StaffReportTableAdapters.GetSalesReportTableAdapter ds = new StaffReportTableAdapters.GetSalesReportTableAdapter();
            DataTable dt = ds.GetData(Convert.ToDateTime("10/10/2017"),Convert.ToDateTime("26/10/2017"),"CO1036");
            Microsoft.Reporting.WebForms.ReportParameter[] rParams = new Microsoft.Reporting.WebForms.ReportParameter[]
            {
                new Microsoft.Reporting.WebForms.ReportParameter("FromDate","10/10/2017"),
                new Microsoft.Reporting.WebForms.ReportParameter("ToDate","26/10/2017"),
                new Microsoft.Reporting.WebForms.ReportParameter("reportType","Custom")
            };
            rvSalesReport.LocalReport.ReportPath = "Staff/Owner/Report/SalesReport.rdlc";
            rvSalesReport.LocalReport.DataSources.Clear();
            rvSalesReport.LocalReport.DataSources.Add(new ReportDataSource("StaffReport", dt));
            //rvSalesReport.LocalReport.SetParameters(rParams);
            rvSalesReport.LocalReport.Refresh();
        }
    }
}