using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Text;

namespace serviceKud
{
    public class RequiredHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            
            if(actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p> Use Https instade of Http </p>", Encoding.UTF8, "text/html");

                UriBuilder uribuilder = new UriBuilder(actionContext.Request.RequestUri)
                {
                    Scheme = Uri.UriSchemeHttps, 
                    Port = 44346
                };
                actionContext.Response.Headers.Location = uribuilder.Uri;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}