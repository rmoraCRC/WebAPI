using System.Linq;
using System.Net.Http;

namespace SecurityAppBusiness.Interface
{
    public interface IBusinessMethods<TEntity>
    {
        void Save(TEntity tEntity);
        void Update(TEntity tEntity);
        void Delete(TEntity tEntity);
        IQueryable<TEntity> GetAll();
        TEntity GetById(int id);
        void FieldsValidation();
    }
}