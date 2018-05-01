﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CustomerServices;
using System.Web.Http.Cors;

namespace serviceKud.Controllers
{
    [EnableCorsAttribute("http://localhost:53742/", "*","*")]
    public class CustomerController : ApiController
    {
        // http://localhost:53742/api/customer?gender=male
        [DisableCors]
        [HttpGet]
        public HttpResponseMessage AllGet(string gender="all")
        {
            try
            {
                using (CustomerDBEntities ctx = new CustomerDBEntities())
                {
                    switch (gender.ToLower())
                    {
                        case "all":
                            return Request.CreateResponse(HttpStatusCode.OK, ctx.Customers.ToList());
                        case "male":
                            return Request.CreateResponse(HttpStatusCode.OK, ctx.Customers.Where(x => x.gender == "male").ToList());
                        case "female":
                            return Request.CreateResponse(HttpStatusCode.OK, ctx.Customers.Where(x => x.gender == "female").ToList());
                        default:
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Select male or female gender");
                    }

                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (CustomerDBEntities ctx = new CustomerDBEntities())
                {
                    var customer = ctx.Customers.FirstOrDefault(x => x.ID == id);
                    if (customer != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, customer);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, " Customer not found for -" + id.ToString());
                    }

                }
            }catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        public HttpResponseMessage Post([FromBody] Customer customer)
        {
            try
            {
                using (CustomerDBEntities entity = new CustomerDBEntities())
                {
                    entity.Customers.Add(customer);
                    entity.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, customer);
                    return message;
                }
            } catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (CustomerDBEntities entity = new CustomerDBEntities())
                {
                    var deletedCustomer = entity.Customers.Remove(entity.Customers.FirstOrDefault(x=> x.ID == id));
                    if(deletedCustomer == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Delete for id" + id.ToString() + "unsuccessfull");
                    }
                    else
                    {
                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, deletedCustomer);
                    }
                   
                }
            }catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Customer customer)
        {
            try
            {
                using (CustomerDBEntities entity = new CustomerDBEntities())
                {
                    var cust = entity.Customers.FirstOrDefault(x => x.ID == id);
                    if(cust !=null)
                    {
                        cust.FirstName = customer.FirstName;
                        cust.LastName = customer.LastName;
                        cust.Email = customer.Email;
                        cust.Mobile = customer.Mobile;
                        cust.Address = customer.Address;
                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, cust.ToString());
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, id.ToString() + "Not found!");
                    }

                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }

}
