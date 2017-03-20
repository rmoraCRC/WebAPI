using System.Collections.Generic;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.Interface
{
    public interface IGroupEntity
    {
        int IdGroup { get; set; }
        string Description { get; set; }
        ICollection<Role> Roles { get; set; }
    }
}