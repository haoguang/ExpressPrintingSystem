using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class Company
    {
        private string companyID;
        private string companyName;
        private string companyAddress;
        private string companyContactNo;
        private string companyEmail;

        public Company(string companyID, string companyName, string companyAddress, string companyContactNo, string companyEmail)
        {
            this.companyID = companyID;
            this.companyName = companyName;
            this.companyAddress = companyAddress;
            this.companyContactNo = companyContactNo;
            this.companyEmail = companyEmail;
        }

        public string CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }

        public string Name
        {
            get { return companyName; }
            set { companyName = value; }
        }

        public string Address
        {
            get { return companyAddress; }
            set { companyAddress = value; }
        }

        public string ContactNo
        {
            get { return companyContactNo; }
            set { companyContactNo = value; }
        }

        public string Email
        {
            get { return companyEmail; }
            set { companyEmail = value; }
        }

        public static Company getCompanyByID(string companyID)
        {
            DataTable companyResult = null;
            Company company = null;

            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = "SELECT * FROM Company WHERE CompanyID = @companyID";

                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        cmdSelect.Parameters.AddWithValue("@companyID", companyID);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            companyResult = new DataTable();
                            da.Fill(companyResult);
                        }

                    }

                    company = new Company((string)companyResult.Rows[0]["CompanyID"], (string)companyResult.Rows[0]["CompanyName"], (string)companyResult.Rows[0]["CompanyAddress"], (string)companyResult.Rows[0]["CompanyContactNo"], (string)companyResult.Rows[0]["CompanyEmail"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return company;

        }

    }
}