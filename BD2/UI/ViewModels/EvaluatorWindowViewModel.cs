using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Classes;
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
        public BasicListViewModel<CandidateListViewTemplate> AllCandidatesViewModel { get; set; }

        public BasicListViewModel<CandidateListViewTemplate> StageZeroCandidatesViewModel { get; set; }

        public BasicListViewModel<CandidateListViewTemplate> StageOneCandidatesViewModel { get; set; }

        public BasicListViewModel<CandidateListViewTemplate> StageTwoCandidatesViewModel { get; set; }

        #endregion

        #region Ctors
        public EvaluatorWindowViewModel()
        {
            this.AllCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(new CandidateListDataDeveloper(), new CandidateDetails());
            this.StageZeroCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(new CandidateListDataDeveloper(CandidatesStagesEnum.Stage0), new CandidateDetails());
            this.StageOneCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(new CandidateListDataDeveloper(CandidatesStagesEnum.Stage1), new CandidateDetails());
            this.StageTwoCandidatesViewModel = new BasicListViewModel<CandidateListViewTemplate>(new CandidateListDataDeveloper(CandidatesStagesEnum.Stage2), new CandidateDetails());
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
                        WindowManager.DisplayListWindow<SkillListViewTemplate>(new BasicListViewModel<SkillListViewTemplate>(new SkillsListDataDeveloper(), this.SkillEvaluationContextClick));
                              
                        break;
                    case DictionaryTypesEnum.SoftSkills:
                        break;
                    default:
                        break;
                }
            }
           // WindowManager.DisplayEditableWindow(new PersonUserDetails(), Components.Enums.DetailsWindowModes.New);
        }
        private void SkillEvaluationContextClick(object obj)
        {
            if(obj != null)
            {
                long id = (long)obj.GetType().GetField("Id").GetValue(obj);
                WindowManager.DisplayEditableWindow(new SkillDetails(), DetailsWindowModes.Readonly, id);
            }
            else
            {
                WindowManager.DisplayEditableWindow(new SkillDetails(), DetailsWindowModes.New);
            }
        }
        #endregion
    }
}
