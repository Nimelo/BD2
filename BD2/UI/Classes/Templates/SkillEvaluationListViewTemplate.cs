using Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.Templates
{
    public class SkillEvaluationListViewTemplate
    {

        public long Id;

        public string Name { get; set; }
        public byte Mark { get; set; }
        
        public SkillEvaluationListViewTemplate(long id)
        {
            this.Id = id;
        }
        public SkillEvaluationListViewTemplate()
        {

        }
    }
}
