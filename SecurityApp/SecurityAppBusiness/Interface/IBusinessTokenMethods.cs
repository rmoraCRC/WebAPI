using System.Collections.Generic;
using System.Linq;
using SecurityAppBusiness.Entity;

namespace SecurityAppBusiness.Interface
{
    public interface IBusinessTokenMethods
    {
        ITokenEntity GenerateToken(int userId);
        bool ValidateToken(string tokenId);
        bool Kill(string tokenId);
        bool DeleteByUserId(int userId);
        void UpdateStatus(ITokenEntity tokenEntity);
        IQueryable<ITokenEntity> GetTokenByUserId(int userId);
    }
}