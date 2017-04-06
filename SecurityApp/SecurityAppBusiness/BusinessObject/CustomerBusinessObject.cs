using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SecurityAppBusiness.Interface;
using SecurityAppDataAccess.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.BusinessObject
{
    public class CustomerBusinessObject : ICustomerBusinessServices
    {
        private static ICustomerBusinessServices _customerInstance;
        private CustomerBusinessObject()
        {

        }
        public void MapperConfiguration()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer, ICustomerEntity>();
                cfg.CreateMap<ICustomerEntity, Customer>();
            });
        }
        public static ICustomerBusinessServices GetNewCustomer()
        {
            return _customerInstance ?? (_customerInstance = new CustomerBusinessObject());
        }
        public void Save(ICustomerEntity customerEntity)
        {
            var dataCustomer = Mapper.Map<ICustomerEntity, ICustomer>(customerEntity);
            dataCustomer.Save();
        }
        public void Update(ICustomerEntity customerEntity)
        {
            var dataCustomer = Mapper.Map<ICustomerEntity, ICustomer>(customerEntity);
            dataCustomer.Save();
        }
        public void Delete(ICustomerEntity customerEntity)
        {
            var dataCustomer = Mapper.Map<ICustomerEntity, ICustomer>(customerEntity);
            dataCustomer.Save();
        }
        public IQueryable<ICustomerEntity> GetAll()
        {
            var customer = Customer.GetNewCustomer();
            var dataCustomers = customer.GetAll();

            if (dataCustomers.Any())
                return Mapper.Map<IEnumerable<ICustomer>, IEnumerable<ICustomerEntity>>(dataCustomers).AsQueryable();

            return null;
        }
        public ICustomerEntity GetById(int id)
        {
            var customer = Customer.GetNewCustomer();
            var findedCustomer = customer.GetById(id);
            if (findedCustomer.Name.Equals(null))
                return null;

            return Mapper.Map<ICustomer, ICustomerEntity>(findedCustomer);

        }



        public void FieldsValidation()
        {
            throw new NotImplementedException();
        }
    }
}