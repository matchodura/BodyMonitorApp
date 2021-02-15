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
        private double _userWeight;
        private double _userHeight;
        private string _userName;
        private double _bmi;

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


        public double BMI
        {
            get
            {
                return _bmi;
            }
            set
            {
                _bmi = value;

                OnPropertyChanged("BMI");
            }
        }


        public double Weight
        {
            get
            {
                return _userWeight;
            }
            set
            {
                _userWeight = value;

                OnPropertyChanged("Weight");
            }
        }

        public double Height
        {
            get
            {
                return _userHeight;
            }
            set
            {
                _userHeight = value;

                OnPropertyChanged("Height");
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;

                OnPropertyChanged("UserName");
            }
        }



        #endregion properties/commands

        #region methods

        public void PopulateDashboard()
        {

            var queries = new Queries();
            Height = queries.GetHeight(UserName);
            Weight = queries.GetLastWeight(UserName);

            CalculateBMI(Height, Weight);


        }


        public void CalculateBMI(double height, double weight)
        {

            var heightInMeters = height / 100;

            BMI = weight / Math.Pow(heightInMeters, 2);
            BMI = Math.Round(BMI, 2);

        }
        #endregion

    }
}
