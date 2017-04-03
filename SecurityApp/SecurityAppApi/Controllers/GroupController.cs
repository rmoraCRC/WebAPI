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
    public class GroupController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var group = GroupBusiness.GetNewGroup();
            var groupEntities = group.GetAll();
            if (groupEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, groupEntities);
            throw new ApiDataException(1000, "Groups not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Get(int id)
        {
            var groupBusinesses = GroupBusiness.GetNewGroup().GetById(id);

            if (groupBusinesses.Equals(null))
                throw new ApiDataException(1000, "Group not found", HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, groupBusinesses);
        }

        public IHttpActionResult Post(GroupEntity groupEntity)
        {
            GroupBusiness.GetNewGroup().Save(groupEntity);
            return Ok();
        }

        public IHttpActionResult Put(GroupEntity groupEntity)
        {
            GroupBusiness.GetNewGroup().Update(groupEntity);
            return Ok();
        }

        public IHttpActionResult Delete([FromUri]GroupEntity groupEntity)
        {
            GroupBusiness.GetNewGroup().Delete(groupEntity);
            return Ok();
        }
    }
}
