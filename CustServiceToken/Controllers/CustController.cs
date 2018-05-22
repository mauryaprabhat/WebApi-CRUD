using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CustomerServices;

namespace CustServiceToken.Controllers
{
    //[Authorize]
    public class CustController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (CustomerDBEntities entites = new CustomerDBEntities())
            {
                return Request.CreateResponse(HttpStatusCode.OK, entites.Customers.ToList());// entites.Customers.ToList();
            }
        }
    }
}
