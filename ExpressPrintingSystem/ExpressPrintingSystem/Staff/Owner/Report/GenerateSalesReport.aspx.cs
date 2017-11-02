using ExpressPrintingSystem.Model.Entities;
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
            if (!IsPostBack)
            {
                if (Request.QueryString["StartDate"] != null && Request.QueryString["EndDate"] != null && Request.QueryString["ReportType"] != null)
                {
                    string startingDate = ClassHashing.basicDecryption(Request.QueryString["StartDate"]);
                    string endingDate = ClassHashing.basicDecryption(Request.QueryString["EndDate"]);
                    string reportType = ClassHashing.basicDecryption(Request.QueryString["ReportType"]);

                    Company company = Company.getCompanyByID(Request.Cookies["CompanyID"].Value);

                    StaffReportTableAdapters.GetSalesReportTableAdapter ds = new StaffReportTableAdapters.GetSalesReportTableAdapter();
                    DataTable dt = ds.GetData(DateTime.Parse(startingDate), (DateTime.Parse(endingDate)).AddDays(1), company.CompanyID);
                    Microsoft.Reporting.WebForms.ReportParameter[] rParams = new Microsoft.Reporting.WebForms.ReportParameter[]
                    {
                        new Microsoft.Reporting.WebForms.ReportParameter("FromDate",startingDate),
                        new Microsoft.Reporting.WebForms.ReportParameter("ToDate",endingDate),
                        new Microsoft.Reporting.WebForms.ReportParameter("reportType",reportType),
                        new Microsoft.Reporting.WebForms.ReportParameter("CompanyName",company.Name),
                        new Microsoft.Reporting.WebForms.ReportParameter("CompanyAddress",company.Address)
                    };
                    rvSalesReport.LocalReport.ReportPath = "Staff/Owner/Report/SaleReport.rdlc";
                    rvSalesReport.LocalReport.DataSources.Clear();
                    rvSalesReport.LocalReport.DataSources.Add(new ReportDataSource("getSales", dt));
                    rvSalesReport.LocalReport.SetParameters(rParams);
                    rvSalesReport.LocalReport.Refresh();
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Unable retrieve generate relative document.')</script>");
                }

            }
            
        }
    }

}