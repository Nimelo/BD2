
namespace Database
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;

    [DataContract(IsReference = true)]

    public  class Skill
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
