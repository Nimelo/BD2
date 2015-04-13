using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.ListsDataDevelopers
{
    public class PersonListDataDeveloper : AbstractListDataDeveloper
    {
        public override int GetAmountOfRecords()
        {
            int returnValue;
            using(var service = new PersonsServiceReference.PersonsServiceClient())
            {
                returnValue = service.GetAmountOfRecords();

            }

            return returnValue;
        }

        public override List<object> LoadData(int pageNumber)
        {
            List<object> returnList;
            using(var service = new PersonsServiceReference.PersonsServiceClient())
            {
                returnList = service.GetPersonByPage(pageNumber).Cast<object>().ToList();

            }
            return returnList;
        }

        public override List<object> Next()
        {
            List<object> returnList = null;
            

            return returnList;
        }

        public override List<object> Previous()
        {
            List<object> returnList = null;
            

            return returnList;
        }

        public PersonListDataDeveloper(int pageNumber = 1)
        {
            this.CurrentPageNumber = pageNumber;
        }

    }
}
