using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.DictionaryServiceReference;

namespace UI.Classes.ListsDataDevelopers
{
    public class StageListDataDeveloper : AbstractListDataDeveloper
    {
        public override List<object> LoadData(int pageNumber)
        {
            List<object> returnList = new List<object>();
            try
            {
                using(var service = new DictionaryServiceClient())
                {
                    returnList = service.GetStagesByPage(pageNumber).Cast<object>().ToList();
                }
            }
            catch(Exception e)
            {

                throw;
            }

            return returnList;
        }

        public override List<object> Next()
        {
            throw new NotImplementedException();
        }

        public override int GetAmountOfRecords()
        {
            int amount = 0;
            try
            {
                using(var service = new DictionaryServiceClient())
                {
                    amount = service.GetAmountOfStages();
                }
            }
            catch(Exception e)
            {

                throw;
            }

            return amount;
        }

        public override List<object> Previous()
        {
            throw new NotImplementedException();
        }
    }
}
