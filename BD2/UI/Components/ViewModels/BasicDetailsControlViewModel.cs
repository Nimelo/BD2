using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Classes;
using UI.Classes.Configurations;
using UI.Components.Enums;
using UI.Managers;

namespace UI.Components.ViewModels
{
    class BasicDetailsControlViewModel : INotifyPropertyChanged
    {
        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
            if(obj.GetType() == typeof(DetailsWindowButtonsEnum))
            {
                switch((DetailsWindowButtonsEnum)obj)
                {
                    case DetailsWindowButtonsEnum.Edit:
                        this.ChangeMode(DetailsWindowModes.Edit);
                        break;
                    case DetailsWindowButtonsEnum.Save:

                        this.ChangeContentDataBackgroundOperation(() =>
                            {
                                if(this.CurrentMode != DetailsWindowModes.New)
                                {
                                    this.ContentControl.Save();
                                }
                                else
                                {
                                    this.ContentControl.Add();
                                    this.CurrentId = this.ContentControl.CurrentId;
                                }

                                this.ChangeMode(DetailsWindowModes.Readonly);
                            });


                        break;
                    case DetailsWindowButtonsEnum.Refresh:
                        this.ChangeContentDataBackgroundOperation(() =>
                            {
                                this.ContentControl.Load();
                                this.ContentControl.Refresh();
                            }
                            );

                        break;
                    case DetailsWindowButtonsEnum.Close:
                        break;

                    case DetailsWindowButtonsEnum.Delete:
                        this.ContentControl.Delete();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Ctors

        public BasicDetailsControlViewModel(AbstractDetailsComponent uc, DetailsWindowModes mode, long id, BasicDetailsControlConfiguration configuration)
        {
            this.ContentControl = uc;
            this.ActualMode = mode;
            this.IsBusy = false;
            this.ChangeMode(this.ActualMode);
            this.IsSelectedReadonly = mode == DetailsWindowModes.Readonly ? true : false;
            this.CurrentId = id;
            this.CurrentMode = mode;
            this.DeleteEnabled = DeleteEnabled;
            this.Configuration = configuration;

        }

        #endregion

        #region Properties and Fields

        private BasicDetailsControlConfiguration basicDetailsControlConfiguration;

        public BasicDetailsControlConfiguration Configuration
        {
            get { return basicDetailsControlConfiguration; }
            set
            {
                basicDetailsControlConfiguration = value;
                this.Notify("Configuration");
            }
        }

        public long CurrentId { get; set; }

        public DetailsWindowModes CurrentMode { get; set; }
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

        private Visibility _isReadonly;

        public Visibility IsReadonly
        {
            get { return _isReadonly; }
            set
            {
                _isReadonly = value;
                this.Notify("IsReadonly");
            }
        }

        private Visibility _isEdit;

        public Visibility IsEdit
        {
            get { return _isEdit; }
            set
            {
                _isEdit = value;
                this.Notify("IsEdit");
            }
        }

        public AbstractDetailsComponent ContentControl { get; set; }

        public DetailsWindowModes ActualMode { get; set; }

        private bool _isSelectedReadonly;

        public bool IsSelectedReadonly
        {
            get { return _isSelectedReadonly; }
            set
            {
                _isSelectedReadonly = value;
                this.Notify("IsSelectedReadonly");
                this.Notify("IsSelectedEdit");
            }
        }

        public bool IsSelectedEdit
        {
            get { return !_isSelectedReadonly; }
            set
            {
                _isSelectedReadonly = !value;
                this.Notify("IsSelectedEdit");
            }
        }

        private bool deleteEnabled;

        public bool DeleteEnabled
        {
            get { return deleteEnabled; }
            set
            {
                deleteEnabled = value;
                this.Notify("DeleteEnabled");
            }
        }

        #endregion

        #region Public methods

        public void ChangeMode(DetailsWindowModes mode)
        {
            this.CurrentMode = mode;
            switch(mode)
            {
                case DetailsWindowModes.Readonly:

                    this.IsReadonly = Visibility.Visible;
                    this.IsEdit = Visibility.Collapsed;
                    this.IsSelectedReadonly = true;

                    this.ChangeModeBackgroundOperation(mode);

                    break;
                case DetailsWindowModes.Edit:

                    this.IsEdit = Visibility.Visible;
                    this.IsReadonly = Visibility.Collapsed;

                    this.IsSelectedReadonly = false;
                    this.ChangeModeBackgroundOperation(mode);

                    break;
                case DetailsWindowModes.New:
                    this.IsReadonly = Visibility.Visible;
                    this.IsEdit = Visibility.Collapsed;
                    this.IsSelectedReadonly = true;

                    this.IsEdit = Visibility.Visible;
                    this.IsReadonly = Visibility.Collapsed;

                    this.IsSelectedReadonly = false;

                    this.ChangeModeBackgroundOperation(mode);

                    break;
                default:
                    break;
            }
        }

        private void ChangeModeBackgroundOperation(DetailsWindowModes changeTo)
        {
            TaskManager.DoBackgroundWork(() =>
            {
                this.IsBusy = true;
                this.ContentControl.ChangeMode(changeTo);
            }, () =>
            {
                this.IsBusy = false;
            }
            );
        }

        private void ChangeContentDataBackgroundOperation(Action work)
        {
            TaskManager.DoBackgroundWork(() =>
            {
                this.IsBusy = true;
                work.Invoke();
            }, () =>
            {
                this.IsBusy = false;
            }
            );
        }
        #endregion



    }
}
