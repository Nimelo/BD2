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
using UI.Components.ViewModels;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class EvaluatorWindow : UserControl
    {
        public EvaluatorWindow()
        {
            InitializeComponent();
            this.DataContext = new EvaluatorWindowViewModel();
        }
    }
}
