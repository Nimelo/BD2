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
using UI.Classes.Configurations;
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
            this.FakeSkillsEvaluationListDataDeveloper = new FakeSkillsEvaluationListDataDeveloper();
            this.FakeSoftSkillsEvaluationListDataDeveloper = new FakeSoftSkillsEvaluationListDataDeveloper();
            this.FakeRecruitmentStageListDataDeveloper = new FakeRecruitmentStageListDataDeveloper();

            this.DocumentsDataContext = new BasicListViewModel<DocumentListViewTemplate>(this.FakeDocumentsListDataDeveloper,
                            new BasicListConfiguration(false, true), this.DocumentClick);
            this.SoftSkillsEvaluationDataContext = new BasicListViewModel<SoftSkillEvaluationListViewTemplate>(this.FakeSoftSkillsEvaluationListDataDeveloper,
                            new BasicListConfiguration(), this.SoftSkillEvaluationContextClick);
            this.SkillsEvaluationListDataContext = new BasicListViewModel<SkillEvaluationListViewTemplate>(this.FakeSkillsEvaluationListDataDeveloper,
                            new BasicListConfiguration(), this.SkillEvaluationContextClick);
            this.RecruitmentStageDataContext = new BasicListViewModel<RecruitmentStageListViewTemplate>(this.FakeRecruitmentStageListDataDeveloper,
                            new BasicListConfiguration(), this.StageContextClick);

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

        public FakeSoftSkillsEvaluationListDataDeveloper FakeSoftSkillsEvaluationListDataDeveloper { get; set; }
        public BasicListViewModel<SoftSkillEvaluationListViewTemplate> SoftSkillsEvaluationDataContext { get; set; }

        #endregion

        #region Skills
        public FakeSkillsEvaluationListDataDeveloper FakeSkillsEvaluationListDataDeveloper { get; set; }
        public BasicListViewModel<SkillEvaluationListViewTemplate> SkillsEvaluationListDataContext { get; set; }

        #endregion

        #region RecruitmentStages
        public FakeRecruitmentStageListDataDeveloper FakeRecruitmentStageListDataDeveloper { get; set; }

        public BasicListViewModel<RecruitmentStageListViewTemplate> RecruitmentStageDataContext { get; set; }
        #endregion
        #endregion

        #endregion

        #region Private methods

        private void SkillEvaluationContextClick(object obj)
        {
            if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.Readonly)
            {
                if(( (object[])obj )[1] != null)
                {
                    long id = (long)( (object[])obj )[1].GetType().GetField("Id").GetValue(( (object[])obj )[1]);
                    WindowManager.DisplayEditableWindow(new SkillEvaluation(this.Candidate), DetailsWindowModes.Readonly, id);
                }
            }
            else if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.New)
            {
                WindowManager.DisplayEditableWindow(new SkillEvaluation(this.Candidate), DetailsWindowModes.New);
            }
        }
        private void StageContextClick(object obj)
        {
            if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.Readonly)
            {
                if(( (object[])obj )[1] != null)
                {
                    long id = (long)( (object[])obj )[1].GetType().GetField("Id").GetValue(( (object[])obj )[1]);
                    WindowManager.DisplayEditableWindow(new RecruitmentStageDetails(this.Candidate), DetailsWindowModes.Readonly, new BasicDetailsControlConfiguration(true,false, false, true), id);
                }
            }
            else if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.New)
            {
                WindowManager.DisplayEditableWindow(new RecruitmentStageDetails(this.Candidate), DetailsWindowModes.New, new BasicDetailsControlConfiguration(true, false, false, true));
            }
        }
        private void SoftSkillEvaluationContextClick(object obj)
        {
            if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.Readonly)
            {
                if(( (object[])obj )[1] != null)
                {
                    long id = (long)( (object[])obj )[1].GetType().GetField("Id").GetValue(( (object[])obj )[1]);
                    WindowManager.DisplayEditableWindow(new SoftSkillEvaluation(this.Candidate), DetailsWindowModes.Readonly, id);
                }
            }
            else if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.New)
            {
                WindowManager.DisplayEditableWindow(new SoftSkillEvaluation(this.Candidate), DetailsWindowModes.New);
            }
        }

        private void DocumentClick(object obj)
        {
            if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.Readonly)
            {
                if(( (object[])obj )[1] != null)
                {
                    long id = Convert.ToInt64(( (object[])obj )[1].GetType().GetField("Id").GetValue(( (object[])obj )[1]));

                    using(var service = new DocumentServiceReference.DocumentsServiceClient())
                    {

                        DocumentServiceReference.Document doc = service.GetDocumentById(id);

                        string path = System.IO.Path.Combine( System.IO.Path.GetTempPath(), System.IO.Path.GetTempFileName() + doc.Extension);
                        System.IO.File.WriteAllBytes(path, doc.File);

                        System.Diagnostics.Process.Start(path);
                    }
                }
            }
        }
        #endregion

        public override void Refresh()
        {

            this.FakeDocumentsListDataDeveloper.InsertData(this.Candidate.Document);
            this.FakeSkillsEvaluationListDataDeveloper.InsertData(this.Candidate.Evaluation.SkillsEvaluation);
            this.FakeSoftSkillsEvaluationListDataDeveloper.InsertData(this.Candidate.Evaluation.SoftSkillsEvaluation);
            this.FakeRecruitmentStageListDataDeveloper.InsertData(this.Candidate.RecruitmentStage);

            //this.Candidate.Document = new List<Document>();

            this.ChangeContentDataBackgroundOperation(() =>
                {
                    this.DocumentsDataContext.Refresh();
                    this.SoftSkillsEvaluationDataContext.Refresh();
                    this.SkillsEvaluationListDataContext.Refresh();
                    this.RecruitmentStageDataContext.Refresh();
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
