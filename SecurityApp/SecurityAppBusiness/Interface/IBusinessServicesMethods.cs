using System.Linq;

namespace SecurityAppBusiness.Interface
{
    public interface IBusinessServicesMethods<TEntity>
    {
        void Save(TEntity tEntity);
        void Update(TEntity tEntity);
        void Delete(TEntity tEntity);
        IQueryable<TEntity> GetAll();
        TEntity GetById(int id);
        void MapperConfiguration();
    }
}