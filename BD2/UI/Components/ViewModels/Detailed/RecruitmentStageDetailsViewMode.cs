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
    public class RecruitmentStageDetailsViewModel : AbstractDetailsViewModel
    {
        #region Model
        private RecruitmentStage recruitmentStage;

        public RecruitmentStage RecruitmentStage
        {
            get { return recruitmentStage; }
            set
            {
                recruitmentStage = value;
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
        public RecruitmentStageDetailsViewModel()
        {
            this.RecruitmentStage = new RecruitmentStage();
            this.RecruitmentStage.Stage = new Stage();

            this.Names = null;

        }

        public void InitializeSkillsNames()
        {
            using(var service = new CandidatesServiceReference.CandidatesServiceClient())
            {
                this.Names = service.GetNotUsedStageNames(this.Candidate);
            }
        }


        public override void Refresh()
        {
            
        }
    }
}
