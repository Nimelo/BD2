using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace UI.Managers
{
    public class TaskManager
    {
        public static void DoBackgroundWork(Action work, Action workCompleted)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, ea) =>
            {
                try
                {
                    work.Invoke();
                }
                catch(Exception e)
                {

                }
                
            };

            worker.RunWorkerCompleted += (o, ea) =>
            {
                workCompleted.Invoke();
            };

            Dispatcher.CurrentDispatcher.Invoke(() => worker.RunWorkerAsync());
        }

        public static void DoDispatcherWork(Action work, Action workCompleted)
        {

            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                work.Invoke();
                workCompleted.Invoke();
            });
        }
    }
}
