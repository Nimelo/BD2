using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.Classes.Configurations
{
    public class BasicDetailsControlConfiguration
    {
        public Visibility IsEnableSave { get; set; }

        public Visibility IsEnableDelete { get; set; }

        public Visibility IsEnableRefresh { get; set; }

        public Visibility IsEnableClose { get; set; }

        public BasicDetailsControlConfiguration()
        {
            this.IsEnableClose = this.Do(true);
            this.IsEnableDelete = this.Do(true);
            this.IsEnableRefresh = this.Do(false);
            this.IsEnableSave = this.Do(true);
        }

        public BasicDetailsControlConfiguration(bool save, bool delete, bool refreshInEdit, bool close)
        {
            this.IsEnableClose = this.Do(close);
            this.IsEnableDelete = this.Do(delete);
            this.IsEnableRefresh = this.Do(refreshInEdit);
            this.IsEnableSave = this.Do(save);
        }

        public Visibility Do(bool value)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
