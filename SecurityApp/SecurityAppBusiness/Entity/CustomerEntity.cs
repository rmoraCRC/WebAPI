using System;
using SecurityAppBusiness.Interface;

namespace SecurityAppBusiness.Entity
{
    public class CustomerEntity : ICustomerEntity
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public int IdCompany { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
    }
}