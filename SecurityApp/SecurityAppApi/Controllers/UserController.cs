using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using SecurityAppApi.ActionFilters;
using SecurityAppApi.ErrorHelper;
using SecurityAppBusiness.BusinessObject;
using SecurityAppBusiness.Entity;
using SecurityAppBusiness.Interface;

namespace SecurityAppApi.Controllers
{
    [EnableCors("*", "*", "GET,DELETE,PUT,POST")]
    [AuthorizationRequired]
    public class UserController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var user = UserBusiness.GetNewUser();
            var usersBusinesses = user.GetAll();
            if (usersBusinesses.Any())
                return Request.CreateResponse(HttpStatusCode.OK, usersBusinesses);
            throw new ApiDataException(1000, "Users not found", HttpStatusCode.NotFound);
        }
        public HttpResponseMessage Get(int id)
        {
            var userBusinesses = UserBusiness.GetNewUser().GetById(id);

            if (userBusinesses.Equals(null))
                throw new ApiDataException(1000, "User not found", HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, userBusinesses);
        }
        public IHttpActionResult Post(UserEntity userEntity)
        {
            UserBusiness.GetNewUser().Save(userEntity);
            return Ok();
        }
        public IHttpActionResult Put(UserEntity userEntity)
        {
            UserBusiness.GetNewUser().Update(userEntity);
            return Ok();
        }
        public IHttpActionResult Delete([FromBody]UserEntity userEntity)
        {
            var requestContent = Request.Headers.GetValues("body").FirstOrDefault();
            var entity = JsonConvert.DeserializeObject<UserEntity>(requestContent);
            UserBusiness.GetNewUser().Delete(entity);
            return Ok();
        }

        public HttpResponseMessage GetToken(int userId)
        {
            var token = TokenBusiness.GetNewToken().GetTokenByUserId(userId);
            if (token.Any())
                return Request.CreateResponse(HttpStatusCode.OK, token);
            throw new ApiDataException(1000, "Users not found", HttpStatusCode.NotFound);
        }
    }
}
