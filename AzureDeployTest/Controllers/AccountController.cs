using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AzureDeployTest.Core;
using Microsoft.AspNet.Identity;
using NLog;

namespace IMSParamed.Ebix.Webservice.Controllers
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class SiteUser:IUser<string>
    {
        public SiteUser(){
            Id = Guid.NewGuid().ToString();
        }
        public SiteUser(string userName): this()
        {
            UserName = userName;
            Id = userName;
        }
        public string Id { get; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    [Authorize]
    public class AccountController : Controller
    {
        private static readonly Logger LOGGER = LogManager.GetLogger("AccountController");
        
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                SiteUser user = null;
                if (model.UserName=="admin" && model.Password==Config.DefaultConfig.SitePassword)
                {
                    user = new SiteUser(model.UserName);
                }
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult LogOut()
        {
            var ctx = HttpContext.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        #region Helpers


        private async Task SignInAsync(SiteUser user, bool isPersistent)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
            claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", user.UserName));
            var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            var ctx = HttpContext.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(id);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

       
    }
}