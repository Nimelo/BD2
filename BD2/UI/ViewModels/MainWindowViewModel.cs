using Common.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Classes;
using UI.Components.ViewModels;
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

        private LoginViewModel loginViewModel;

        public LoginViewModel LoginViewModel
        {
            get { return loginViewModel; }
            set
            {
                loginViewModel = value;
                this.Notify("LoginViewModel");
            }
        }


        public delegate void LogInEventHandler(UserAccess access);

        public event LogInEventHandler Event;

        ICommand _logInCommand;
        public ICommand LoginInCommand
        {
            get
            {
                return _logInCommand ??
                    ( _logInCommand = new RelayCommand(LogInCLick) );
            }
        }

        private void LogInCLick(object obj)
        {
            try
            {
                using(var service = new LoginServiceReference.LoginServiceClient())
                {
                    Event(service.Login(this.LoginViewModel.Login, this.LoginViewModel.Password));
                }
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show("Bad login or password!");
            }
        }

        public MainWindowViewModel()
        {
            this.LoginViewModel = new LoginViewModel();
        }

    }
}
