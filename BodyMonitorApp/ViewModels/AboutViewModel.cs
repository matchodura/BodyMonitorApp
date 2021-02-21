using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BodyMonitorApp
{
    public class AboutViewModel : ObservableObject, IPageViewModel
    {
        #region fields

        private Visibility _visibility = Visibility.Hidden;

        #endregion

        #region properties

        public string Name
        {
            get { return "About";}
            set {;}
        }     
        public Visibility Visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                _visibility = value;

                OnPropertyChanged("Visibility");
            }
        }

        #endregion properties
    }
}
