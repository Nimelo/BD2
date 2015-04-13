using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Components.ViewModels
{
    public abstract class AbstractDetailsViewModel : INotifyPropertyChanged
    {
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties
        public int CurrentId { get; set; }

        private bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                this.Notify("IsEnabled");
            }
        }
        #endregion

        #region Ctors
        public AbstractDetailsViewModel()
        {
            this.IsEnabled = true;
        }

        #endregion

        public abstract void Refresh();
    }
}
