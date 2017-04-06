namespace SecurityAppDataAccess.Interface
{
    public interface IUserAuthenticationMethods
    {
        int Authenticate(string userName, string password);
    }
}