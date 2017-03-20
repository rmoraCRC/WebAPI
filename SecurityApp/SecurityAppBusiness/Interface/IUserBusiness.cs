using System;
using System.Collections.Generic;
using SecurityAppDataAccess.Interface;
using SecurityAppDataAccess.Model;

namespace SecurityAppBusiness.Interface
{
    public interface IUserBusiness : IBusinessMethods<IUserEntity>
    {
        int Authenticate(string userName, string password);
    }
}