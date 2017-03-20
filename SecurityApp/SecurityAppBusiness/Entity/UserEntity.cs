using System;
using System.Collections.Generic;
using SecurityAppBusiness.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.Entity
{
    public class UserEntity : IUserEntity
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDateTime { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}