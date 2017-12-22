using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.Report
{
    public partial class Report : System.Web.UI.Page
    {
        private const string SaleReportUrl = "GenerateSalesReport.aspx";
        private const string StockReportUrl = "GenerateStockRemainReport.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rblPeriod.SelectedIndex = 0;
                rblPeriod_SelectedIndexChanged(null, null);
                ddlReportName_SelectedIndexChanged(null, null);
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {

            Response.Write("<script>window.open ('"+getReportUrl()+"','_blank');</script>");
        }

        private string getReportUrl()
        {
            string url = null;

            switch (ddlReportName.SelectedValue)
            {
                case SALES_REPORT:
                    DateTime startingDate = new DateTime(), endingDate = new DateTime();
                    switch (rblPeriod.SelectedValue)
                    {
                        case "Daily":
                            string[] dailyString = txtDaily.Text.Split('-');
                            startingDate = new DateTime(Convert.ToInt32(dailyString[0]), Convert.ToInt32(dailyString[1]), Convert.ToInt32(dailyString[2]));
                            endingDate = new DateTime(Convert.ToInt32(dailyString[0]), Convert.ToInt32(dailyString[1]), Convert.ToInt32(dailyString[2]));
                            break;
                        case "Monthly":
                            string[] monthlyString = txtMonthly.Text.Split('-');                           
                            startingDate = new DateTime(Convert.ToInt32(monthlyString[0]), Convert.ToInt32(monthlyString[1]), 1);
                            endingDate = new DateTime(Convert.ToInt32(monthlyString[0]), Convert.ToInt32(monthlyString[1]), DateTime.DaysInMonth(Convert.ToInt32(monthlyString[0]), Convert.ToInt32(monthlyString[1])),23,59,59);
                            break;
                        case "Yearly":
                            string[] yearlyString = txtYearly.Text.Split('-');
                            startingDate = new DateTime(Convert.ToInt32(yearlyString[0]), 1, 1);
                            endingDate = new DateTime(Convert.ToInt32(yearlyString[0]), 12, 31, 23, 59, 59);
                            break;
                        case "Custom":
                            string[] dateFromString = txtDateFrom.Text.Split('-');
                            string[] dateToString = txtDateTo.Text.Split('-');
                            startingDate = new DateTime(Convert.ToInt32(dateFromString[0]), Convert.ToInt32(dateFromString[1]), Convert.ToInt32(dateFromString[2]));
                            endingDate = new DateTime(Convert.ToInt32(dateToString[0]), Convert.ToInt32(dateToString[1]), Convert.ToInt32(dateToString[2]));
                            break;
                    }

                    url = SaleReportUrl + "?StartDate=" + HttpUtility.UrlEncode(ClassHashing.basicEncryption(startingDate.ToString("dd/MM/yyyy"))) + "&EndDate="+ HttpUtility.UrlEncode(ClassHashing.basicEncryption(endingDate.ToString("dd/MM/yyyy"))) + "&ReportType=" + HttpUtility.UrlEncode(ClassHashing.basicEncryption(rblPeriod.SelectedValue));
                    break;
                case STOCK_REMAIN_REPORT:
                    url = StockReportUrl + "?QuantityUnder=" + HttpUtility.UrlEncode(ClassHashing.basicEncryption(txtStock.Text));
                    break;
            }

            return url;
        }

        public const string SALES_REPORT = "SR";
        public const string STOCK_REMAIN_REPORT = "SRR";

        protected void rblPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblPeriod.SelectedValue)
            {
                case "Daily":
                    enableDailyControl(true);
                    enableMonthlyControl(false);
                    enableYearlyControl(false);
                    enableCustomControl(false);
                    break;
                case "Monthly":
                    enableDailyControl(false);
                    enableMonthlyControl(true);
                    enableYearlyControl(false);
                    enableCustomControl(false);
                    break;
                case "Yearly":
                    enableDailyControl(false);
                    enableMonthlyControl(false);
                    enableYearlyControl(true);
                    enableCustomControl(false);
                    break;
                case "Custom":
                    enableDailyControl(false);
                    enableMonthlyControl(false);
                    enableYearlyControl(false);
                    enableCustomControl(true);
                    break;

            }
        }

        private void enableDailyControl(bool state)
        {
            if (state)
            {
                dateControlDaily.Visible = true;
                txtDaily.Enabled = true;
            }
            else
            {
                dateControlDaily.Visible = false;
                txtDaily.Enabled = false;
            }
        }

        private void enableMonthlyControl(bool state)
        {
            if (state)
            {
                dateControlMonthly.Visible = true;
                txtMonthly.Enabled = true;

            }
            else
            {
                dateControlMonthly.Visible = false;
                txtMonthly.Enabled = false;

            }
        }

        private void enableYearlyControl(bool state)
        {
            if (state)
            {
                dateControlYearly.Visible = true;
                txtYearly.Enabled = true;
            }
            else
            {
                dateControlYearly.Visible = false;
                txtYearly.Enabled = false;

            }
        }

        private void enableCustomControl(bool state)
        {
            if (state)
            {
                dateControlCustom.Visible = true;
                txtDateFrom.Enabled = true;
                txtDateTo.Enabled = true;
            }
            else
            {
                dateControlCustom.Visible = false;
                txtDateFrom.Enabled = false;
                txtDateTo.Enabled = false;
            }
        }

        protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlReportName.SelectedValue)
            {
                case SALES_REPORT:
                    periodRow.Visible = true;
                    rblPeriod.Enabled = true;
                    rblPeriod_SelectedIndexChanged(null, null);

                    stockControl.Visible = false;
                    txtStock.Enabled = false;
                    break;
                case STOCK_REMAIN_REPORT:
                    periodRow.Visible = false;
                    rblPeriod.Enabled = false;
                    enableDailyControl(false);
                    enableMonthlyControl(false);
                    enableYearlyControl(false);
                    enableCustomControl(false);

                    stockControl.Visible = true;
                    txtStock.Enabled = true;

                    break;
            }
        }
    }
}