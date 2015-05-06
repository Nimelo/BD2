using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Classes;
using UI.Classes.Configurations;
using UI.Classes.ListsDataDevelopers;
using UI.Classes.Templates;
using UI.Components.Detailed;
using UI.Components.Enums;
using UI.Components.ViewModels;
using UI.Managers;

namespace UI.ViewModels
{
    public class EvaluatorWindowViewModel : INotifyPropertyChanged
    {
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properites and Fields
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

        private string priority;

        public string Priority
        {
            get
            {
                int value = 0;
                using(var service = new DictionaryServiceReference.DictionaryServiceClient())
                {
                        value = service.GetStagePriorityByStageName(this.priority);
                }
                this.StageCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(this.DD, new CandidateDetails(),
                           new BasicListConfiguration(false, true), new BasicDetailsControlConfiguration(true, false, true, false));
                this.DD.Parameter = (object)value;

                return this.priority;
            }
            set
            {
                priority = value;
                this.Notify("Priority");
            }
        }


        public BasicListViewModel<CandidateListViewTemplate> AllCandidatesViewModel { get; set; }

        public BasicListViewModel<CandidateListViewTemplate> StageCandidatesViewModel { get; set; }

        public BasicListViewModel<CandidateListViewTemplate> ConfirmedCandidatesViewModel { get; set; }

        public BasicListViewModel<CandidateListViewTemplate> RejectedCandidatesViewModel { get; set; }

        public BasicListViewModel<CandidateListViewTemplate> DECandidatesViewModel { get; set; }

        public BasicListViewModel<CandidateListViewTemplate> ApprovedCandidatesViewModel { get; set; }


        #endregion

        #region Ctors
        public EvaluatorWindowViewModel()
        {
            this.InitializeStagesNames();

            this.DD = new CandidateListDataDeveloper();
            this.AllCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(new CandidateListDataDeveloper(), new CandidateDetails(),
                            new BasicListConfiguration(false, true), new BasicDetailsControlConfiguration(true, false, true, false));
            this.StageCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(this.DD, new CandidateDetails(),
                            new BasicListConfiguration(false, true), new BasicDetailsControlConfiguration(true, false, true, false));

            this.ConfirmedCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(new CandidateListDataDeveloper((byte)Common.Enums.DecisionTypesEnum.Confirmed), new CandidateDetails(),
                           new BasicListConfiguration(false, true), new BasicDetailsControlConfiguration(true, false, true, false));

            this.RejectedCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(new CandidateListDataDeveloper((byte)Common.Enums.DecisionTypesEnum.Rejected), new CandidateDetails(),
                           new BasicListConfiguration(false, true), new BasicDetailsControlConfiguration(true, false, true, false));

            this.DECandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(new CandidateListDataDeveloper((byte)Common.Enums.DecisionTypesEnum.DuringEvaluation), new CandidateDetails(),
                           new BasicListConfiguration(false, true), new BasicDetailsControlConfiguration(true, false, true, false));

            this.ApprovedCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(new CandidateListDataDeveloper((byte)Common.Enums.DecisionTypesEnum.Approved), new CandidateDetails(),
                           new BasicListConfiguration(false, true), new BasicDetailsControlConfiguration(true, false, true, false));

        }


        private CandidateListDataDeveloper dd;

        public CandidateListDataDeveloper DD
        {
            get { return dd; }
            set
            {
                dd = value;
                this.Notify("DD");
            }
        }


        #endregion

        #region Commands
        ICommand _menuCommand;
        public ICommand MenuCommand
        {
            get
            {
                return _menuCommand ??
                    ( _menuCommand = new RelayCommand(MenuClick) );
            }
        }

        private void MenuClick(object obj)
        {
            if(obj.GetType() == typeof(DictionaryTypesEnum))
            {
                switch((DictionaryTypesEnum)obj)
                {
                    case DictionaryTypesEnum.Skills:
                        WindowManager.DisplayListWindow<SkillListViewTemplate>(new BasicListViewModel<SkillListViewTemplate>(new SkillsListDataDeveloper(),
                            new BasicListConfiguration(),
                            this.SkillEvaluationContextClick));

                        break;
                    case DictionaryTypesEnum.SoftSkills:
                        WindowManager.DisplayListWindow<SoftSkillListViewTemplate>(new BasicListViewModel<SoftSkillListViewTemplate>(new SoftSkillsListDataDeveloper(),
                            new BasicListConfiguration(), this.SoftSkillEvaluationContextClick));

                        break;

                    case DictionaryTypesEnum.Stages:
                        WindowManager.DisplayListWindow<StageListViewTemplate>(new BasicListViewModel<StageListViewTemplate>(new StageListDataDeveloper(),
                            new BasicListConfiguration(), this.StageContextClick));
                        break;
                    default:
                        break;
                }

                this.InitializeStagesNames();
            }
            // WindowManager.DisplayEditableWindow(new PersonUserDetails(), Components.Enums.DetailsWindowModes.New);
        }

        private void StageContextClick(object obj)
        {
            if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.Readonly)
            {
                if(( (object[])obj )[1] != null)
                {
                    long id = (long)( (object[])obj )[1].GetType().GetField("Id").GetValue(( (object[])obj )[1]);
                    WindowManager.DisplayEditableWindow(new StageDetails(), DetailsWindowModes.Readonly, id);
                }
            }
            else if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.New)
            {
                WindowManager.DisplayEditableWindow(new StageDetails(), DetailsWindowModes.New);
            }
        }

        private void SoftSkillEvaluationContextClick(object obj)
        {
            if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.Readonly)
            {
                if(( (object[])obj )[1] != null)
                {
                    long id = (long)( (object[])obj )[1].GetType().GetField("Id").GetValue(( (object[])obj )[1]);
                    WindowManager.DisplayEditableWindow(new SoftSkillDetails(), DetailsWindowModes.Readonly, id);
                }
            }
            else if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.New)
            {
                WindowManager.DisplayEditableWindow(new SoftSkillDetails(), DetailsWindowModes.New);
            }
        }

        private void SkillEvaluationContextClick(object obj)
        {
            if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.Readonly)
            {
                if(( (object[])obj )[1] != null)
                {
                    long id = (long)( (object[])obj )[1].GetType().GetField("Id").GetValue(( (object[])obj )[1]);
                    WindowManager.DisplayEditableWindow(new SkillDetails(), DetailsWindowModes.Readonly, id);
                }
            }
            else if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.New)
            {
                WindowManager.DisplayEditableWindow(new SkillDetails(), DetailsWindowModes.New);
            }
        }

        public void InitializeStagesNames()
        {
            using(var service = new DictionaryServiceReference.DictionaryServiceClient())
            {
                this.Names = service.GetStagesNames();
            }
        }
        #endregion
    }
}
