using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApp.Controllers
{
    public class DocumentController : Controller
    {
        //
        // GET: /Document/

        public ActionResult Index()
        {
            CandidateWebServiceReference.Candidate c = null;
            using(var service = new CandidateWebServiceReference.CandidatesService())
            {
                c = service.GetCandidateByLogin(this.GetLogin());
            }

            return View(c);
        }

        private string GetLogin()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            return ticket.Name;
        }

        //
        // GET: /Document/Details/5

        public ActionResult Details(int id)
        {
            DocumentWebServiceReference.Document d = null;
            using(var service = new DocumentWebServiceReference.DocumentsService())
            {
                d = service.GetDocumentById(id, true);
            }
            return File(d.File, d.Extension, d.Name + d.Extension);
            
        }

        //
        // GET: /Document/Create

        public ActionResult Create()
        {
            return View(new DocumentWebServiceReference.Document());
        }

        //
        // POST: /Document/Create

        [HttpPost]
        public ActionResult Create(DocumentWebServiceReference.Document document)
        {
            try
            {
                byte[] buffer = new byte[Request.Files["file1"].ContentLength];
                Request.Files["file1"].InputStream.Read(buffer, 0, buffer.Length);
                document.Extension = Request.Files["file1"].FileName.Substring(Request.Files["file1"].FileName.LastIndexOf('.'));
                document.File = buffer;

                using(var service = new CandidateWebServiceReference.CandidatesService())
                {
                    document.CandidateId = service.GetCandidateByLogin(this.GetLogin()).Id;
                }

                using(var service = new DocumentWebServiceReference.DocumentsService())
                {
                    int ID;
                    bool result;
                    service.SaveDocument(document, document.CandidateId, true, document.Type, true, out ID, out result);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Document/Edit/5

        public ActionResult Edit(int id)
        {
            DocumentWebServiceReference.Document doc;

            using(var service = new DocumentWebServiceReference.DocumentsService())
            {
               doc =  service.GetDocumentById(id, true);
            }

            return View(doc);
        }

        //
        // POST: /Document/Edit/5

        [HttpPost]
        public ActionResult Edit(DocumentWebServiceReference.Document document)
        {
            try
            {
                DocumentWebServiceReference.Document tmpDoc = document;
                if(Request.Files["file1"].FileName != string.Empty)
                {
                    byte[] buffer = new byte[Request.Files["file1"].ContentLength];
                    Request.Files["file1"].InputStream.Read(buffer, 0, buffer.Length);
                    document.Extension = Request.Files["file1"].FileName.Substring(Request.Files["file1"].FileName.LastIndexOf('.'));
                    document.File = buffer;
                }
                else
                {
                    using(var service = new DocumentWebServiceReference.DocumentsService())
                    {
                        tmpDoc = service.GetDocumentById(document.Id, true);
                    }
                         
                }

                document.CandidateIdSpecified = true;

                using(var service = new CandidateWebServiceReference.CandidatesService())
                {
                    document.CandidateId = service.GetCandidateByLogin(this.GetLogin()).Id;
                }

                using(var service = new DocumentWebServiceReference.DocumentsService())
                {
                    int ID;
                    bool result;
                    service.SaveDocument(tmpDoc, document.CandidateId, true, document.Type, true, out ID, out result);
                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }

        //
        // GET: /Document/Delete/5

        public ActionResult Delete(long id)
        {
            using(var service = new DocumentWebServiceReference.DocumentsService())
            {
                service.DeleteDocumenyById((long)id, true);
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Document/Delete/5
    }
}
