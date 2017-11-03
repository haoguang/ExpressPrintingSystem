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
    public partial class GenerateStockRemainReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["QuantityUnder"] != null)
                {
                    string quantityUnder = ClassHashing.basicDecryption(Request.QueryString["QuantityUnder"]);

                    Company company = Company.getCompanyByID(Request.Cookies["CompanyID"].Value);
                    
                    StaffReportTableAdapters.GetStockRemainReportTableAdapter ds = new StaffReportTableAdapters.GetStockRemainReportTableAdapter();
                    DataTable dt = ds.GetData(Convert.ToInt32(quantityUnder), company.CompanyID);
                    ReportParameter[] rParams = new ReportParameter[]
                    {

                        new ReportParameter("Limit",quantityUnder),
                        new ReportParameter("CompanyName",company.Name),
                        new ReportParameter("CompanyAddress",company.Address)
                    };
                    rvStockRemainReport.LocalReport.ReportPath = "Staff/Owner/Report/StockRemainReport.rdlc";
                    rvStockRemainReport.LocalReport.DataSources.Clear();
                    rvStockRemainReport.LocalReport.DataSources.Add(new ReportDataSource("getStockRemain", dt));
                    rvStockRemainReport.LocalReport.SetParameters(rParams);
                    rvStockRemainReport.LocalReport.Refresh();
                }
                else
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Unable to generate relative document.')</script>");
                }

            }
        }
    }
}