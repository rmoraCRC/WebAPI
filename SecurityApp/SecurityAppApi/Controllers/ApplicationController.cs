using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SecurityAppApi.ActionFilters;
using SecurityAppApi.ErrorHelper;
using SecurityAppBusiness.BusinessObject;
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
            var application = ApplicationBusiness.GetNewApplication();
            var applicationEntities = application.GetAll();
            if (applicationEntities != null  && applicationEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, applicationEntities);
            throw new ApiDataException(1000, "Applications not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Get(int id)
        {
            var applicationBusinesses = ApplicationBusiness.GetNewApplication().GetById(id);

            if (applicationBusinesses.Equals(null))
                throw new ApiDataException(1000, "Application not found", HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, applicationBusinesses);
        }

        public IHttpActionResult Post(ApplicationEntity applicationEntity)
        {
            ApplicationBusiness.GetNewApplication().Save(applicationEntity);
            return Ok();
        }

        public IHttpActionResult Put(ApplicationEntity applicationEntity)
        {
            ApplicationBusiness.GetNewApplication().Update(applicationEntity);
            return Ok();
        }

        public IHttpActionResult Delete(ApplicationEntity applicationEntity)
        {
            ApplicationBusiness.GetNewApplication().Delete(applicationEntity);
            return Ok();
        }
    }
}
