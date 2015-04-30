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
        int GetCandidateIdByLogin(string login);
        [OperationContract]
        int GetAmountOfRecords(int stage);

        [OperationContract]
        int GetAmountOfAllRecords();

        [OperationContract]
        Candidate GetCandidateById(long id);

        [OperationContract]
        Candidate GetCandidateByLogin(string login);

        [OperationContract]
        List<Candidate> GetCandidatesByPage(int pageNumber, int stage);

        [OperationContract]
        List<Candidate> GetAllCandidatesByPage(int pageNumber);

        [OperationContract]
        int Save(Candidate candidate);

        [OperationContract]
        void Delete(long candidateId);

        [OperationContract]
        List<string> GetNotUsedSkillsNames(Candidate candidate);

        [OperationContract]
        List<string> GetNotUsedSoftSkillsNames(Candidate candidate);

        [OperationContract]
        List<string> GetNotUsedStageNames(Candidate candidate);

        [OperationContract]

        void SaveSkill(SkillsEvaluation skillEvaluation);

        [OperationContract]

        long AddSkill(Candidate candidate, SkillsEvaluation skillEvaluation, string skillName);
        [OperationContract]

        Candidate GetCandidateBySkillEvaluationId(long skillEvaluationId);

        [OperationContract]

        Candidate GetCandidateByRecruitmentStageId(long recruitmentStageId);

        [OperationContract]

        Candidate GetCandidateBySoftSkillEvaluationId(long softSkillEvaluationId);

        [OperationContract]

        void SaveSoftSkill(SoftSkillsEvaluation skillEvaluation);

        [OperationContract]

        long AddSoftSkill(Candidate candidate, SoftSkillsEvaluation skillEvaluation, string skillName);

        [OperationContract]
        long SaveRecriutmentStage(RecruitmentStage rs);

        [OperationContract]
        void DeleteRecriutmentStage(RecruitmentStage rs);

    }
}
