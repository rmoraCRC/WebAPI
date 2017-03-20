using System.Net;

namespace SecurityAppApi.ErrorHelper
{
    public interface IApiExceptions
    {
        int ErrorCode { get; set; }
        string ErrorDescription { get; set; }
        HttpStatusCode HttpStatus { get; set; }
        string ReasonPhrase { get; set; }
    }
}
