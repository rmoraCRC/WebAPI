using System;
using System.Configuration;
using System.Linq;
using AutoMapper;
using SecurityAppBusiness.Entity;
using SecurityAppBusiness.Interface;
using SecurityAppDataAccess.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.BusinessObject
{
    public class TokenBusiness : IBusinessTokenMethods
    {
        private static IBusinessTokenMethods _tokenInstance;
        private TokenBusiness()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Token, ITokenEntity>();
                cfg.CreateMap<ITokenEntity, Token>();
            });
        }
        public static IBusinessTokenMethods GetNewApplication()
        {
            return _tokenInstance ?? (_tokenInstance = new TokenBusiness());
        }
        public ITokenEntity GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(
                                              Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));

            var tokendomain = Token.GetNewToken();

            tokendomain.IdUser = userId;
            tokendomain.AuthToken = token;
            tokendomain.IssuedOn = issuedOn;
            tokendomain.ExpiresOn = expiredOn;
            tokendomain.Status = true;

            tokendomain.Save();

            return Mapper.Map<IToken, ITokenEntity>(tokendomain);
        }
        public bool ValidateToken(string authToken)
        {
            var newtoken = Token.GetNewToken();
            var token = newtoken.GetValidAuthToken(authToken);

            if (token != null && token.Status)
            {
                return true;
            }
            return false;
        }
        public bool Kill(string tokenId)
        {
            var newtoken = Token.GetNewToken();
            var token = newtoken.GetAuthToken(tokenId);

            token.Delete();
            var isNotDeleted = newtoken.GetAll().Any(x => x.AuthToken == tokenId);
            return !isNotDeleted;
        }
        public bool DeleteByUserId(int userId)
        {
            var newtoken = Token.GetNewToken();
            var token = newtoken.GetAll();

            var firstOrDefault = token.FirstOrDefault(x => x.IdUser.Equals(userId));
            firstOrDefault?.Delete();

            var isNotDeleted = newtoken.GetAll().Any(x => x.IdUser == userId);
            return !isNotDeleted;

        }
        public void UpdateStatus(ITokenEntity tokenEntity)
        {
            var dataUser = Mapper.Map<ITokenEntity, Token>(tokenEntity);
            dataUser.Update();
        }
    }
}