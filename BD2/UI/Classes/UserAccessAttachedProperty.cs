using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.Classes
{
    public class UserAccessAttachedProperty : FrameworkElement
    {
        public static UserAccess GetUserAccessType(DependencyObject obj)
        {
            return (UserAccess)obj.GetValue(UserAccessProperty);
        }

        public static void SetUserAccessType(DependencyObject obj, UserAccess value)
        {
            obj.SetValue(UserAccessProperty, value);
        }

        public static readonly DependencyProperty UserAccessProperty =
            DependencyProperty.RegisterAttached("UserAccessType",
            typeof(UserAccess), typeof(UserAccess), new PropertyMetadata
            (UserAccess.Administrator, new PropertyChangedCallback(UserAcccessnChanged)));

        private static void UserAcccessnChanged
        (DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement userAccess = (FrameworkElement)d;
            if(( (UserAccess)e.NewValue & AppCommon.LoggedUserAccess ) == AppCommon.LoggedUserAccess)
                userAccess.IsEnabled = true;
            else
                userAccess.IsEnabled = false;
        }

    }
}
