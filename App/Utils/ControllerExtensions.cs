using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace App.Utils
{
    public static class ControllerExtensions
    {
        public static UserModel GetUser(this ControllerBase controller,  IIdentity identity)
        {
            return new UserModel()
            {
                Username = string.IsNullOrEmpty(identity.Name)? "Guest" : identity.Name,
                IsAuthenticated = identity.IsAuthenticated
            };
        }

        public static string GetName(this ControllerBase controller)
        {
            return controller.GetType().Name.Replace("Controller", "");
        }
    }
}