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
                        .Where(x => x.Id == id).Single();

                    person.User.Password = string.Empty;
                }
            }
            catch(Exception e)
            {

            }

            return person;
        }

        public Person GerPersonByLogin(string login)
        {
            Person person = null;
            try
            {
                using(var db = new DatabaseContainer())
                {
                    person = db.Persons.Include("User")
                        .Include("Candidate")
                        .Where(x => x.User.Login == login).Single();

                    person.User.Password = string.Empty;
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



        public int Save(Person person)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    System.Data.Entity.EntityState state = person.Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;

                    if(state == System.Data.Entity.EntityState.Modified)
                    {
                        if(this.GetPersonById(person.Id).User.Login != person.User.Login)
                            if(db.Users.Where(x => x.Login == person.User.Login).Count() != 0)
                                throw new Exception();
                    }
                    else
                    {
                        if(db.Users.Where(x => x.Login == person.User.Login).Count() != 0)
                            throw new Exception();
                    }

                    db.Entry(person).State = state;
                    db.Entry(person.User).State = state;

                    if(state == System.Data.Entity.EntityState.Added)
                    {
                        person.User.Password = Common.Encryption.Encrypt(person.User.Password);
                    }
                    else
                    {
                        if(person.User.Password == string.Empty)
                        {
                            person.User.Password = db.Users.Where(x => x.Id == person.User.Id).First().Password;
                        }
                        else
                        {
                            person.User.Password = Common.Encryption.Encrypt(person.User.Password);
                        }
                    }

                    if((UserRolesEnum)person.User.Role == UserRolesEnum.Candidate)
                    {
                        if(person.Candidate == null)
                        {
                            db.Entry(new Candidate()
                                {
                                    Person = person,
                                    Evaluation = new Evaluation(),
                                    Decision = new Decision()
                                    {
                                        Type = (byte)DecisionTypesEnum.DuringEvaluation
                                    }

                                }).State = System.Data.Entity.EntityState.Added;
                        }
                        else
                        {
                            db.Entry(person.Candidate).State = state;
                        }
                    }
                    else
                    {
                        if(person.Candidate != null)
                        {
                            Candidate c = db.Candidates
                                            .Include("Evaluation")
                                            .Include("Decision").Where(x => x.Id == person.Candidate.Id).First();
                         
                            db.Entry(c.Evaluation).State = System.Data.Entity.EntityState.Deleted;

                            db.Entry(c.Decision).State = System.Data.Entity.EntityState.Deleted;

                            db.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }

            return person.Id;
        }

        public int SaveC(Person person)
        {
            try
            {
                person.User.Role = (byte)UserRolesEnum.Candidate;
                using(var db = new DatabaseContainer())
                {
                    System.Data.Entity.EntityState state = person.Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                    Person p = null;
                    if(state == System.Data.Entity.EntityState.Modified)
                    {
                        if(this.GetPersonById(person.Id).User.Login != person.User.Login)
                            if(db.Users.Where(x => x.Login == person.User.Login).Count() != 0)
                                throw new Exception();

                        p = db.Persons.Include("User").Where(x => x.Id == person.Id).Single();
                        p.Mail = person.Mail;
                        p.Name = person.Name;
                        p.Pesel = person.Pesel;
                        p.Phone = person.Phone;
                        p.SurName = person.SurName;
                        p.User.Login = person.Pesel;
                    }

                    if(person.Candidate == null)
                    {
                        db.Entry(new Candidate()
                        {
                            Person = person,
                            Evaluation = new Evaluation(),
                            Decision = new Decision()
                            {
                                Type = (byte)DecisionTypesEnum.DuringEvaluation
                            }

                        }).State = System.Data.Entity.EntityState.Added;
                    }

                    if(person.User.Password == string.Empty || person.User.Password == null)
                    {
                        if(state == System.Data.Entity.EntityState.Modified)
                         p.User.Password = db.Users.Where(x => x.Id == person.User.Id).First().Password;

                         person.User.Password = db.Users.Where(x => x.Id == person.User.Id).First().Password;
                    }
                    else
                    {
                        if(state == System.Data.Entity.EntityState.Modified)
                        p.User.Password = Common.Encryption.Encrypt(person.User.Password);
                        person.User.Password = Common.Encryption.Encrypt(person.User.Password);
                    }

                     if(state == System.Data.Entity.EntityState.Added)
                     {
                         db.Entry(person).State = System.Data.Entity.EntityState.Added;
                         db.Entry(person.User).State = System.Data.Entity.EntityState.Added; 
                     }
                     else
                         db.Entry(p).State = System.Data.Entity.EntityState.Modified;            

                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                return 0;
            }

            return person.Id;
        }
        public void Delete(long personId)
        {
            try
            {
                using(var db = new DatabaseContainer())
                {
                    Person p = db.Persons
                        .Include("User")
                        .Include("Candidate")
                        .Include("Candidate.Evaluation")
                        .Include("Candidate.Decision")
                        .Where(x => x.Id == personId).First();

                    db.Entry(p.Candidate.Evaluation).State = System.Data.Entity.EntityState.Deleted;
                    db.Entry(p.Candidate.Decision).State = System.Data.Entity.EntityState.Deleted;
                    db.Entry(p.Candidate).State = System.Data.Entity.EntityState.Deleted;
                    db.Entry(p.User).State = System.Data.Entity.EntityState.Deleted;
                    db.Entry(p).State = System.Data.Entity.EntityState.Deleted;

                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}
