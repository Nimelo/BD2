using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.Classes.Configurations
{
    public class BasicListConfiguration
    {
        public Visibility IsEnableNew { get; set; }

        public Visibility IsEnablePreview { get; set; }


        public BasicListConfiguration()
        {
            this.IsEnableNew = this.Do(true);
            this.IsEnablePreview = this.Do(true);
        }

        public BasicListConfiguration(bool New, bool preview)
        {
            this.IsEnableNew = this.Do(New);
            this.IsEnablePreview = this.Do(preview);
        }

        public Visibility Do(bool value)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
