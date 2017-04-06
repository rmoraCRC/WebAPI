namespace SecurityAppBusiness.Interface
{
    public interface IUserBusinessServices : IBusinessServicesMethods<IUserEntity>
    {
        int Authenticate(string userName, string password);
    }
}