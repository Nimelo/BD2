using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UI.Interfaces;

namespace UI.Components
{
    public abstract class AbstractDetailsComponent : UserControl, IDetailsWindow
    {
        public  virtual void Save()
        {
            System.Threading.Thread.Sleep(1000);   
        }

        public virtual void Add()
        {
            System.Threading.Thread.Sleep(1000);
        }

        public virtual void Delete()
        {
            System.Threading.Thread.Sleep(1000);   
        }

        public virtual void Load()
        {
            System.Threading.Thread.Sleep(1000);   
        }

        public virtual void EnableControls(bool enable)
        {
            System.Threading.Thread.Sleep(1000);   
        }


        public virtual void ChangeMode(Enums.DetailsWindowModes mode)
        {
            System.Threading.Thread.Sleep(1000);   
        }

        public virtual void Refresh()
        {
            System.Threading.Thread.Sleep(1000); 
        }
        public long CurrentId { get; set; }
    
    }
}
