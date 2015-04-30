using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Components.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private string login;

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                this.Notify("Login");
            }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                this.Notify("Password");
            }
        }

    }
}
