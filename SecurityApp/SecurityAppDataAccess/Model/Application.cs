using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using SecurityAppCustomException;
using SecurityAppDataAccess.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppDataAccess.Model
{
    [Table("securityapp.Applications")]
    public sealed class Application : IApplication
    {
        private static IApplication _applicationInstance = null;
        #region properties
        [Key]
        public int IdApplication { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreateDateTime { get; set; }

        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Role> roles { get; set; }
        #endregion
        private Application()
        {
            roles = new HashSet<Role>();
        }
        public static IApplication GetNewApplication()
        {
            return _applicationInstance ?? (_applicationInstance = new Application());
        }
        public IEnumerable<IApplication> GetAll()
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    return securityAppDbContext.Applications.ToList();
                }
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }
        public IApplication GetById(int id)
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    return securityAppDbContext.Applications.FirstOrDefault(x => x.IdApplication.Equals(id));
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

                    securityAppDbContext.Applications.Add(this);
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
