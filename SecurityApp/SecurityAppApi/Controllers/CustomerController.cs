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
    public class CustomerController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var customer = CustomerBusinessObject.GetNewCustomer();
            var customerEntities = customer.GetAll();
            if (customerEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, customerEntities);
            throw new ApiDataException(1000, "Customers not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Get(int id)
        {
            var customerBusinesses = CustomerBusinessObject.GetNewCustomer().GetById(id);

            if (customerBusinesses.Equals(null))
                throw new ApiDataException(1000, "Customer not found", HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, customerBusinesses);
        }

        public IHttpActionResult Post(CustomerEntity customerEntity)
        {
            CustomerBusinessObject.GetNewCustomer().Save(customerEntity);
            return Ok();
        }

        public IHttpActionResult Put(CustomerEntity customerEntity)
        {
            CustomerBusinessObject.GetNewCustomer().Update(customerEntity);
            return Ok();
        }

        public IHttpActionResult Delete(CustomerEntity customerEntity)
        {
            CustomerBusinessObject.GetNewCustomer().Delete(customerEntity);
            return Ok();
        }
    }
}
