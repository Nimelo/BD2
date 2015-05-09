using Common.Interfaces;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPersonsService" in both code and config file together.
    [ServiceContract]
    public interface IPersonsService : IBaseService
    {
        [OperationContract]
        Person GerPersonByLogin(string login);

        [OperationContract]
        int GetAmountOfRecords();

        [OperationContract]
        Person GetPersonById(long id);

        [OperationContract]
        List<Person> GetPersonByPage(int pageNumber);

        [OperationContract]
        int Save(Person person);

        [OperationContract]
        int SaveC(Person person);

        [OperationContract]
        void Delete(long personId);

    }
}
