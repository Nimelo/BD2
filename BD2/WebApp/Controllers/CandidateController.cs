using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApp.Controllers
{
    public class CandidateController : Controller
    {
        //
        // GET: /Candidate/

        public ActionResult Stages()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            CandidateWebServiceReference.Candidate candidate = null;
            using(var service = new CandidateWebServiceReference.CandidatesService())
            {
               candidate =  service.GetCandidateByLogin(ticket.Name);
            }

            if(candidate == null)
            {
                    ModelState.AddModelError("", "Error in stages!");
                    return RedirectToAction("Index", "Home");
            }

            return View(candidate);
        }


        public ActionResult Documents()
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            CandidateWebServiceReference.Candidate candidate = null;
            using(var service = new CandidateWebServiceReference.CandidatesService())
            {
                candidate = service.GetCandidateByLogin(ticket.Name);
            }

            if(candidate == null)
            {
                ModelState.AddModelError("", "Error in documents!");
                return RedirectToAction("Index", "Home");
            }

            return View(candidate);
        }
    }
}
