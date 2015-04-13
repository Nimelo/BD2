using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.DictionaryServiceReference;

namespace UI.Components.ViewModels.Detailed
{
    public class SkillDetailsViewModel : AbstractDetailsViewModel
    {
        #region Model
        private Skill skill;

        public Skill Skill
        {
            get { return skill; }
            set
            {
                skill = value;
                this.Notify("Skill");
            }
        }
        #endregion

        public SkillDetailsViewModel()
        {
            this.Skill = new Skill();
        }


        public override void Refresh()
        {
            
        }
    }
}
