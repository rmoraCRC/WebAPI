using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SecurityAppBusiness.Interface;
using SecurityAppDataAccess.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.BusinessObject
{
    public class ApplicationBusinessObject : IBusinessMethods<IApplicationEntity>
    {
        private readonly IApplicationBusinessServices _applicationBusinessServices;

        public ApplicationBusinessObject(IApplicationBusinessServices applicationBusinessBusinessServices)
        {
            _applicationBusinessServices = applicationBusinessBusinessServices;
        }
        public IQueryable<IApplicationEntity> GetAll()
        {
            return _applicationBusinessServices.GetAll();
        }
        public IApplicationEntity GetById(int id)
        {
            return _applicationBusinessServices.GetById(id);
        }
        public void FieldsValidation()
        {
            throw new NotImplementedException();
        }
        public void Save(IApplicationEntity applicationEntityEntity)
        {
            _applicationBusinessServices.Save(applicationEntityEntity);
        }
        public void Update(IApplicationEntity applicationEntityEntity)
        {
            _applicationBusinessServices.Update(applicationEntityEntity);
        }
        public void Delete(IApplicationEntity applicationEntityEntity)
        {
            _applicationBusinessServices.Delete(applicationEntityEntity);
        }
    }
}