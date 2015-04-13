using Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.Templates
{
    public class SkillListViewTemplate
    {

        public long Id;

        public string Name { get; set; }
  
        public SkillListViewTemplate(long id)
        {
            this.Id = id;
        }
        public SkillListViewTemplate()
        {

        }
    }
}
