using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.CandidatesServiceReference;


namespace UI.Components.ViewModels.Detailed
{
    public class SoftSkillEvaluationDetailsViewModel : AbstractDetailsViewModel
    {
        #region Model
        private SoftSkillsEvaluation softSkillsEvaluation;

        public SoftSkillsEvaluation SoftSkillsEvaluation
        {
            get { return softSkillsEvaluation; }
            set
            {
                softSkillsEvaluation = value;
                this.Notify("SoftSkillsEvaluation");
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
        public SoftSkillEvaluationDetailsViewModel()
        {
            this.SoftSkillsEvaluation = new SoftSkillsEvaluation();
            this.SoftSkillsEvaluation.SoftSkill = new SoftSkill();
            this.Names = null;
        }

        public void InitializeSkillsNames()
        {
            using(var service = new CandidatesServiceReference.CandidatesServiceClient())
            {               
                this.Names = service.GetNotUsedSoftSkillsNames(this.Candidate);
            }
        }

        public override void Refresh()
        {
           
        }
    }
}
