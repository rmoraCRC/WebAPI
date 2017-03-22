using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SecurityAppApi.ErrorHelper;
using SecurityAppApi.Filters;
using SecurityAppBusiness.BusinessObject;

namespace SecurityAppApi.Controllers
{
    [EnableCors("*", "*", "GET,DELETE,PUT,POST")]
    [ApiAuthenticationFilter]
    public class AuthenticateController : ApiController
    {
        public HttpResponseMessage Authenticate()
        {
            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;
                    return GetAuthToken(userId);
                }
            }
            return null;
        }

        public HttpResponseMessage GetToken(int userId)
        {
            var token = TokenBusiness.GetNewToken().GetTokenByUserId(userId);
            if (token.Any())
                return Request.CreateResponse(HttpStatusCode.OK, token);
            throw new ApiDataException(1000, "Users not found", HttpStatusCode.NotFound);
        }

        private HttpResponseMessage GetAuthToken(int userId)
        {
            var token = TokenBusiness.GetNewToken().GenerateToken(userId);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            return response;
        }
    }
}
