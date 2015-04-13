using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UI.Classes;
using UI.ViewModels;

namespace UI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public AdministratorWindowViewModel AdminViewModel { get; set; }

        public EvaluatorWindowViewModel EvaluatorViewModel { get; set; }

        public MainWindowViewModel()
        {
            this.AdminViewModel = new AdministratorWindowViewModel();
            this.EvaluatorViewModel = new EvaluatorWindowViewModel();
        }    

    }
}
