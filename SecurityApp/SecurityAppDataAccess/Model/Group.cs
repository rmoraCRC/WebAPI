using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using SecurityAppCustomException;
using SecurityAppDataAccess.Interface;

namespace SecurityAppDataAccess.Model
{
    [Table("securityapp.Groups")]
    public sealed class Group : IGroup
    {
        private static Group _groupInstance = null;

        #region properties
        [Key]
        public int IdGroup { get; set; }
        [StringLength(45)]
        public string Description { get; set; }
        public ICollection<Role> roles { get; set; }
        #endregion
        private Group()
        {
            roles = new HashSet<Role>();
        }
        public static Group GetNewGroup()
        {
            return _groupInstance ?? (_groupInstance = new Group());
        }
        public void Save()
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {

                    securityAppDbContext.Groups.Add(this);
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
        public IQueryable<IGroup> GetAll()
        {
            try
            {
                var securityAppDbContext = new SecurityAppDbContext();
                return securityAppDbContext.Groups;
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }
        public IGroup GetById(int id)
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    _groupInstance = securityAppDbContext.Groups.FirstOrDefault(x => x.IdGroup.Equals(id));
                    return GetNewGroup();
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
    }
}
