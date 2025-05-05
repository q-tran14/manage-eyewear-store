using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Object
{
    public class Voucher
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public int MinimumOrderPrice { get; set; }
        public int DiscountAmount { get; set; }
        public int DiscountMaximumAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public Voucher(string id, string description, int minimumOrderPrice, int discountAmount, int discountMaximumAmount, DateTime startDate, DateTime endDate, bool isActive)
        {
            ID = id;
            Description = description;
            MinimumOrderPrice = minimumOrderPrice;
            DiscountAmount = discountAmount;
            DiscountMaximumAmount = discountMaximumAmount;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
        }
    }

}
