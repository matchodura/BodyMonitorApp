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

        #region fields

        private Visibility _visibility = Visibility.Hidden;

        #endregion

        #region properties/commands

        public string Name
        {
            get
            {
                return "Workouts";
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

        #endregion
    }


}
