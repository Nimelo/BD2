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
using UI.Views;
using UI.ViewModels;
using UI.Classes;
using UI.Components.ViewModels;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.MainWindowViewModel = new MainWindowViewModel();
            this.MainWindowViewModel.Event += MainWindowViewModel_Event;
            InitializeComponent();
            this.DataContext = this.MainWindowViewModel;
        }

        void MainWindowViewModel_Event(Common.Enums.UserAccess access)
        {
            switch(access)
            {
                case Common.Enums.UserAccess.Administrator:
                    this.Content = new AdministratorWindow();
                    this.DataContext = new AdministratorWindowViewModel();
                    break;
                case Common.Enums.UserAccess.Evaluator:
                    this.Content = new EvaluatorWindow();
                    this.DataContext = new EvaluatorWindowViewModel();
                    break;
                case Common.Enums.UserAccess.Intern:
                    this.Content = new EvaluatorWindow();
                    this.DataContext = new EvaluatorWindowViewModel();
                    break;
                default:
                    break;
            }

            AppCommon.LoggedUserAccess = access;
        }

        public MainWindowViewModel MainWindowViewModel { get; set; }

    }
}
