using System.Linq;

namespace SecurityAppDataAccess.Interface
{
    public interface IDataAccessMethods<T>
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Save();
        void Update();
        void Delete();
    }
}