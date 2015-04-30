using Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.Templates
{
    public class SoftSkillListViewTemplate
    {

        public long Id;

        public string Name { get; set; }
  
        public SoftSkillListViewTemplate(long id)
        {
            this.Id = id;
        }
        public SoftSkillListViewTemplate()
        {

        }
    }
}
