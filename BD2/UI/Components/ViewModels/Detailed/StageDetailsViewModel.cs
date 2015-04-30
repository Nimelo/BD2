using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.DictionaryServiceReference;


namespace UI.Components.ViewModels.Detailed
{
    public class StageDetailsViewModel : AbstractDetailsViewModel
    {
        #region Model
        private Stage stage;

        public Stage Stage
        {
            get { return stage; }
            set
            {
                stage = value;
                this.Notify("Stage");
            }
        }

        #endregion
        public StageDetailsViewModel()
        {
            this.Stage = new Stage();
        }


        public override void Refresh()
        {
           
        }
    }
}
