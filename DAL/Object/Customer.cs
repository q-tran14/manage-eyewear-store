using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Object
{
    public class Customer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public Customer(string id, string name, string phone)
        {
            ID = id;
            Name = name;
            Phone = phone;
        }
    }

}
