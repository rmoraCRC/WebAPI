using System;
using System.Collections.Generic;
using SecurityAppDataAccess.Model;

namespace SecurityAppDataAccess.Interface
{
    public interface IApplication 
    {
        int IdApplication { get; set; }
        string Description { get; set; }
        DateTime CreateDateTime { get; set; }
        bool Status { get; set; }
        ICollection<Role> roles { get; set; }
    }
}