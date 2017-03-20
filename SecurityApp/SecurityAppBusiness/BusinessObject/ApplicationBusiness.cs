using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SecurityAppBusiness.Abstract;
using SecurityAppBusiness.Interface;
using SecurityAppDataAccess.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.BusinessObject
{
    public class ApplicationBusiness : IApplicationBusiness
    {
        private static IApplicationBusiness _applicationInstance;

        private ApplicationBusiness()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Application, IApplicationEntity>();
                cfg.CreateMap<IApplicationEntity, Application>();
            });
        }
        private static void MapperConfiguration()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Application, IApplicationEntity>();
                cfg.CreateMap<IApplicationEntity, Application>();
            });
        }

        public static IApplicationBusiness GetNewApplication()
        {
            MapperConfiguration();
            return _applicationInstance ?? (_applicationInstance = new ApplicationBusiness());
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
            throw new NotImplementedException();
        }
        public void Save(IApplicationEntity applicationEntityEntity)
        {
            var dataApplication = Mapper.Map<IApplicationEntity, IApplication>(applicationEntityEntity);
            dataApplication.Save();
        }
        public void Update(IApplicationEntity applicationEntityEntity)
        {
            var dataApplication = Mapper.Map<IApplicationEntity, IApplication>(applicationEntityEntity);
            dataApplication.Update();
        }
        public void Delete(IApplicationEntity applicationEntityEntity)
        {
            var dataApplication = Mapper.Map<IApplicationEntity, IApplication>(applicationEntityEntity);
            dataApplication.Delete();
        }
    }
}