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
            List<object> returnList = new List<object>();
            using(var service = new CandidatesServiceReference.CandidatesServiceClient())
            {
                
                if(this.Parameter.GetType() == typeof(int))
                {
                    if((int)this.Parameter == -1)
                    returnList = service.GetAllCandidatesByPage(pageNumber).Cast<object>().ToList();
                    else
                        returnList = service.GetCandidatesByPage(pageNumber, (int)this.Parameter).Cast<object>().ToList();
                }
                   
                else if(this.Parameter.GetType() == typeof(byte))
                    returnList = service.GetCandidatesByPageDecisionType(pageNumber, (byte)this.Parameter).Cast<object>().ToList();
                
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

                if(this.Parameter.GetType() == typeof(int))
                {
                    if((int)this.Parameter == -1)
                        returnValue = service.GetAmountOfAllRecords();
                    else
                    returnValue = service.GetAmountOfRecords((int)this.Parameter);
                }
                else if(this.Parameter.GetType() == typeof(byte))
                {
                    returnValue = service.GetAmountOfRecordsDecisionType((byte)this.Parameter);
                }
                else
                {
                    returnValue = 0;
                }

            }

            return returnValue;
        }

        public override List<object> Previous()
        {
            throw new NotImplementedException();
        }

        public CandidateListDataDeveloper()
        {
            this.Parameter = -1;
        }

        public CandidateListDataDeveloper(object parameter)
        {
            this.Parameter = parameter;
        }
    }
}
