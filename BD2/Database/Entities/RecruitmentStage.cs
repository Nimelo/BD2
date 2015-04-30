
namespace Database
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract(IsReference = true)]
    [KnownType(typeof(Candidate))]
    [KnownType(typeof(Stage))]

    public  class RecruitmentStage
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public byte Mark { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public int CandidateId { get; set; }
        [DataMember]
        public bool IsCurrent { get; set; }
        [DataMember]
        public int StageId { get; set; }

        [DataMember]
        public virtual Candidate Candidate { get; set; }
        [DataMember]
        public virtual Stage Stage { get; set; }
    }
}
