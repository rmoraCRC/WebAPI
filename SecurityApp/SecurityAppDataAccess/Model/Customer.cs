using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using SecurityAppCustomException;
using SecurityAppDataAccess.Interface;

namespace SecurityAppDataAccess.Model
{
    [Table("securityapp.Customers")]
    public sealed class Customer : ICustomer
    {
        private static ICustomer _customerInstance = null;
        #region properties
        [Key]
        public int IdCompany { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

        [StringLength(45)]
        public string Address { get; set; }

        [StringLength(45)]
        public string Email { get; set; }

        [StringLength(45)]
        public string Phone { get; set; }

        [StringLength(45)]
        public string Status { get; set; }
        #endregion
        private Customer()
        {

        }
        public static ICustomer GetNewCustomer()
        {
            return _customerInstance ?? (_customerInstance = new Customer());
        }
        public IQueryable<ICustomer> GetAll()
        {
            try
            {
                var securityAppDbContext = new SecurityAppDbContext();
                return securityAppDbContext.Customers;
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }
        public ICustomer GetById(int id)
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    _customerInstance = securityAppDbContext.Customers.FirstOrDefault(x => x.IdCompany.Equals(id));
                    return GetNewCustomer();
                }
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }
        public void Save()
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    securityAppDbContext.Customers.Add(this);
                    securityAppDbContext.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }
        public void Update()
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {

                    securityAppDbContext.Entry(this).State = EntityState.Modified;
                    securityAppDbContext.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }
        public void Delete()
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    securityAppDbContext.Entry(this).State = EntityState.Deleted;
                    securityAppDbContext.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }
    }
}
