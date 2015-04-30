using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UI.Classes.Configurations;
using UI.Components;
using UI.Components.Enums;
using UI.Components.ViewModels;


namespace UI.Managers
{
    public class WindowManager
    {
        public static void DisplayEditableWindow(AbstractDetailsComponent content, DetailsWindowModes mode, BasicDetailsControlConfiguration conf, long Id = -1)
        {
            Window window = new Window();
            var tmp = new BasicDetailsControl();
            content.CurrentId = Id;
            tmp.DataContext = new BasicDetailsControlViewModel(content, mode, Id, conf);
            window.Content = tmp;

            window.ShowDialog();

        }

        public static void DisplayEditableWindow(AbstractDetailsComponent content, DetailsWindowModes mode, long Id = -1)
        {
            Window window = new Window();
            var tmp = new BasicDetailsControl();
            content.CurrentId = Id;
            tmp.DataContext = new BasicDetailsControlViewModel(content, mode, Id, new BasicDetailsControlConfiguration());
            window.Content = tmp;

            window.ShowDialog();

        }
        public static void DisplayListWindow<TemplateClass>(BasicListViewModel<TemplateClass> vm)  
            where TemplateClass : class, new()
        {
            Window window = new Window();
            var tmp = new BasicList();
            tmp.DataContext = vm;
            window.Content = tmp;

            window.ShowDialog();

            
        }
    }
}
