using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using SecurityAppCustomException;
using SecurityAppDataAccess.Interface;

namespace SecurityAppDataAccess.Model
{
    [Table("securityapp.Tokens")]
    public class Token : IToken
    {
        private static IToken _tokenInstance = null;
        #region properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdToken { get; set; }
        [Column("IdUser")]
        public int IdUser { get; set; }        
        public string AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool Status { get; set; }

        #endregion properties
        private Token()
        {
            
        }
        public static IToken GetNewToken()
        {
            return _tokenInstance ?? (_tokenInstance = new Token());
        }
        public IEnumerable<IToken> GetAll()
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    return securityAppDbContext.Tokens.ToList();
                }
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }

        public IToken GetById(int id)
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    return securityAppDbContext.Tokens.FirstOrDefault(x => x.IdToken.Equals(id));
                }
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }

        public IToken GetValidAuthToken(string id)
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    return securityAppDbContext.Tokens.FirstOrDefault(x => x.AuthToken.Equals(id) && x.Status.Equals(true));
                }
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }

        public IEnumerable<IToken> GetTokenByUserId(int userId)
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    return securityAppDbContext.Tokens.Where(x => x.IdUser.Equals(userId)).ToList();
                }
            }
            catch (Exception exception)
            {
                throw new EntityExceptionHandler(exception);
            }
        }

        public IToken GetAuthToken(string id)
        {
            try
            {
                using (var securityAppDbContext = new SecurityAppDbContext())
                {
                    return securityAppDbContext.Tokens.FirstOrDefault(x => x.AuthToken.Equals(id));
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
                    securityAppDbContext.Tokens.Add(this);
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