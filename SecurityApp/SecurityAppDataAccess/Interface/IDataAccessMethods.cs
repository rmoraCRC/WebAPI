using System.Collections.Generic;
using System.Linq;

namespace SecurityAppDataAccess.Interface
{
    public interface IDataAccessMethods<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Save();
        void Update();
        void Delete();
    }
}