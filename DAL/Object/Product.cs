using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Object
{
    public class Product
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int StockEntryQuantity { get; set; }
        public DateTime StockEntryDate { get; set; }
        public DateTime? SoldOutDate { get; set; } // nullable

        public Product(string id, string name, int price, int quantity,
                       int stockEntryQuantity, DateTime stockEntryDate, DateTime? soldOutDate = null)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            StockEntryQuantity = stockEntryQuantity;
            StockEntryDate = stockEntryDate;
            SoldOutDate = soldOutDate;
        }
    }

}
