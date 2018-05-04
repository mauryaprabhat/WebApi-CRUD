using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;

namespace serviceKud
{
    public class BasicAuthenticationAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodeAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string[] usernamepasswordArray = decodeAuthToken.Split(':');
                string username = usernamepasswordArray[0];
                string password = usernamepasswordArray[1];
               bool loginStatus = CustomerSecurity.Login(username, password);
                if(loginStatus)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
            base.OnAuthorization(actionContext);
        }
    }
}