using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Classes.Templates
{
    public class StageListViewTemplate
    {
        public long Id;

        public string Name { get; set; }

        public int Priority
        {
            get;
            set;
        }
        public StageListViewTemplate(long id)
        {
            this.Id = id;
        }

        public StageListViewTemplate()
        {

        }
    }
}
