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
        public string Name
        {
            get
            {
                return "Home";
            }
            set {; }
        }

        private Visibility _visibility = Visibility.Hidden;
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
    }
}
