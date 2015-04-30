
namespace Database
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract(IsReference = true)]
    [KnownType(typeof(Candidate))]

    public  class Document
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CandidateId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Extension { get; set; }
        [DataMember]
        public byte Type { get; set; }
        [DataMember]
        public byte[] File { get; set; }

        [DataMember]
        public virtual Candidate Candidate { get; set; }
    }
}
