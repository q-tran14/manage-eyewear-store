using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Object
{
    public class Employee
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StreetAddress { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public int Salary { get; set; }
        public string Role { get; set; }
        public DateTime DateEmployed { get; set; }

        public Employee(string id, string name, string email, string password, string streetAddress,
                        string ward, string district, string city, string phone, int salary,
                        string role, DateTime dateEmployed)
        {
            ID = id;
            Name = name;
            Email = email;
            Password = password;
            StreetAddress = streetAddress;
            Ward = ward;
            District = district;
            City = city;
            Phone = phone;
            Salary = salary;
            Role = role;
            DateEmployed = dateEmployed;
        }
    }

}
