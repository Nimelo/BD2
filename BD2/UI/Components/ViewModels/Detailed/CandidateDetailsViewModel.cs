using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.CandidatesServiceReference;
using UI.Classes;
using UI.Classes.ListsDataDevelopers;
using UI.Classes.Templates;
using UI.Components.Detailed;
using UI.Components.Enums;
using UI.Managers;

namespace UI.Components.ViewModels.Detailed
{
    public class CandidateDetailsViewModel : AbstractDetailsViewModel
    {

        #region Ctors

        public CandidateDetailsViewModel()
        {
            this.FakeDocumentsListDataDeveloper = new FakeDocumentsListDataDeveloper();
            this.FakeSkillsListDataDeveloper = new FakeSkillsListDataDeveloper();
            this.FakeSoftSkillsListDataDeveloper = new FakeSoftSkillsListDataDeveloper();


            this.DocumentsDataContext = new BasicListViewModel<DocumentListViewTemplate>(this.FakeDocumentsListDataDeveloper, this.SkillEvaluationContextClick);
            this.SoftSkillsDataContext = new BasicListViewModel<SoftSkillEvaluationListViewTemplate>(this.FakeSoftSkillsListDataDeveloper, this.SoftSkillEvaluationContextClick);
            this.SkillsDataContext = new BasicListViewModel<SkillEvaluationListViewTemplate>(this.FakeSkillsListDataDeveloper, this.SkillEvaluationContextClick);

        }
        #endregion

        #region Model
        private Candidate candidate;

        public Candidate Candidate
        {
            get { return candidate; }
            set
            {
                candidate = value;
                this.Notify("Candidate");
                this.Refresh();
            }
        }
        #endregion

        #region Properties and Fields


        #region Documents

        public FakeDocumentsListDataDeveloper FakeDocumentsListDataDeveloper { get; set; }
        public BasicListViewModel<DocumentListViewTemplate> DocumentsDataContext { get; set; }

        private bool isDocumentsTabSelected;

        public bool IsDocumentsTabSelected
        {
            get { return isDocumentsTabSelected; }
            set
            {
                isDocumentsTabSelected = value;
                this.Notify("IsDocumentsTabSelected");
            }
        }

        #endregion

        #region Evaluation

        private bool isEvaluationTabSelected;

        public bool IsEvaluationTabSelected
        {
            get { return isEvaluationTabSelected; }
            set
            {
                isEvaluationTabSelected = value;
                this.Notify("IsEvaluationTabSelected");
            }
        }

        #region SoftSkills

        public FakeSoftSkillsListDataDeveloper FakeSoftSkillsListDataDeveloper { get; set; }
        public BasicListViewModel<SoftSkillEvaluationListViewTemplate> SoftSkillsDataContext { get; set; }

        #endregion

        #region Skills
        public FakeSkillsListDataDeveloper FakeSkillsListDataDeveloper { get; set; }
        public BasicListViewModel<SkillEvaluationListViewTemplate> SkillsDataContext { get; set; }

        #endregion

        #endregion

        #endregion

        #region Private methods

        private void SkillEvaluationContextClick(object obj)
        {
            if(obj != null)
            {
                long id = (long)obj.GetType().GetField("Id").GetValue(obj);
                WindowManager.DisplayEditableWindow(new SkillEvaluation(this.Candidate), DetailsWindowModes.Readonly, id);
            }
            else
            {
                WindowManager.DisplayEditableWindow(new SkillEvaluation(this.Candidate), DetailsWindowModes.New);
            }
        }

        private void SoftSkillEvaluationContextClick(object obj)
        {
            if(obj != null)
            {
                long id = (long)obj.GetType().GetField("Id").GetValue(obj);
                WindowManager.DisplayEditableWindow(new SoftSkillEvaluation(this.Candidate), DetailsWindowModes.Readonly, id);
            }
            else
            {
                WindowManager.DisplayEditableWindow(new SoftSkillEvaluation(this.Candidate), DetailsWindowModes.New);
            }
        }
        #endregion

        public override void Refresh()
        {
            this.FakeDocumentsListDataDeveloper.InsertData(this.Candidate.Document);
            this.FakeSkillsListDataDeveloper.InsertData(this.Candidate.Evaluation.SkillsEvaluation);
            this.FakeSoftSkillsListDataDeveloper.InsertData(this.Candidate.Evaluation.SoftSkillsEvaluation);

            this.ChangeContentDataBackgroundOperation(() =>
                {                 
                    this.DocumentsDataContext.Refresh();
                    this.SoftSkillsDataContext.Refresh();
                    this.SkillsDataContext.Refresh();
                });
           
        }
        
        private void ChangeContentDataBackgroundOperation(Action work)
        {
            TaskManager.DoBackgroundWork(() =>
            {
                work.Invoke();
            }, () =>
            {
            }
            );
        }
    }
}
