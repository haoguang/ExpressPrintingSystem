using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class Item
    {
        private string itemID;
        private string itemName;
        private decimal itemPrice;
        private int itemStockQuantity;
        private string itemSupplier;

        public Item(string itemID, string itemName, decimal itemPrice, int itemStockQuantity, string itemSupplier)
        {
            this.itemID = itemID;
            this.itemName = itemName;
            this.itemPrice = itemPrice;
            this.itemStockQuantity = itemStockQuantity;
            this.itemSupplier = itemSupplier;

        }

        public string ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        public int ItemStockQuantity
        {
            get { return itemStockQuantity; }
            set { itemStockQuantity = value; }
        }

        public string ItemSupplier
        {
            get { return itemSupplier; }
            set { itemSupplier = value; }
        }
    }
}