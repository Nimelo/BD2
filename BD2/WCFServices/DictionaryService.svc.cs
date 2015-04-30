using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DictionaryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DictionaryService.svc or DictionaryService.svc.cs at the Solution Explorer and start debugging.
    public class DictionaryService : IDictionaryService
    {

        public int GetAmountOfSkills()
        {
            int amount = 0;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    amount = db.Skills.Count();
                }
            }
            catch(Exception e)
            {

            }

            return amount;
        }

        public int GetAmountOfSoftSkills()
        {
            int amount = 0;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    amount = db.SoftSkills.Count();
                }
            }
            catch(Exception e)
            {

            }

            return amount;
        }

        public int SaveSkill(Database.Skill skill)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {

                    db.Entry(skill).State = skill.Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }

            return skill.Id;
        }

        public int SaveSoftSkill(Database.SoftSkill softSkill)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    db.Entry(softSkill).State = softSkill.Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }

            return softSkill.Id;
        }

        public void DeleteSkill(Database.Skill skill)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    if(db.SkillsEvaluations.Where(x => x.SkillId == skill.Id).Count() == 0)
                    {
                        db.Entry(skill).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
            }
            catch(Exception e)
            {

            }
        }

        public void DeleteSoftSkill(Database.SoftSkill skill)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    if(db.SoftSkillsEvaluations.Where(x => x.SoftSkillId == skill.Id).Count() == 0)
                    {
                        db.Entry(skill).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                }

            }
            catch(Exception e)
            {

            }
        }

        public void Ping()
        {
            throw new NotImplementedException();
        }

        public List<Skill> GetSkillsByPage(int pageNumber)
        {
            List<Skill> returnList = new List<Skill>();
            if(pageNumber <= 0)
                return returnList;

            if(( pageNumber - 1 ) * 10 > this.GetAmountOfSkills())
                return returnList;

            try
            {
                using(var db = new DatabaseContainer())
                {
                    var tmp = db.Skills.OrderBy(x => x.Id).Skip(( pageNumber - 1 ) * 10);

                    returnList = tmp.Take(tmp.Count() >= 10 ? 10 : tmp.Count()).ToList();
                }
            }
            catch(Exception e)
            {

            }

            return returnList;
        }

        public List<SoftSkill> GetSoftSkillsByPage(int pageNumber)
        {
            List<SoftSkill> returnList = new List<SoftSkill>();
            if(pageNumber <= 0)
                return returnList;

            if(( pageNumber - 1 ) * 10 > this.GetAmountOfSoftSkills())
                return returnList;

            try
            {
                using(var db = new DatabaseContainer())
                {
                    var tmp = db.SoftSkills.OrderBy(x => x.Id).Skip(( pageNumber - 1 ) * 10);

                    returnList = tmp.Take(tmp.Count() >= 10 ? 10 : tmp.Count()).ToList();
                }
            }
            catch(Exception e)
            {

            }

            return returnList;
        }

        public Skill GetSkillById(long id)
        {
            Skill skill = null;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    skill = db.Skills.Where(x => x.Id == id).First();
                }
            }
            catch(Exception e)
            {

            }

            return skill;
        }

        public SoftSkill GetSoftSkillById(long id)
        {
            SoftSkill skill = null;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    skill = db.SoftSkills.Where(x => x.Id == id).First();
                }
            }
            catch(Exception e)
            {

            }

            return skill;
        }


        public int SaveStage(Stage stage)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    if(stage.Id == 0)
                    {
                        stage.Priority = db.Stage.Count() == 0 ? 1 : db.Stage.Max(x => x.Priority) + 1;
                    }

                    db.Entry(stage).State = stage.Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }

            return stage.Id;
        }

        public void DeleteStage(Stage stage)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    db.Entry(db.Stage.Where(x => x.Id == stage.Id).First()).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }
        }

        public int GetAmountOfStages()
        {
            int amount = 0;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    amount = db.Stage.Count();
                }
            }
            catch(Exception e)
            {

            }
            return amount;
        }

        public List<Stage> GetStagesByPage(int pageNumber)
        {
            List<Stage> returnList = new List<Stage>();
            if(pageNumber <= 0)
                return returnList;

            if(( pageNumber - 1 ) * 10 > this.GetAmountOfSoftSkills())
                return returnList;

            try
            {
                using(var db = new DatabaseContainer())
                {
                    var tmp = db.Stage.OrderBy(x => x.Id).Skip(( pageNumber - 1 ) * 10);

                    returnList = tmp.Take(tmp.Count() >= 10 ? 10 : tmp.Count()).ToList();
                }
            }
            catch(Exception e)
            {

            }

            return returnList;
        }


        public Stage GetStageById(long id)
        {
            Stage stage = null;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    stage = db.Stage.Where(x => x.Id == id).First();
                }
            }
            catch(Exception e)
            {

                throw;
            }

            return stage;
        }


        public List<string> GetStagesNames()
        {
            List<string> returnList = new List<string>();
            try
            {
                using(var db = new DatabaseContainer())
                {
                    returnList = db.Stage.ToList().Select(x => x.Name).ToList();
                }
            }
            catch(Exception e)
            {
                return returnList;
            }

            return returnList;
        }

        public int GetStagePriorityByStageName(string name)
        {
            int returnValue = -1;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    returnValue = db.Stage.Where(x => x.Name == name).First().Priority;
                }
            }
            catch(Exception e)
            {
                return returnValue;
            }

            return returnValue;
        }
    }
}
