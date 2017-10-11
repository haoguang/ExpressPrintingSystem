﻿using System;
using System.Collections;
using System.Collections.Generic;
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

        public static readonly List<string> documentType = new List<string>{ ".docx", ".doc", ".pdf", "pptx"};

        public const string TYPE_PRINTING = "Printing";
        public const string TYPE_PRODUCT = "Product";

        public static readonly List<string> PACKAGE_TYPE_LIST = new List<string> { TYPE_PRINTING,TYPE_PRODUCT };
    }
}