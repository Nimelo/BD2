using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.CandidatesServiceReference;
using UI.Classes;

namespace UI.Components.ViewModels.Detailed
{
    public class SkillEvaluationDetailsViewModel : AbstractDetailsViewModel
    {
        #region Model
        private SkillsEvaluation skillsEvaluation;

        public SkillsEvaluation SkillsEvaluation
        {
            get { return skillsEvaluation; }
            set
            {
                skillsEvaluation = value;
                this.Notify("SkillsEvaluation");
            }
        }

        private Candidate candidate;

        public Candidate Candidate
        {
            get { return candidate; }
            set
            {
                candidate = value;
                this.Notify("Candidate");
            }
        }

        #endregion

        private List<string> names;
        public List<string> Names
        {
            get
            {
                return this.names;
            }
            set
            {
                this.names = value;
                this.Notify("Names");
            }
        }
        public SkillEvaluationDetailsViewModel()
        {
            this.SkillsEvaluation = new SkillsEvaluation();
            this.SkillsEvaluation.Skill = new Skill();
            this.Names = null;

        }

        public void InitializeSkillsNames()
        {
            using(var service = new CandidatesServiceReference.CandidatesServiceClient())
            {
                this.Candidate.Document = null;
                this.Names = service.GetNotUsedSkillsNames(this.Candidate);
            }
        }


        public override void Refresh()
        {
            
        }
    }
}
