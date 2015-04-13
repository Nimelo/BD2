using Common.Enums;
using Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CandidatesService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CandidatesService.svc or CandidatesService.svc.cs at the Solution Explorer and start debugging.
    public class CandidatesService : ICandidatesService
    {
        public int GetAmountOfRecords(CandidatesStagesEnum stage)
        {
            int amount = -1;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    if(stage == CandidatesStagesEnum.AllStages)
                    {
                        amount = db.RecruitmentStages
                             .Where(x => x.Stage == 0).Count();
                    }
                    else
                    {
                        amount = db.RecruitmentStages
                             .Where(x => x.Stage == (byte)stage).Count();
                    }
                }
            }
            catch(Exception e)
            {

            }

            return amount;
        }

        public Candidate GetCandidateById(long id)
        {
            Candidate candidate = null;

            try
            {
                using(var db = new DatabaseContainer())
                {
                    candidate = db.Candidates
                        .Include("Person")
                        .Include("Document")
                        .Include("Decision")
                        .Include("RecruitmentStage")
                        .Include("Evaluation")
                        .Include("Evaluation.SkillsEvaluation")
                        .Include("Evaluation.SkillsEvaluation.Skill")
                        .Include("Evaluation.SoftSkillsEvaluation")
                        .Include("Evaluation.SoftSkillsEvaluation.SoftSkill")
                        .Where(x => x.Id == id).SingleOrDefault();

                }
            }
            catch(Exception e)
            {

            }

            return candidate;
        }

        public List<Candidate> GetCandidatesByPage(int pageNumber, CandidatesStagesEnum stage)
        {
            var returnList = new List<Candidate>();
            if(pageNumber <= 0)
                return returnList;

            try
            {
                using(var db = new DatabaseContainer())
                {

                    if(( pageNumber - 1 ) * 10 > this.GetAmountOfRecords(stage))
                        return returnList;

                    IQueryable<Candidate> tmp = null;
                    if(stage == CandidatesStagesEnum.AllStages)
                    {
                        tmp = db.Candidates
                     .Include("Person")
                    .Include("Document")
                    .Include("Decision")
                    .Include("RecruitmentStage")
                    .Include("Evaluation")
                    .Include("Evaluation.SkillsEvaluation")
                    .Include("Evaluation.SkillsEvaluation.Skill")
                    .Include("Evaluation.SoftSkillsEvaluation")
                    .Include("Evaluation.SoftSkillsEvaluation.SoftSkill")
                    .OrderBy(x => x.Id)
                    .Skip(( pageNumber - 1 ) * 10);

                    }
                    else
                    {
                        tmp = (
                        ( db.RecruitmentStages
                        .Include("Candidate")
                        .Where(x => x.Stage == (byte)stage
                        && x.IsCurrent == true)
                        .Select(y => y.Candidate) ) as DbQuery<Candidate> )
                        .Include("Person")
                        .Include("Document")
                        .Include("Decision")
                        .Include("RecruitmentStage")
                        .Include("Evaluation")
                        .Include("Evaluation.SkillsEvaluation")
                        .Include("Evaluation.SkillsEvaluation.Skill")
                        .Include("Evaluation.SoftSkillsEvaluation")
                        .Include("Evaluation.SoftSkillsEvaluation.SoftSkill")
                        .OrderBy(x => x.Id)
                        .Skip(( pageNumber - 1 ) * 10);

                    }

                    tmp = tmp.Take(tmp.Count() >= 10 ? 10 : tmp.Count());
                    return tmp.ToList();

                }
            }
            catch(Exception e)
            {
                return returnList;
            }

        }

        public void Save(Candidate candidate)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    db.Candidates.Attach(candidate);
                    db.Entry(candidate).State = System.Data.Entity.EntityState.Modified;

                    foreach(var doc in candidate.Document)
                    {
                        db.Entry(doc).State = System.Data.Entity.EntityState.Modified;
                    }

                    foreach(var rs in candidate.RecruitmentStage)
                    {
                        db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                    }

                    db.Entry(candidate.Evaluation).State = System.Data.Entity.EntityState.Modified;

                    foreach(var se in candidate.Evaluation.SkillsEvaluation)
                    {
                        db.Entry(se).State = System.Data.Entity.EntityState.Modified;
                    }

                    foreach(var sse in candidate.Evaluation.SoftSkillsEvaluation)
                    {
                        db.Entry(sse).State = System.Data.Entity.EntityState.Modified;
                    }

                }
            }
            catch(Exception e)
            {


            }
        }

        public void Delete(Candidate candidate)
        {
            throw new NotImplementedException();
        }

        public void Modify(Candidate candidate)
        {
            throw new NotImplementedException();
        }

        public int Add(Candidate candidate)
        {
            throw new NotImplementedException();
        }

        public void Ping()
        {
            throw new NotImplementedException();
        }

        public List<string> GetNotUsedSkillsNames(Candidate candidate)
        {
            List<string> returnList = new List<string>();
            try
            {
                using(var db = new DatabaseContainer())
                {
                    returnList = db.Skills.Select(x => x.Name)
                        .Except(candidate.Evaluation
                                            .SkillsEvaluation.Select(x => x.Skill.Name))
                                            .ToList();
                }
            }
            catch(Exception e)
            {
                return returnList;
            }

            return returnList;
        }

        public List<string> GetNotUsedSoftSkillsNames(Candidate candidate)
        {
            List<string> returnList = new List<string>();
            try
            {
                using(var db = new DatabaseContainer())
                {
                    returnList = db.SoftSkills
                        .Select(x => x.Name)
                        .Except(candidate.Evaluation
                                            .SoftSkillsEvaluation
                                            .Select(x => x.SoftSkill.Name))
                                            .ToList();
                }
            }
            catch(Exception e)
            {
                return returnList;
            }

            return returnList;
        }


        public void SaveSkill(SkillsEvaluation skillEvaluation)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {

                    db.SkillsEvaluations.Where(x => x.Id == skillEvaluation.Id).First().Mark = skillEvaluation.Mark;

                    db.SaveChanges();

                }
            }
            catch(Exception e)
            {

            }
        }


        public Candidate GetCandidateBySkillEvaluationId(long skillEvaluationId)
        {
            Candidate candidate = null;
            try
            {
                using(var db = new DatabaseContainer())
                {

                    candidate = this.GetCandidateById(db.SkillsEvaluations
                                                            .Include("Evaluation")
                                                            .Include("Evaluation.Candidate")
                                                            .Where(x => x.Id == skillEvaluationId)
                                                            .First()
                                                            .Evaluation.Candidate.Id);

                }
            }
            catch(Exception e)
            {

            }

            return candidate;
        }


        public long AddSkill(Candidate candidate, SkillsEvaluation skillEvaluation, string skillName)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {

                    skillEvaluation.Skill = db.Skills.Where(x => x.Name == skillName).First();
                    skillEvaluation.Mark = skillEvaluation.Mark;
                    skillEvaluation.EvaluationId = candidate.Evaluation.Id;

                    db.Entry(skillEvaluation).State = System.Data.Entity.EntityState.Added;
                    db.Entry(skillEvaluation.Skill).State = System.Data.Entity.EntityState.Unchanged;
                    db.SaveChanges();                 
                }
            }
            catch(Exception e)
            {

            }

            return skillEvaluation.Id;
        }


        public void SaveSoftSkill(SoftSkillsEvaluation skillEvaluation)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {

                    db.SoftSkillsEvaluations.Where(x => x.Id == skillEvaluation.Id).First().Mark = skillEvaluation.Mark;

                    db.SaveChanges();

                }
            }
            catch(Exception e)
            {

            }
        }

        public long AddSoftSkill(Candidate candidate, SoftSkillsEvaluation skillEvaluation, string skillName)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {

                    skillEvaluation.SoftSkill = db.SoftSkills.Where(x => x.Name == skillName).First();
                    skillEvaluation.Mark = skillEvaluation.Mark;
                    skillEvaluation.EvaluationId = candidate.Evaluation.Id;

                    db.Entry(skillEvaluation).State = System.Data.Entity.EntityState.Added;
                    db.Entry(skillEvaluation.SoftSkill).State = System.Data.Entity.EntityState.Unchanged;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }

            return skillEvaluation.Id;
        }

        public Candidate GetCandidateBySoftSkillEvaluationId(long softSkillEvaluationId)
        {
            Candidate candidate = null;
            try
            {
                using(var db = new DatabaseContainer())
                {

                    candidate = this.GetCandidateById(db.SoftSkillsEvaluations
                                                            .Include("Evaluation")
                                                            .Include("Evaluation.Candidate")
                                                            .Where(x => x.Id == softSkillEvaluationId)
                                                            .First().Evaluation
                                                            .Candidate.Id);

                }
            }
            catch(Exception e)
            {

            }

            return candidate;
        }
    }
}
