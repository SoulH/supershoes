using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Api.Security
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public string BasicRealm { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }

        public BasicAuthenticationAttribute()
        {
            this.Username = "my_user";
            this.Password = "my_password";
        }

        public BasicAuthenticationAttribute(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!String.IsNullOrEmpty(auth))
            {
                var cred = ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                if (cred[0] == Username && cred[1] == Password) return;
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", $"Basic realm=\"{BasicRealm ?? "Realm"}\"");
            var result = new JsonResult()
            {
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = ErrorResponseModel.NotAuthorized
            };

            filterContext.Result = result;
        }
    }
}