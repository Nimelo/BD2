using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.Templates
{
    public class PersonListViewTemplate
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Pesel { get; set; }

        public long Id;

        public PersonListViewTemplate(long id)
        {
            this.Id = id;
        }
        public PersonListViewTemplate()
        {

        }
    }
}
