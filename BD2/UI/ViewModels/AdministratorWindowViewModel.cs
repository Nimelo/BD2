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
using UI.Components.Enums;
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
            this.BasicListViewModel = new BasicListViewModel<PersonListViewTemplate>(new PersonListDataDeveloper(),new Classes.Configurations.BasicListConfiguration(), this.DetailsClick);
        }

        #endregion

        private void DetailsClick(object obj)
        {
            if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.Readonly)
            {
                if(( (object[])obj )[1] != null)
                {
                    long id = (long)( (object[])obj )[1].GetType().GetField("Id").GetValue(( (object[])obj )[1]);
                    WindowManager.DisplayEditableWindow(new PersonUserDetails(), DetailsWindowModes.Readonly, id);
                }
            }
            else if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.New)
            {
                WindowManager.DisplayEditableWindow(new PersonUserDetails(), DetailsWindowModes.New);
            }
        }

       
    }
}
