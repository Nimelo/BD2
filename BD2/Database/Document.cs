//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    
    [DataContract(IsReference = true)]
    [KnownType(typeof(Candidate))]
    
    public partial class Document
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
