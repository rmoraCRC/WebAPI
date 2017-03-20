namespace SecurityAppBusiness.Interface
{
    public interface ITokenEntity
    {
        int TokenId { get; set; }
        int UserId { get; set; }
        string AuthToken { get; set; }
        System.DateTime IssuedOn { get; set; }
        System.DateTime ExpiresOn { get; set; }
        bool Status { get; set; }
    }
}