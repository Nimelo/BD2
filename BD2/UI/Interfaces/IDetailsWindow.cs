using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Components.Enums;

namespace UI.Interfaces
{
    interface IDetailsWindow
    {
        void Save();
        void Delete();

        void Load();

        void EnableControls(bool enable);

        void ChangeMode(DetailsWindowModes mode);

        void Add();
    }
}
