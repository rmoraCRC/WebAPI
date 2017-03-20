using System;
using SecurityAppDataAccess.Model;

namespace SecurityAppDataAccess.Interface
{
    public interface IToken : IDataAccessMethods<IToken>
    {
        int IdToken { get; set; }
        int IdUser { get; set; }
        string AuthToken { get; set; }
        DateTime IssuedOn { get; set; }
        DateTime ExpiresOn { get; set; }
        bool Status { get; set; }
        IToken GetAuthToken(string id);
        IToken GetValidAuthToken(string id);
    }
}