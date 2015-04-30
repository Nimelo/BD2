using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Classes;
using UI.Classes.Configurations;
using UI.Classes.ListsDataDevelopers;
using UI.Classes.Templates;
using UI.Components.Enums;
using UI.Converters;
using UI.Managers;

namespace UI.Components.ViewModels
{
    public class BasicListViewModel<TemplateClass> : INotifyPropertyChanged
        where TemplateClass : class, new()
    {
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public List<TemplateClass> DisplayCollection { get; set; }

        #region Ctors

        public BasicListViewModel(AbstractListDataDeveloper dd, AbstractDetailsComponent detailsClass)
        {
            this.DisplayCollection = new List<TemplateClass>();
            this.DetailsControl = detailsClass;
            this.DataDeveloper = dd;

            this.ActualPage = 1;
            this.AmountOfPages = (int)Math.Ceiling((double)this.DataDeveloper.GetAmountOfRecords() / 10);
            this.DisplayCollection = TemplateConverter.Convert<TemplateClass>(this.DataDeveloper.LoadData(1));

            this.DetailsConfiguration = new BasicDetailsControlConfiguration();
            this.Configuration = new BasicListConfiguration();

        }
        public BasicListViewModel(AbstractListDataDeveloper dd, AbstractDetailsComponent detailsClass, BasicListConfiguration conf, BasicDetailsControlConfiguration detailsConf)
        {
            this.DisplayCollection = new List<TemplateClass>();
            this.DetailsControl = detailsClass;
            this.DataDeveloper = dd;

            this.ActualPage = 1;
            this.AmountOfPages = (int)Math.Ceiling((double)this.DataDeveloper.GetAmountOfRecords() / 10);
            this.DisplayCollection = TemplateConverter.Convert<TemplateClass>(this.DataDeveloper.LoadData(1));

            this.DetailsConfiguration = detailsConf;

            this.Configuration = conf;
        }

        public BasicListViewModel(AbstractListDataDeveloper dd, BasicListConfiguration conf, Action<object> click = null)
        {
            this.DisplayCollection = new List<TemplateClass>();
            this.DataDeveloper = dd;

            this.ActualPage = 1;
            this.AmountOfPages = (int)Math.Ceiling((double)this.DataDeveloper.GetAmountOfRecords() / 10);
            this.DisplayCollection = TemplateConverter.Convert<TemplateClass>(this.DataDeveloper.LoadData(1));

            if(click != null)
                this._contextMenuCommand = new RelayCommand(click);

            this.DetailsConfiguration = new BasicDetailsControlConfiguration();

            this.Configuration = conf;

        }


        #endregion

        #region Delegate
        public delegate void MenuClick(object parameter);
        #endregion

        #region Properites and Fields

        private TemplateClass selectedItem;

        public TemplateClass SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                this.Notify("SelectedItem");
            }
        }

        private BasicListConfiguration configuration;

        public BasicListConfiguration Configuration
        {
            get { return configuration; }
            set
            {
                configuration = value;
                this.Notify("Configuration");
            }
        }

        private BasicDetailsControlConfiguration DetailsConfiguration { get; set; }
        public AbstractDetailsComponent DetailsControl { get; set; }
        public AbstractListDataDeveloper DataDeveloper { get; set; }

        private int _actualPage;

        public int ActualPage
        {
            get { return _actualPage; }
            set
            {
                _actualPage = value;
                this.Notify("ActualPage");
            }
        }

        private int _amoutOfPages;

        public int AmountOfPages
        {
            get { return _amoutOfPages; }
            set
            {
                _amoutOfPages = value;
                this.Notify("AmountOfPages");
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                this.Notify("IsBusy");
            }
        }
        #endregion


        #region Commands
        ICommand _navigationCommand;
        public ICommand NavigationCommand
        {
            get
            {
                return _navigationCommand ??
                    ( _navigationCommand = new RelayCommand(NavigationClick) );
            }
        }

        ICommand _contextMenuCommand;
        public ICommand ContextMenuCommand
        {
            get
            {
                return _contextMenuCommand ??
                    ( _contextMenuCommand = new RelayCommand(ContextMenuClick) );
            }
        }

        public virtual void ContextMenuClick(object obj)
        {
            if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.Readonly)
            {
                if(( (object[])obj )[1] != null)
                {
                    long id = (long)( (object[])obj )[1].GetType().GetField("Id").GetValue(( (object[])obj )[1]);
                    WindowManager.DisplayEditableWindow(this.DetailsControl, DetailsWindowModes.Readonly, this.DetailsConfiguration, id);
                }
            }
            else if((DetailsWindowModes)( (object[])obj )[0] == DetailsWindowModes.New)
            {
                WindowManager.DisplayEditableWindow(this.DetailsControl, DetailsWindowModes.New, this.DetailsConfiguration);
            }

        }

        public virtual void NavigationClick(object obj)
        {
            if(obj.GetType() == typeof(ListsButtonEnum))
            {
                switch((ListsButtonEnum)obj)
                {
                    case ListsButtonEnum.Previous:
                        this.AmountOfPages = (int)Math.Ceiling((double)this.DataDeveloper.GetAmountOfRecords() / 10);
                        this.ChangeContentDataBackgroundOperation(() =>
                        {
                            this.ChangeToPage(this.ActualPage - 1);
                        });

                        break;
                    case ListsButtonEnum.Next:
                        this.AmountOfPages = (int)Math.Ceiling((double)this.DataDeveloper.GetAmountOfRecords() / 10);
                        this.ChangeContentDataBackgroundOperation(() =>
                        {
                            this.ChangeToPage(this.ActualPage + 1);
                        });

                        break;
                    case ListsButtonEnum.Refresh:
                        this.AmountOfPages = (int)Math.Ceiling((double)this.DataDeveloper.GetAmountOfRecords() / 10);
                        this.ChangeContentDataBackgroundOperation(() =>
                        {
                            this.ChangeToPage(1);
                        });

                        break;
                    default:
                        break;
                }
            }
        }
        #endregion


        #region Private Methods
        private void ChangeToPage(int pageNumber)
        {
            if(pageNumber <= 0
                    || pageNumber > this.AmountOfPages)
            {
                this.DisplayCollection.Clear();
                this.Notify("DisplayCollection");
                return;
            }

            this.DisplayCollection.Clear();
            this.DisplayCollection = TemplateConverter.Convert<TemplateClass>(this.DataDeveloper.LoadData(pageNumber));
            this.Notify("DisplayCollection");

            this.ActualPage = pageNumber;
        }

        private void ChangeContentDataBackgroundOperation(Action work)
        {

            TaskManager.DoBackgroundWork(() =>
          {
              this.IsBusy = true;
              System.Threading.Thread.Sleep(1000);
              work.Invoke();
              this.IsBusy = false;
          }, () =>
          {
              this.IsBusy = false;
          }
          );
        }


        #endregion

        public void Refresh()
        {
            this.NavigationClick(ListsButtonEnum.Refresh);
        }
    }
}
