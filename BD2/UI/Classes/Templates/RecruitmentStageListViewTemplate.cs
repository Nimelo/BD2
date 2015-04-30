using Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.Templates
{
    public class RecruitmentStageListViewTemplate
    {

        public long Id;
        public string Name { get; set; }
        public byte Mark { get; set; }
       
        public RecruitmentStageListViewTemplate(int id)
        {
            this.Id = id;
        }
        public RecruitmentStageListViewTemplate()
        {

        }
    }
}
