using System;
using System.Collections.Generic;
using SecurityAppBusiness.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.Entity
{
    public class ApplicationEntity :IApplicationEntity
    {
        public int IdApplication { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool Status { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}