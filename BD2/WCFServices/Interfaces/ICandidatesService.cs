using Common.Enums;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICandidatesService" in both code and config file together.
    [ServiceContract]
    public interface ICandidatesService : IBaseService
    {
        [OperationContract]
        int GetAmountOfRecords(CandidatesStagesEnum stage);

        [OperationContract]
        Candidate GetCandidateById(long id);

        [OperationContract]
        List<Candidate> GetCandidatesByPage(int pageNumber, CandidatesStagesEnum stage);

        [OperationContract]
        void Save(Candidate candidate);

        [OperationContract]
        void Delete(Candidate candidate);

        [OperationContract]
        void Modify(Candidate candidate);
        [OperationContract]
        int Add(Candidate candidate);

        [OperationContract]
        List<string> GetNotUsedSkillsNames(Candidate candidate);

        [OperationContract]
        List<string> GetNotUsedSoftSkillsNames(Candidate candidate);

        [OperationContract]

        void SaveSkill(SkillsEvaluation skillEvaluation);

        [OperationContract]

        long AddSkill(Candidate candidate, SkillsEvaluation skillEvaluation, string skillName);
        [OperationContract]

        Candidate GetCandidateBySkillEvaluationId(long skillEvaluationId);

        [OperationContract]

        void SaveSoftSkill(SoftSkillsEvaluation skillEvaluation);

        [OperationContract]

        long AddSoftSkill(Candidate candidate, SoftSkillsEvaluation skillEvaluation, string skillName);
        [OperationContract]

        Candidate GetCandidateBySoftSkillEvaluationId(long softSkillEvaluationId);

    }
}
