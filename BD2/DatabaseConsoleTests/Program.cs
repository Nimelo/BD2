using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //  using(
            //var db = new Database.DatabaseContainer())
            //  { 

            //db.Documents.Add(new Database.Document()
            //{
            //   CandidateId =7,
            //   Extension =".xd",
            //   Name ="asd",
            //   Type = 0,
            //   File = new byte[1000000]
            //});
            //db.SaveChanges();
            //zaczynam od 1MB
            for(int i = 4000 * 1000; i < 1000 * 1000 * 1000; i+=1000 * 1000)
            {
                using(var ser = new ServiceReference1.DocumentsServiceClient())
                {
                    ser.SaveDocument(new Database.Document()
                    {
                        CandidateId = 7,
                        Extension = ".xd",
                        Name = "asd",
                        Type = 0,
                        File = new byte[i]//[10 * 1000 * 1000]   524288
                    }, 7, 0);
                }
                Console.WriteLine(i.ToString());
            }
            }
                 

      //  }
    }
}
