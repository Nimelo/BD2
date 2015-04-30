namespace Database
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract(IsReference = true)]
    [KnownType(typeof(Document))]
    [KnownType(typeof(Evaluation))]
    [KnownType(typeof(RecruitmentStage))]
    [KnownType(typeof(Decision))]
    [KnownType(typeof(Person))]

    public class Candidate
    {
        public Candidate()
        {
            this.Document = new HashSet<Document>();
            this.RecruitmentStage = new HashSet<RecruitmentStage>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public virtual ICollection<Document> Document { get; set; }
        [DataMember]
        public virtual Evaluation Evaluation { get; set; }
        [DataMember]
        public virtual ICollection<RecruitmentStage> RecruitmentStage { get; set; }
        [DataMember]
        public virtual Decision Decision { get; set; }
        [DataMember]
        public virtual Person Person { get; set; }
    }
}