
namespace Database
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract(IsReference = true)]
    [KnownType(typeof(Evaluation))]
    [KnownType(typeof(Skill))]

    public  class SkillsEvaluation
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public byte Mark { get; set; }
        [DataMember]
        public int EvaluationId { get; set; }
        [DataMember]
        public int SkillId { get; set; }

        [DataMember]
        public virtual Evaluation Evaluation { get; set; }
        [DataMember]
        public virtual Skill Skill { get; set; }
    }
}
