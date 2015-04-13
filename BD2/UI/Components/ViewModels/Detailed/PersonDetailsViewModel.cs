using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.PersonsServiceReference;

namespace UI.Components.ViewModels.Detailed
{
    public class PersonUserDetailsViewModel : AbstractDetailsViewModel
    {
        #region Model
        private Person person;

        public Person Person
        {
            get { return person; }
            set
            {
                person = value;
                this.Notify("Person");
            }
        }
        #endregion

        public PersonUserDetailsViewModel()
        {
            this.Person = new Person();
            this.person.User = new User();
        }


        public override void Refresh()
        {
            
        }
    }
}
