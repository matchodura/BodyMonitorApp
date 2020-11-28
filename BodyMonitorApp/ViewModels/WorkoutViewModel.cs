using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BodyMonitorApp
{
    public class WorkoutViewModel : ObservableObject, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Workouts";
            }
            set {;}
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
