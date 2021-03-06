﻿using Common.Enums;
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
        public int GetAmountOfRecords(int stage)
        {
            int amount = -1;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    amount = db.RecruitmentStages.Include("Stage")
                         .Where(x => x.Stage.Priority == stage).Count();
                }
            }
            catch(Exception e)
            {

            }

            return amount;
        }

        public int GetAmountOfRecordsDecisionType(byte decisionType)
        {
            int amount = -1;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    amount = db.Decisions
                         .Where(x => x.Type == decisionType).Count();
                }
            }
            catch(Exception e)
            {

            }

            return amount;
        }
        public int GetAmountOfAllRecords()
        {
            int amount = -1;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    amount = db.Candidates.Count();
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
                        .Include("RecruitmentStage.Stage")
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

        public List<Candidate> GetCandidatesByPage(int pageNumber, int stage)
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

                    tmp = (
                    ( db.RecruitmentStages
                    .Include("Candidate")
                    .Include("Stage")
                    .Where(x => x.Stage.Priority == stage)
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

                    tmp = tmp.Take(tmp.Count() >= 10 ? 10 : tmp.Count());
                    return tmp.ToList();

                }
            }
            catch(Exception e)
            {
                return returnList;
            }

        }

        public List<Candidate> GetCandidatesByPageDecisionType(int pageNumber, byte decisionType)
        {
            var returnList = new List<Candidate>();
            if(pageNumber <= 0)
                return returnList;

            try
            {
                using(var db = new DatabaseContainer())
                {

                    if(( pageNumber - 1 ) * 10 > this.GetAmountOfRecords(decisionType))
                        return returnList;

                    IQueryable<Candidate> tmp = null;

                    tmp = (
                    ( db.Decisions.Include("Candidate").Where(x => x.Type == decisionType)                 
                    .Select(y => y.Candidate) as DbQuery<Candidate> ) 
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
                    .Skip(( pageNumber - 1 ) * 10));

                    tmp = tmp.Take(tmp.Count() >= 10 ? 10 : tmp.Count());
                    return tmp.ToList();

                }
            }
            catch(Exception e)
            {
                return returnList;
            }

        }
        public int Save(Candidate candidate)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    db.Candidates.Attach(candidate);
                    db.Entry(candidate).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(candidate.Decision).State = System.Data.Entity.EntityState.Modified;

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

                    db.SaveChanges();

                }
            }
            catch(Exception e)
            {

            }

            return candidate.Id;
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


        public List<Candidate> GetAllCandidatesByPage(int pageNumber)
        {
            var returnList = new List<Candidate>();
            if(pageNumber <= 0)
                return returnList;

            try
            {
                using(var db = new DatabaseContainer())
                {

                    if(( pageNumber - 1 ) * 10 > this.GetAmountOfAllRecords())
                        return returnList;

                    IQueryable<Candidate> tmp = null;

                    tmp =
                     db.Candidates
                    .Include("Person")
                    .Include("Document")
                    .Include("Decision")
                    .Include("RecruitmentStage")
                    .Include("RecruitmentStage.Stage")
                    .Include("Evaluation")
                    .Include("Evaluation.SkillsEvaluation")
                    .Include("Evaluation.SkillsEvaluation.Skill")
                    .Include("Evaluation.SoftSkillsEvaluation")
                    .Include("Evaluation.SoftSkillsEvaluation.SoftSkill")
                    .OrderBy(x => x.Id)
                    .Skip(( pageNumber - 1 ) * 10);

                    tmp = tmp.Take(tmp.Count() >= 10 ? 10 : tmp.Count());
                    return tmp.ToList();

                }
            }
            catch(Exception e)
            {
                return returnList;
            }
        }


        public void Delete(long candidateId)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    db.Entry(db.Candidates.Where(x => x.Id == candidateId).First()).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

                throw;
            }
        }

        public long SaveRecriutmentStage(RecruitmentStage rs)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {

                    rs.Stage = db.Stage.Where(x => x.Name == rs.Stage.Name).First();
                    db.Entry(rs).State = rs.Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }

            return rs.Id;
        }

        public void DeleteRecriutmentStage(RecruitmentStage rs)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    db.Entry(db.RecruitmentStages.Where(x => x.Id == rs.Id).First()).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

                throw;
            }
        }

        public List<string> GetNotUsedStageNames(Candidate candidate)
        {
            List<string> returnList = new List<string>();
            try
            {
                using(var db = new DatabaseContainer())
                {
                    returnList = db.Stage.Select(x => x.Name)
                        .Except(candidate.RecruitmentStage
                                            .Select(x => x.Stage.Name))
                                            .ToList();
                }
            }
            catch(Exception e)
            {
                return returnList;
            }

            return returnList;
        }


        public Candidate GetCandidateByRecruitmentStageId(long recruitmentStageId)
        {
            Candidate candidate = null;
            try
            {
                using(var db = new DatabaseContainer())
                {

                    candidate = this.GetCandidateById(db.RecruitmentStages
                                                        .Where(x => x.Id == recruitmentStageId).First().CandidateId);

                }
            }
            catch(Exception e)
            {

            }

            return candidate;
        }

        public Candidate GetCandidateByLogin(string login)
        {
            Candidate candidate = null;
            try
            {
                using(var db = new DatabaseContainer())
                {

                  int id =   db.Users
                    .Include("Person")
                    .Where(u => u.Login == login)
                    .First()
                    .Person.Id; 
                    candidate = this.GetCandidateById(
                        db.Candidates
                        .Include("Person")
                        .Where(x=> x.Person.Id == id)
                                                    
                        .First().Id);

                }
            }
            catch(Exception e)
            {

            }

            return candidate;
        }

        public int GetCandidateIdByLogin(string login)
        {
            int id = 0;
            try
            {
                using(var db = new DatabaseContainer())
                {
                  id = db.Users.Include("Person").Include("Person.Candidate").Where(x => x.Login == login).First().Person.Candidate.Id;
                }
            }
            catch(Exception e)
            {
                
            }

            return id;  
        }

    }
}
