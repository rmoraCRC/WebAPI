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
using SecurityAppBusiness.BusinessServices;
using SecurityAppBusiness.Entity;
using SecurityAppBusiness.Interface;

namespace SecurityAppApi.Controllers
{
    [EnableCors("*", "*", "GET,DELETE,PUT,POST")]
    [AuthorizationRequired]
    public class ApplicationController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var application = new ApplicationBusinessObject(new ApplicationBusinessService());
            var applicationEntities = application.GetAll();
            if (applicationEntities != null  && applicationEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, applicationEntities);
            throw new ApiDataException(1000, "Applications not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Get(int id)
        {
            var applicationBusinesses = new ApplicationBusinessObject(new ApplicationBusinessService()).GetById(id);

            if (applicationBusinesses.Equals(null))
                throw new ApiDataException(1000, "Application not found", HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, applicationBusinesses);
        }

        public IHttpActionResult Post(ApplicationEntity applicationEntity)
        {
            new ApplicationBusinessObject(new ApplicationBusinessService()).Save(applicationEntity);
            return Ok();
        }

        public IHttpActionResult Put(ApplicationEntity applicationEntity)
        {
            new ApplicationBusinessObject(new ApplicationBusinessService()).Update(applicationEntity);
            return Ok();
        }

        public IHttpActionResult Delete([FromBody]ApplicationEntity applicationEntity)
        {
            var requestContent = Request.Headers.GetValues("body").FirstOrDefault();
            var entity = JsonConvert.DeserializeObject<ApplicationEntity>(requestContent);
            new ApplicationBusinessObject(new ApplicationBusinessService()).Delete(entity);
            return Ok();
        }
    }
}
