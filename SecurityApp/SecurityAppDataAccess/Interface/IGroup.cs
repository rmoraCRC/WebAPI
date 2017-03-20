using System.Collections.Generic;
using SecurityAppDataAccess.Model;

namespace SecurityAppDataAccess.Interface
{
    public interface IGroup : IDataAccessMethods<IGroup>
    {
        int IdGroup { get; set; }
        string Description { get; set; }
        ICollection<Role> roles { get; set; }
    }
}