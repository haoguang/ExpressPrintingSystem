using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class Package
    {
        private string packageID;
        private string packageName;
        private decimal packagePrice;
        private string packageSupport;
        private string packageType;
        private decimal printingPrice; //Printing Price Per Paper
        private List<PackageItems> packageItems; // store PackageItems objects

        public Package(string packageID, string packageName, decimal packagePrice, string packageSupport, string packageType, List<PackageItems> packageItems)
        {
            this.packageID = packageID;
            this.packageName = packageName;
            this.packagePrice = packagePrice;
            this.packageSupport = packageSupport;
            this.packageType = packageType;
            this.printingPrice = 0;
            this.packageItems = packageItems;
        }

        public Package (string packageID, string packageName, decimal packagePrice, string packageSupport, string packageType)
        {
            this.packageID = packageID;
            this.packageName = packageName;
            this.packagePrice = packagePrice;
            this.packageSupport = packageSupport;
            this.packageType = packageType;
            this.printingPrice = 0;
            this.packageItems = new List<PackageItems>();
        }

        public Package(string packageID, string packageName, decimal packagePrice, string packageSupport, string packageType, decimal printingPrice, List<PackageItems> packageItems)
        {
            this.packageID = packageID;
            this.packageName = packageName;
            this.packagePrice = packagePrice;
            this.packageSupport = packageSupport;
            this.packageType = packageType;
            this.printingPrice = printingPrice;
            this.packageItems = packageItems;
        }

        public Package(string packageID, string packageName, decimal packagePrice, string packageSupport, string packageType, decimal printingPrice)
        {
            this.packageID = packageID;
            this.packageName = packageName;
            this.packagePrice = packagePrice;
            this.packageSupport = packageSupport;
            this.packageType = packageType;
            this.printingPrice = printingPrice;
            this.packageItems = new List<PackageItems>();
        }

        public string PackageID
        {
            get { return packageID; }
            set { packageID = value; }
        }

        public string PackageName
        {
            get { return packageName; }
            set { packageName = value; }
        }

        public decimal PackagePrice
        {
            get { return packagePrice; }
            set { packagePrice = value; }
        }

        public string PackageSupport
        {
            get { return packageSupport; }
            set { packageSupport = value; }
        }

        public string PackageType
        {
            get { return packageType; }
            set { packageType = value; }
        }

        public decimal PrintingPrice
        {
            get { return printingPrice; }
            set { printingPrice = value; }
        }

        public List<PackageItems> PackageItems
        {
            get { return packageItems; }
            set { packageItems = value; }
        }

        public void addPackageItem(PackageItems packageItem)
        {
            packageItems.Add(packageItem);
        }

        public void removePackageItem(int index)
        {
            packageItems.RemoveAt(index);
        }

        public static Package getPackage(string packageID)
        {
            DataTable packageResult = null;
            DataTable itemsResult = null;
            Package package = null;

            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = "SELECT * FROM Package WHERE PackageID = @packageID";

                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        cmdSelect.Parameters.AddWithValue("@packageID", packageID);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            packageResult = new DataTable();
                            da.Fill(packageResult);
                        }

                    }

                    if (packageResult != null)
                    {
                        itemsResult = null;

                        strSelect = "SELECT i.ItemID, i.ItemName, i.ItemPrice, i.ItemStockQuantity, i.ItemSupplier, p.Quantity FROM Item i, PackageItem p WHERE i.ItemID = p.ItemID AND p.PackageID = @packageID";

                        using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                        {
                            cmdSelect.Parameters.Clear();
                            cmdSelect.Parameters.AddWithValue("@packageID", packageID);

                            using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                            {
                                itemsResult = new DataTable();
                                da.Fill(itemsResult);
                            }
                        }

                        List<PackageItems> packageItems = new List<PackageItems>();


                        if (itemsResult != null)
                        {
                            for (int j = 0; j < itemsResult.Rows.Count; j++)
                            {
                                packageItems.Add(new PackageItems(new Model.Entities.Item((string)itemsResult.Rows[j]["ItemID"], (string)itemsResult.Rows[j]["ItemName"], (decimal)itemsResult.Rows[j]["ItemPrice"],
                                    (int)itemsResult.Rows[j]["ItemStockQuantity"], (string)itemsResult.Rows[j]["ItemSupplier"]), (int)itemsResult.Rows[j]["Quantity"]));
                            }
                            package = new Package((string)packageResult.Rows[0]["PackageID"], (string)packageResult.Rows[0]["PackageName"], (decimal)packageResult.Rows[0]["PackagePrice"], (string)packageResult.Rows[0]["PackageSupport"], (string)packageResult.Rows[0]["PackageType"], (decimal)packageResult.Rows[0]["PrintingPricePerPaper"], packageItems);
                        }
                        else
                        {
                            package = new Package((string)packageResult.Rows[0]["PackageID"], (string)packageResult.Rows[0]["PackageName"], (decimal)packageResult.Rows[0]["PackagePrice"], (string)packageResult.Rows[0]["PackageSupport"], (string)packageResult.Rows[0]["PackageType"], (decimal)packageResult.Rows[0]["PrintingPricePerPaper"]);
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return package;
        }

        public static readonly List<string> documentType = new List<string>{ ".docx", ".doc", ".pdf", "pptx", ".jpg", ".png"};

        public const string TYPE_PRINTING = "Printing";
        public const string TYPE_PRODUCT = "Product";

        public static readonly List<string> PACKAGE_TYPE_LIST = new List<string> { TYPE_PRINTING,TYPE_PRODUCT };
    }
}