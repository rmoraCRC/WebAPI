using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecurityAppApi.ErrorHelper;
using SecurityAppBusiness.BusinessObject;
using SecurityAppBusiness.Entity;
using SecurityAppBusiness.Interface;

namespace SecurityAppApi.Controllers
{
    public class CustomerController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var customer = CustomerBusiness.GetNewCustomer();
            var customerEntities = customer.GetAll();
            if (customerEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, customerEntities);
            throw new ApiDataException(1000, "Customers not found", HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Get(int id)
        {
            var customerBusinesses = CustomerBusiness.GetNewCustomer().GetById(id);

            if (customerBusinesses.Equals(null))
                throw new ApiDataException(1000, "Customer not found", HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, customerBusinesses);
        }

        public IHttpActionResult Post(CustomerEntity customerEntity)
        {
            CustomerBusiness.GetNewCustomer().Save(customerEntity);
            return Ok();
        }

        public IHttpActionResult Put(CustomerEntity customerEntity)
        {
            CustomerBusiness.GetNewCustomer().Update(customerEntity);
            return Ok();
        }

        public IHttpActionResult Delete(CustomerEntity customerEntity)
        {
            CustomerBusiness.GetNewCustomer().Delete(customerEntity);
            return Ok();
        }
    }
}
