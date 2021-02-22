using BodyMonitorApp.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private double _bodyValue;
        private string _userName;
        private double _bmi;
        private ComboBoxHistory _selectedItem;

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

        public double BodyValue
        {
            get
            {
                return _bodyValue;
            }
            set
            {
                _bodyValue = value;

                OnPropertyChanged("BodyValue");
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

        public ComboBoxHistory SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;

                    if(UserName!=null)
                    {
                        PopulateDashboard();
                    }
                    


                    OnPropertyChanged("SelectedItem");
                   
                }
            }
        }

        public ObservableCollection<ComboBoxHistory> ComboBoxChoices { get; set; }

        #endregion

        public HomeViewModel()
        {
            ComboBoxChoices = new ObservableCollection<ComboBoxHistory>();
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Neck" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Chest" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Stomach" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Waist" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Hips" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Thigh" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Calf" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Biceps" });

            SelectedItem = ComboBoxChoices[0];
        }
     
        #region methods

        public void PopulateDashboard()
        {       
            Height = Queries.GetHeight(UserName);
            Weight = Queries.GetLastWeight(UserName);
            BodyValue = Queries.GetBodyPart(UserName, SelectedItem.Symbol);
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
