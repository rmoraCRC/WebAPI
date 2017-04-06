using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SecurityAppBusiness.Interface;
using SecurityAppDataAccess.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.BusinessServices
{
    public class ApplicationBusinessService : IApplicationBusinessServices
    {
        public ApplicationBusinessService()
        {
            MapperConfiguration();
        }

        public  void MapperConfiguration()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Application, IApplicationEntity>();
                cfg.CreateMap<IApplicationEntity, Application>();
            });
        }
        public void Save(IApplicationEntity tEntity)
        {
            var dataApplication = Mapper.Map<IApplicationEntity, Application>(tEntity);
            dataApplication.Save();
        }

        public void Update(IApplicationEntity tEntity)
        {
            var dataApplication = Mapper.Map<IApplicationEntity, Application>(tEntity);
            dataApplication.Update();
        }

        public void Delete(IApplicationEntity tEntity)
        {
            var dataApplication = Mapper.Map<IApplicationEntity, Application>(tEntity);
            dataApplication.Delete();
        }

        public IQueryable<IApplicationEntity> GetAll()
        {
            var application = Application.GetNewApplication();
            var dataApplications = application.GetAll();

            if (dataApplications.Any())
                return Mapper.Map<IEnumerable<IApplication>, IEnumerable<IApplicationEntity>>(dataApplications).AsQueryable();

            return null;
        }

        public IApplicationEntity GetById(int id)
        {
            var application = Application.GetNewApplication();
            var findedApplication = application.GetById(id);

            return Mapper.Map<IApplication, IApplicationEntity>(findedApplication);
        }

        public void FieldsValidation()
        {
            throw new System.NotImplementedException();
        }
    }
}