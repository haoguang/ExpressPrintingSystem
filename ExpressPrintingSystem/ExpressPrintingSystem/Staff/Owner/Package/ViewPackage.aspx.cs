using ExpressPrintingSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Staff.Owner.Package
{
    public partial class ViewPackage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<ExpressPrintingSystem.Model.Entities.Package> packageList = getPackageList(null);
            lvProductList.DataSource = packageList;
            lvProductList.DataBind();
        }

        private List<ExpressPrintingSystem.Model.Entities.Package> getPackageList(string keyword)
        {
            List<ExpressPrintingSystem.Model.Entities.Package> packageList = new List<ExpressPrintingSystem.Model.Entities.Package>();

            DataTable packageResult = null;
            DataTable itemsResult = null;

            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect;
                    if(keyword == null)
                        strSelect = "SELECT * FROM Package";
                    else
                        strSelect = "SELECT * FROM Package WHERE PackageID LIKE '%' + @keyword + '%' OR PackageName  LIKE '%' + @keyword + '%' OR PackageSupport  LIKE '%' + @keyword + '%' OR PackageType LIKE '%' + @keyword + '%'";


                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        if(keyword!= null)
                            cmdSelect.Parameters.AddWithValue("@keyword", keyword);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            packageResult = new DataTable();
                            da.Fill(packageResult);
                        }

                    }

                    if(packageResult != null)
                    {
                        for (int i = 0; i < packageResult.Rows.Count; i++)
                        {
                            itemsResult = null;

                            strSelect = "SELECT i.ItemID, i.ItemName, i.ItemPrice, i.ItemStockQuantity, i.ItemSupplier, p.Quantity FROM Item i, PackageItem p WHERE i.ItemID = p.ItemID AND p.PackageID = @packageID";

                            using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                            {
                                cmdSelect.Parameters.Clear();
                                cmdSelect.Parameters.AddWithValue("@packageID", (string)packageResult.Rows[i]["PackageID"]);

                                using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                                {
                                    itemsResult = new DataTable();
                                    da.Fill(itemsResult);
                                }
                            }

                            List<PackageItems> packageItems = new List<PackageItems>();

                            if(itemsResult != null)
                            {
                                for(int j=0; j<itemsResult.Rows.Count; j++)
                                {
                                    packageItems.Add(new PackageItems(new Model.Entities.Item((string)itemsResult.Rows[j]["ItemID"], (string)itemsResult.Rows[j]["ItemName"], (decimal)itemsResult.Rows[j]["ItemPrice"], 
                                        (int)itemsResult.Rows[j]["ItemStockQuantity"], (string)itemsResult.Rows[j]["ItemSupplier"]),(int)itemsResult.Rows[j]["Quantity"]));
                                }

                                packageList.Add(new Model.Entities.Package((string)packageResult.Rows[i]["PackageID"], (string)packageResult.Rows[i]["PackageName"], (decimal)packageResult.Rows[i]["PackagePrice"], (string)packageResult.Rows[i]["PackageSupport"].ToString(), (string)packageResult.Rows[i]["PackageType"], (decimal)packageResult.Rows[i]["PrintingPricePerPaper"], packageItems));
                            }
                            else
                            {
                                packageList.Add(new Model.Entities.Package((string)packageResult.Rows[i]["PackageID"], (string)packageResult.Rows[i]["PackageName"], (decimal)packageResult.Rows[i]["PackagePrice"], (string)packageResult.Rows[i]["PackageSupport"].ToString(), (string)packageResult.Rows[i]["PackageType"], (decimal)packageResult.Rows[i]["PrintingPricePerPaper"]));
                            }

                        }

                    }

                    return packageList;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string getEditPackageUrl(string packageID)
        {
            return String.Format("EditPackage.aspx?PackageID={0}", HttpUtility.UrlEncode(ClassHashing.basicEncryption(packageID)));
        }
    }
}