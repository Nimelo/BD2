using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.ListsDataDevelopers
{
    public abstract class AbstractListDataDeveloper
    {
        public abstract List<object> LoadData(int pageNumber);

        public abstract List<object> Next();

        public abstract int GetAmountOfRecords();

        public abstract List<object> Previous();

        private int currentPageNumber;

        public int CurrentPageNumber
        {
            get { return currentPageNumber; }
            set { currentPageNumber = value; }
        }

        private object parameter;

        public object Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }
        


    }
}
