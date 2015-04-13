using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Components;
using UI.Managers;

namespace ConsoleTests
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //WindowManager.DisplayEditableWindow(new CandidatDetails(), UI.Components.Enums.DetailsWindowModes.Readonly);

            //System.Windows.Window  a= new System.Windows.Window();
            //var b = new CandidatesList();
            //b.DataContext = new UI.Components.ViewModels.CandidatesListViewModel();
            //a.Content = b;
            //a.ShowDialog();

            using(Database.DatabaseContainer db = new Database.DatabaseContainer())
            {
                db.Documents.Add(new Database.Document()
                    {
                        CandidateId = db.Candidates.First().Id,
                        Extension = ".txt",
                        File = new byte[] {0,1},
                        Name = "coś",
                        Type = 0
                    });
                db.SaveChanges();
            }

        }
    }
}
