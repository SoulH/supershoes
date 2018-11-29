using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace Api.Security
{
    public class BasicHttpAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
               actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ErrorResponseModel.NotAuthorized, "application/json");
            else
            {
                try
                {
                    // Gets header parameters  
                    string authenticationString = actionContext.Request.Headers.Authorization.Parameter;
                    string originalString = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationString));

                    // Gets username and password  
                    string username = originalString.Split(':')[0];
                    string password = originalString.Split(':')[1];

                    // Validate username and password  
                    if (!VaidateUser(username, password))
                    {
                        // returns unauthorized error  
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ErrorResponseModel.NotAuthorized, "application/json");
                    }
                }
                catch(Exception)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ErrorResponseModel.NotAuthorized, "application/json");
                }
            }

            base.OnAuthorization(actionContext);
        }

        public static bool VaidateUser(string username, string password) => (username == "my_user") && (password == "my_password");
    }
}