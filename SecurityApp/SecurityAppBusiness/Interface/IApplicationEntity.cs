using System;
using System.Collections.Generic;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.Interface
{
    public interface IApplicationEntity
    {
        int IdApplication { get; set; }
        string Description { get; set; }
        DateTime CreateDateTime { get; set; }
        bool Status { get; set; }
        ICollection<Role> Roles { get; set; }
    }
}