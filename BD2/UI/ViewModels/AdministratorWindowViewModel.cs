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
using UI.Components.ViewModels;
using UI.Managers;

namespace UI.ViewModels
{
    public class AdministratorWindowViewModel : INotifyPropertyChanged
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
        public BasicListViewModel<PersonListViewTemplate> BasicListViewModel { get; set; }

        #endregion

        #region Ctors
        public AdministratorWindowViewModel()
        {
            this.BasicListViewModel = new BasicListViewModel<PersonListViewTemplate>(new PersonListDataDeveloper(), new PersonUserDetails());
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
            WindowManager.DisplayEditableWindow(new PersonUserDetails(), Components.Enums.DetailsWindowModes.New);
        }
        #endregion
    }
}
