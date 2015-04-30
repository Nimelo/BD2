
namespace Database
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract(IsReference = true)]
    [KnownType(typeof(User))]
    [KnownType(typeof(Candidate))]

    public  class Person
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string SurName { get; set; }
        [DataMember]
        public string Mail { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Pesel { get; set; }

        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual Candidate Candidate { get; set; }
    }
}
