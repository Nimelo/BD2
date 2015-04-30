using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DocumentsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DocumentsService.svc or DocumentsService.svc.cs at the Solution Explorer and start debugging.
    public class DocumentsService : IDocumentsService
    {

        public Document GetDocumentById(long id)
        {
            Document doc = null;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    doc = db.Documents.Where(x => x.Id == id).First();
                }
            }
            catch(Exception e)
            {

            }

            return doc;
        }


        public int SaveDocument(Document document, int candidateId, byte type)
        {
            try
            {
                using(var db = new DatabaseContainer())               
                {
                    document.CandidateId = candidateId;
                    document.Type = type;
                    db.Entry(document).State = document.Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }
            return document.Id;
        }


        public void DeleteDocumenyById(long id)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    db.Entry(db.Documents.Where(x => x.Id == id).First()).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}
