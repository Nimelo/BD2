using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.ListsDataDevelopers
{
    public class CandidateListDataDeveloper : AbstractListDataDeveloper
    {
        public override List<object> LoadData(int pageNumber)
        {
            List<object> returnList;
            using(var service = new CandidatesServiceReference.CandidatesServiceClient())
            {
                returnList = service.GetCandidatesByPage(pageNumber, (CandidatesStagesEnum)this.Parameter).Cast<object>().ToList();
            }
            return returnList;
        }

        public override List<object> Next()
        {
            throw new NotImplementedException();
        }

        public override int GetAmountOfRecords()
        {
            int returnValue;
            using(var service = new CandidatesServiceReference.CandidatesServiceClient())
            {
                returnValue = service.GetAmountOfRecords((CandidatesStagesEnum)this.Parameter);
            }

            return returnValue;
        }

        public override List<object> Previous()
        {
            throw new NotImplementedException();
        }

        public CandidateListDataDeveloper()
        {
            this.Parameter = CandidatesStagesEnum.AllStages;
        }

        public CandidateListDataDeveloper(CandidatesStagesEnum parameter)
        {
            this.Parameter = parameter;
        }
    }
}
