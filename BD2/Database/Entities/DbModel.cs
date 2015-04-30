using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Database
{
   

    public class DatabaseContainer : DbContext
    {
        public DatabaseContainer()
            : this(false) { }

        public DatabaseContainer(bool proxyCreationEnabled)
            : base("name=DatabaseContainer")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }
      

        public ObjectContext ObjectContext
        {
            get { return ( (IObjectContextAdapter)this ).ObjectContext; }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasRequired(x => x.User).WithRequiredDependent().WillCascadeOnDelete(true);
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<Decision> Decisions { get; set; }
        public virtual DbSet<RecruitmentStage> RecruitmentStages { get; set; }
        public virtual DbSet<SoftSkill> SoftSkills { get; set; }
        public virtual DbSet<SoftSkillsEvaluation> SoftSkillsEvaluations { get; set; }
        public virtual DbSet<SkillsEvaluation> SkillsEvaluations { get; set; }
        public virtual DbSet<Stage> Stage { get; set; }
    }
}
