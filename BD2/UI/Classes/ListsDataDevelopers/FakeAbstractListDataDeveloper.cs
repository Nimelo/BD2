using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.ListsDataDevelopers
{
    public class FakeAbstractListDataDeveloper<T> : AbstractListDataDeveloper
    {
        public override List<object> LoadData(int pageNumber)
        {
            return this.Collection.Cast<object>().ToList();
        }

        public override List<object> Next()
        {
            throw new NotImplementedException();
        }

        public override int GetAmountOfRecords()
        {
            return this.Collection.Count();
        }

        public override List<object> Previous()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<T> Collection { get; set; }


        public FakeAbstractListDataDeveloper()
        {
            this.Collection = new List<T>();
        }

        public FakeAbstractListDataDeveloper(IEnumerable<T> collection)
        {
            this.Collection = collection;
        }
        public virtual void InsertData(IEnumerable<T> collection)
        {
            this.Collection = collection;
        }
    }
}
