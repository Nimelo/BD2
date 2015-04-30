
namespace Database
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract(IsReference = true)]
    [KnownType(typeof(SkillsEvaluation))]
    [KnownType(typeof(SoftSkillsEvaluation))]
    [KnownType(typeof(Candidate))]

    public  class Evaluation
    {
        public Evaluation()
        {
            this.SkillsEvaluation = new HashSet<SkillsEvaluation>();
            this.SoftSkillsEvaluation = new HashSet<SoftSkillsEvaluation>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public bool IsEvaluated { get; set; }

        [DataMember]
        public virtual ICollection<SkillsEvaluation> SkillsEvaluation { get; set; }
        [DataMember]
        public virtual ICollection<SoftSkillsEvaluation> SoftSkillsEvaluation { get; set; }
        [DataMember]
        public virtual Candidate Candidate { get; set; }
    }
}
