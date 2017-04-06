using System;
using System.Collections.Generic;
using SecurityAppDataAccess.Model;

namespace SecurityAppDataAccess.Interface
{
    public interface IUser 
    {
        int IdUser { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        string Address { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        bool Status { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        DateTime CreateDateTime { get; set; }
        ICollection<Role> Roles { get; set; }
    }
}