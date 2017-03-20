using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using SecurityAppCustomException;
using SecurityAppDataAccess.Interface;

namespace SecurityAppDataAccess.Model
{
    [Table("securityapp.Users")]
    public sealed class User : IUser
    {
        private static IUser _userInstance = null;

        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        [StringLength(45)]
        public string LastName { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Index(IsUnique = true)]
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(45)]
        public string Phone { get; set; }

        public bool Status { get; set; }

        [Index(IsUnique = true)]
        [Column("UserName")]
        [Required]
        [StringLength(45)]
        public string UserName { get; set; }

        [Required]
        [StringLength(1000)]
        public string Password { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreateDateTime { get; set; }

        public ICollection<Role> Roles { get; set; }

        #endregion

        private User()
        {
            Roles = new HashSet<Role>();
        }
        public static IUser GetNewUser()
        {
            return _userInstance ?? (_userInstance = new User());
        }
        public void Save()
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    securityAppDbContext.Entry(this).State = EntityState.Added;
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
        public IQueryable<IUser> GetAll()
        {
            try
            {
                var securityAppDbContext = new SecurityAppDbContext();
                return securityAppDbContext.Users;
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }
        public IUser GetById(int id)
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    _userInstance = securityAppDbContext.Users.FirstOrDefault(x => x.IdUser.Equals(id));
                }
                return GetNewUser();
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
        public int Authenticate(string userName, string password)
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    _userInstance = securityAppDbContext.Users.FirstOrDefault(x => x.UserName.Equals(userName) && x.Password.Equals(password));
                }
                return _userInstance?.IdUser ?? 0;
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }
    }
}