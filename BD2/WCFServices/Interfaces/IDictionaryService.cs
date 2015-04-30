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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDictionaryService" in both code and config file together.
    [ServiceContract]
    public interface IDictionaryService :IBaseService
    {
        [OperationContract]
        int GetAmountOfSkills();

        [OperationContract]
        int GetAmountOfSoftSkills();

        [OperationContract]
        List<Skill> GetSkillsByPage(int pageNumber);

        [OperationContract]
        List<SoftSkill> GetSoftSkillsByPage(int pageNumber);

        [OperationContract]
        Skill GetSkillById(long id);

        [OperationContract]
        SoftSkill GetSoftSkillById(long id);

        [OperationContract]
        int SaveSkill(Skill skill);

        [OperationContract]
        int SaveSoftSkill(SoftSkill softSkill);

        [OperationContract]
        void DeleteSkill(Skill skill);


        [OperationContract]
        void DeleteSoftSkill(SoftSkill skill);


        [OperationContract]
        int SaveStage(Stage stage);

        [OperationContract]
        void DeleteStage(Stage stage);

        [OperationContract]
        int GetAmountOfStages();

        [OperationContract]
        List<Stage> GetStagesByPage(int pageNumber);

        [OperationContract]

        Stage GetStageById(long id);

        [OperationContract]
        List<string> GetStagesNames();
        [OperationContract]
        int GetStagePriorityByStageName(string name);


    }
}
