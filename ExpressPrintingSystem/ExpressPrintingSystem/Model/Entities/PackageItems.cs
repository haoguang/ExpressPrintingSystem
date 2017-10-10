using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpressPrintingSystem.Model.Entities
{
    public class PackageItems
    {
        private Item item;
        private int quantity;

        public PackageItems(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }

        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}