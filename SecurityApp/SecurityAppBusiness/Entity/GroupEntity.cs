using System.Collections.Generic;
using SecurityAppBusiness.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.Entity
{
    public class GroupEntity : IGroupEntity
    {
        public int IdGroup { get; set; }
        public string Description { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}