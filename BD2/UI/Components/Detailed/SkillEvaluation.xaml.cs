using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.CandidatesServiceReference;
using UI.Components.ViewModels.Detailed;
using UI.PersonsServiceReference;

namespace UI.Components.Detailed
{
    /// <summary>
    /// Interaction logic for PersonDetails.xaml
    /// </summary>
    public partial class SkillEvaluation : AbstractDetailsComponent
    {
        public SkillEvaluation()
        {
            InitializeComponent();
            this.ViewModel = new SkillEvaluationDetailsViewModel();
            this.DataContext = this.ViewModel;
        }

        public SkillEvaluation(UI.CandidatesServiceReference.Candidate candidate)
        {
            InitializeComponent();
            this.ViewModel = new SkillEvaluationDetailsViewModel();
            this.ViewModel.Candidate = candidate;
            this.DataContext = this.ViewModel;
        }

        public SkillEvaluationDetailsViewModel ViewModel { get; set; }


        public override void Add()
        {
            using(var service = new CandidatesServiceClient())
            {
                try
                {
                    this.CurrentId = service.AddSkill(this.ViewModel.Candidate, this.ViewModel.SkillsEvaluation, this.ViewModel.SkillsEvaluation.Skill.Name);
                }
                catch(Exception e)
                {

                }

            }
        }

        public override void Save()
        {
            using(var service = new CandidatesServiceClient())
            {
                try
                {
                    service.SaveSkill(this.ViewModel.SkillsEvaluation);
                }
                catch(Exception e)
                {

                }

            }
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override void Load()
        {
            using(var service = new CandidatesServiceClient())
            {
                try
                {                   
                    this.ViewModel.Candidate = service.GetCandidateBySkillEvaluationId(this.CurrentId);

                    this.ViewModel.SkillsEvaluation = this.ViewModel.Candidate.Evaluation.SkillsEvaluation.Where(x => x.Id == this.CurrentId).First();
                }
                catch(Exception e)
                {

                }
            }
        }

        public void LoadCandidate()
        {
            using(var service = new CandidatesServiceClient())
            {
                try
                {
                    this.ViewModel.Candidate = service.GetCandidateBySkillEvaluationId(this.CurrentId);
                }
                catch(Exception e)
                {

                }
            }
        }

        public override void EnableControls(bool enable)
        {
            this.ViewModel.IsEnabled = enable;
        }

        public override void ChangeMode(Enums.DetailsWindowModes mode)
        {
            switch(mode)
            {
                case UI.Components.Enums.DetailsWindowModes.Readonly:
                    this.ViewModel.IsEnabled = false;
                    this.Load();
                    this.ViewModel.Names = new List<string>() { this.ViewModel.SkillsEvaluation.Skill.Name };
                    break;
                case UI.Components.Enums.DetailsWindowModes.Edit:
                    this.ViewModel.IsEnabled = true;
                    this.ViewModel.Names = new List<string>() { this.ViewModel.SkillsEvaluation.Skill.Name };
                    break;
                case UI.Components.Enums.DetailsWindowModes.New:
                    this.ViewModel.IsEnabled = true;
                    this.ViewModel.InitializeSkillsNames();

                    break;
                default:
                    break;
            }
        }
    }
}
