
namespace Database
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract(IsReference = true)]
    [KnownType(typeof(Candidate))]

    public  class Decision
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public byte Type { get; set; }
        [DataMember]
        public string Answer { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public int CandidateId { get; set; }

        [DataMember]
        public virtual Candidate Candidate { get; set; }
    }
}
