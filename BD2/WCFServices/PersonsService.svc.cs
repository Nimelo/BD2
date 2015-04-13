using Common;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PersonsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PersonsService.svc or PersonsService.svc.cs at the Solution Explorer and start debugging.
    public class PersonsService : IPersonsService
    {


        public Person GetPersonById(long id)
        {
            Person person = null;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    person = db.Persons.Include("User")
                        .Include("Candidate")
                        .Include("Candidate.Evaluation")
                        .Include("Candidate.RecruitmentStage")
                        .Include("Candidate.Evaluation.SoftSkillsEvaluation")
                        .Include("Candidate.Evaluation.SkillsEvaluation")
                        .Where(x => x.Id == id).Single();
                }
            }
            catch(Exception e)
            {

            }

            return person;
        }

        public List<Person> GetPersonByPage(int pageNumber)
        {
            var returnList = new List<Person>();
            if(pageNumber <= 0)
                return returnList;

            try
            {
                using(DatabaseContainer db = new DatabaseContainer())
                {

                    if(( pageNumber - 1 ) * 10 > db.Persons.Count())
                        return returnList;

                    var tmp = db.Persons.Include("User")
                        .Include("Candidate")
                        .Include("Candidate.Evaluation")
                        .Include("Candidate.RecruitmentStage")
                        .Include("Candidate.Evaluation.SoftSkillsEvaluation")
                        .Include("Candidate.Evaluation.SkillsEvaluation")
                        .OrderBy(x => x.Id).Skip(( pageNumber - 1 ) * 10);
                    tmp = tmp.Take(tmp.Count() >= 10 ? 10 : tmp.Count());
                    return tmp.ToList();

                }
            }
            catch(Exception e)
            {
                return returnList;
            }

        }



        public void Ping()
        {
            throw new NotImplementedException();
        }

        public int GetAmountOfRecords()
        {
            int returnValue;

            using(var db = new DatabaseContainer())
            {
                returnValue = db.Persons.Count();
            }

            return returnValue;
        }




        public void Save(Person person)
        {
            using(var db = new DatabaseContainer())
            {
                try
                {
                    db.Persons.Attach(person);
                    db.Entry(person).State = System.Data.Entity.EntityState.Modified;
                    db.Users.Attach(person.User);
                    db.Entry(person.User).State = System.Data.Entity.EntityState.Modified;

                    if((UserRolesEnum)person.User.Role == UserRolesEnum.Candidate)
                    {
                        if(person.Candidate == null)
                        {
                            db.Candidates.Attach(person.Candidate = new Candidate()
                            {
                                RecruitmentStage = new List<RecruitmentStage>()
                                {     
                                    new RecruitmentStage(){
                                    Mark = 0,
                                    Stage = 0,
                                    IsCurrent = true
                                    }
                                },

                                Evaluation = new Evaluation()
                                {
                                    IsEvaluated = false
                                }
                            });

                            db.Entry(person.Candidate).State = System.Data.Entity.EntityState.Added;
                            db.Entry(person.Candidate.Evaluation).State = System.Data.Entity.EntityState.Added;
                            db.Entry(person.Candidate.RecruitmentStage.First()).State = System.Data.Entity.EntityState.Added;
                        }
                    }
                    else
                    {

                        if(person.Candidate != null)
                        {
                            db.RecruitmentStages.RemoveRange(db.RecruitmentStages.Where(x => x.CandidateId == person.Candidate.Id));

                            db.Evaluations.Attach(db.Evaluations.Include("Candidate").Where(x=> x.Candidate.Id == person.Candidate.Id).First());
                            db.Entry(person.Candidate.Evaluation).State = System.Data.Entity.EntityState.Deleted;

                            db.Candidates.Attach(person.Candidate);
                            db.Entry(person.Candidate).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }

                    db.SaveChanges();
                }
                catch(Exception e)
                {

                }
            }
        }

        public void Delete(Person person)
        {
            using(var db = new DatabaseContainer())
            {
                try
                {
                    db.Persons.Attach(person);
                    db.Users.Remove(person.User);
                    db.Persons.Remove(person);

                    if(person.Candidate != null)
                    {
                        db.Candidates.Attach(person.Candidate);
                        db.Evaluations.Remove(person.Candidate.Evaluation);
                        db.RecruitmentStages.RemoveRange(person.Candidate.RecruitmentStage);
                        db.Candidates.Remove(person.Candidate);
                    }

                    

                    db.SaveChanges();
                }
                catch(Exception e)
                {

                }


            }
        }

        public void Modify(Person person)
        {
            this.Save(person);
        }

        public int Add(Person person)
        {
            using(var db = new DatabaseContainer())
            {
                try
                {
                    db.Persons.Attach(person);
                    db.Entry(person).State = System.Data.Entity.EntityState.Added;
                    db.Users.Attach(person.User);
                    db.Entry(person.User).State = System.Data.Entity.EntityState.Added;

                    if((UserRolesEnum)person.User.Role == UserRolesEnum.Candidate)
                    {
                        db.Candidates.Attach(person.Candidate = new Candidate()
                            {
                                RecruitmentStage = new List<RecruitmentStage>()
                                {     
                                    new RecruitmentStage(){
                                    Mark = 0,
                                    Stage = 0,
                                    IsCurrent = true
                                    }
                                },

                                Evaluation = new Evaluation()
                                {
                                    IsEvaluated = false
                                }
                            });
                        db.Entry(person.Candidate).State = System.Data.Entity.EntityState.Added;
                        db.Entry(person.Candidate.Evaluation).State = System.Data.Entity.EntityState.Added;
                        db.Entry(person.Candidate.RecruitmentStage.First()).State = System.Data.Entity.EntityState.Added;
                    }

                    db.SaveChanges();

                }
                catch(Exception e)
                {

                }
            }
            return person.Id;
        }
    }
}
