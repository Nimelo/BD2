using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using WebApp.Filters;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]

    public class AccountController : Controller
    {
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
        public ActionResult Login(CredentialsModel model, string returnUrl)
        {
            if(model.IsValid(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(PersonWebServiceReference.Person model)
        {
            int Id = 0;
            bool something = false;
            model.User.Role = (byte)Common.Enums.UserRolesEnum.Candidate;
            model.User.Login = model.Pesel;
            using(var service = new PersonWebServiceReference.PersonsService())
	        {
                service.SaveC(model, out Id, out something);
	        }

            if(Id != 0)
            {
                // Attempt to register the user
                try
                {
                    FormsAuthentication.SetAuthCookie(model.User.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                catch(MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            ModelState.AddModelError("", "Fields cannot be empty or Pesel is already in use!");
            // If we got this far, something failed, redisplay form
            return View(model);
        }



        //
        // GET: /Account/Manage

        public ActionResult Manage()
        {
            PersonWebServiceReference.Person p = null;
            ViewBag.ReturnUrl = Url.Action("Manage");
            using(var service = new PersonWebServiceReference.PersonsService())
            {
                p = service.GerPersonByLogin(this.GetLogin());
            }
            return View(p);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Manage(PersonWebServiceReference.Person model)
        {
            int Id = 0;
            bool something = false;
            model.User.Role = (byte)Common.Enums.UserRolesEnum.Candidate;
            model.User.Login = model.Pesel;
             using(var service = new PersonWebServiceReference.PersonsService())
            {
                model.Id =  service.GerPersonByLogin(this.GetLogin()).Id;
                model.IdSpecified = true;
                model.User.Id = service.GerPersonByLogin(this.GetLogin()).User.Id;
                model.User.IdSpecified = true;
            }

            using(var service = new PersonWebServiceReference.PersonsService())
            {
                service.SaveC(model, out Id, out something);
            }

            if(Id != 0)
            {
                // Attempt to register the user
                try
                {
                    FormsAuthentication.SetAuthCookie(model.User.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                catch(MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            ModelState.AddModelError("", "Fields cannot be empty or Pesel is already in use!");
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        private string GetLogin()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            return ticket.Name;
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch(createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
