﻿using System;
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
using UI.DictionaryServiceReference;

namespace UI.Components.Detailed
{
    /// <summary>
    /// Interaction logic for PersonDetails.xaml
    /// </summary>
    public partial class SkillDetails : AbstractDetailsComponent
    {
        public SkillDetails()
        {
            InitializeComponent();
            this.ViewModel = new SkillDetailsViewModel();
            this.DataContext = this.ViewModel;
        }


        public SkillDetailsViewModel ViewModel { get; set; }


        public override void Add()
        {
            using(var service = new DictionaryServiceClient())
            {
                try
                {
                    this.CurrentId = service.SaveSkill(this.ViewModel.Skill);
                }
                catch(Exception e)
                {

                }

            }
        }

        public override void Save()
        {
            using(var service = new DictionaryServiceClient())
            {
                try
                {
                    this.CurrentId = service.SaveSkill(this.ViewModel.Skill);
                }
                catch(Exception e)
                {

                }

            }
        }

        public override void Delete()
        {
            using(var service = new DictionaryServiceClient())
            {
                try
                {
                    service.DeleteSkill(this.ViewModel.Skill);
                }
                catch(Exception e)
                {

                }

            }
        }

        public override void Load()
        {
            using(var service = new DictionaryServiceClient())
            {
                try
                {
                    this.ViewModel.Skill = service.GetSkillById(this.CurrentId);
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
                    break;
                case UI.Components.Enums.DetailsWindowModes.Edit:
                    this.ViewModel.IsEnabled = true;
                    break;
                case UI.Components.Enums.DetailsWindowModes.New:
                    this.ViewModel.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
