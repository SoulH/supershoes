using Api.Models;
using Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [Authorize]
    [RoutePrefix("Services/Account")]
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager => Request.GetOwinContext().Authentication;

        [HttpPost]
        [AllowAnonymous]
        [Route("Authenticate")]
        public async Task<IHttpActionResult> Authenticate(UserModel model)
        {
            if (!ModelState.IsValid)
                return Json(ResponseModel<UserModel>.BadRequest);

            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            if (result.Equals(SignInStatus.Success))
            {
                var tmodel = new TokenModel() { Username = model.Username, Token = TokenGenerator.GenerateTokenJwt(model.Username) };
                return Json(new ResponseModel<TokenModel>(tmodel));
            }
            else
            {
                return Json(ResponseModel<UserModel>.NotAuthorized);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel model)
        {
            if (!ModelState.IsValid)
                return Json(ResponseModel<UserModel>.BadRequest);
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return Json(new ResponseModel<UserModel>());
            else if (result.Errors.Count() > 0)
                return Json(ResponseModel<UserModel>.BadRequest);
            else
                return Json(ResponseModel<UserModel>.ServerError);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
                return Json(ResponseModel<ChangePasswordBindingModel>.BadRequest);

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
                return Json(ResponseModel<ChangePasswordBindingModel>.BadRequest);

            return Json(new ResponseModel<ChangePasswordBindingModel>());
        }
        
        [HttpPost]
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
                return Json(ResponseModel<SetPasswordBindingModel>.BadRequest);

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
                return Json(ResponseModel<SetPasswordBindingModel>.BadRequest);

            return Json(new ResponseModel<SetPasswordBindingModel>());
        }

        [HttpPost]
        [Route("LogOut")]
        public IHttpActionResult LogOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Json(new ResponseModel<UserModel>());
        }

        [HttpGet]
        [Route("Auth")]
        public IHttpActionResult Auth()
        {
            var model = new UserModel() { Username = User.Identity.Name };
            return Json(new ResponseModel<UserModel>(model));
        }
    }
}
