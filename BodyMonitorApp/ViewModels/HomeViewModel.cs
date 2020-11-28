using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BodyMonitorApp
{
    public class HomeViewModel : ObservableObject, IPageViewModel
    {

        #region fields

        private Visibility _visibility = Visibility.Hidden;

        #endregion fields

        #region properties/commands
        public string Name
        {
            get
            {
                return "Home";
            }
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
        #endregion properties/commands
    }
}
