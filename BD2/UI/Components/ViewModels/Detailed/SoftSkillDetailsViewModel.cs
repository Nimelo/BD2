using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.DictionaryServiceReference;

namespace UI.Components.ViewModels.Detailed
{
    public class SoftSkillDetailsViewModel : AbstractDetailsViewModel
    {
        #region Model
        private SoftSkill softSkill;

        public SoftSkill SoftSkill
        {
            get { return softSkill; }
            set
            {
                softSkill = value;
                this.Notify("SoftSkill");
            }
        }
        #endregion

        public SoftSkillDetailsViewModel()
        {
            this.SoftSkill = new SoftSkill();
        }


        public override void Refresh()
        {
            
        }
    }
}
