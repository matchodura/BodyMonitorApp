﻿using BodyMonitorApp.Models;
using Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace BodyMonitorApp
{

    public class ProgressViewModel : ObservableObject, IPageViewModel
    {
        #region fields
               
        private ComboBoxHistory _selectedItem;
        private ProgressModel _currentProgress;
        private DateTime _calendarDate = DateTime.Now;  
        private bool _isChecked = false;
        private bool _recordExists = false;
        private Visibility _visibility = Visibility.Hidden;
        private Visibility _addButtonVisiblity = Visibility.Hidden;
        private Visibility _editButtonVisiblity = Visibility.Hidden;
        private Visibility _updateButtonVisiblity = Visibility.Hidden;
        private Visibility _gridVisibility = Visibility.Hidden;
        private Visibility _editBoxVisibility = Visibility.Hidden;
        private Visibility _textBlockVisibility = Visibility.Visible;

        #endregion                  

        #region properties/commands             

        public string Name
        {
            get
            {
                return "Progress";
            }
            set {; }
        }

        public ProgressModel CurrentProgress
        {
            get { return _currentProgress; }

            set
            {
                if (value != _currentProgress)
                {
                    _currentProgress = value;
                    OnPropertyChanged("CurrentProgress");
                }
            }

        }

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;

                OnPropertyChanged("IsChecked");
            }
        }

        public int UserId { get; set; }

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
        public Visibility AddButtonVisibility
        {
            get
            {
                return _addButtonVisiblity;
            }
            set
            {
                _addButtonVisiblity = value;

                OnPropertyChanged("AddButtonVisibility");
            }
        }
        public Visibility EditButtonVisibility
        {
            get
            {
                return _editButtonVisiblity;
            }
            set
            {
                _editButtonVisiblity = value;

                OnPropertyChanged("EditButtonVisibility");
            }
        }
        public Visibility UpdateButtonVisibility
        {
            get
            {
                return _updateButtonVisiblity;
            }
            set
            {
                _updateButtonVisiblity = value;

                OnPropertyChanged("UpdateButtonVisibility");
            }
        }        

        public Visibility TextBlockVisibility
        {
            get
            {
                return _textBlockVisibility;
            }
            set
            {
                _textBlockVisibility = value;

                OnPropertyChanged("TextBlockVisibility");
            }
        }

        public Visibility EditBoxVisibility
        {
            get
            {
                return _editBoxVisibility;
            }
            set
            {
                _editBoxVisibility = value;

                OnPropertyChanged("EditBoxVisibility");
            }
        }

        public Visibility GridVisibility
        {
            get
            {
                return _gridVisibility;
            }
            set
            {
                _gridVisibility = value;

                OnPropertyChanged("GridVisibility");
            }
        }

        public bool RecordExists
        {
            get
            {
                return _recordExists;
            }
            set
            {
                _recordExists = value;

                OnPropertyChanged("RecordExists");
            }
        }

        public ObservableCollection<ComboBoxHistory> ComboBoxChoices { get; set; }

        public ComboBoxHistory SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;

                    OnPropertyChanged("SelectedItem");

                    if (SelectedItem.Symbol == "Add new record")
                    {                                           
                        EditButtonVisibility = Visibility.Visible;
                        GridVisibility = Visibility.Visible;
                                              
                    }

                    else if (SelectedItem.Symbol == "View Values")
                    {
                        EditButtonVisibility = Visibility.Hidden;
                        EditBoxVisibility = Visibility.Hidden;
                        AddButtonVisibility = Visibility.Hidden;
                        UpdateButtonVisibility = Visibility.Hidden;

                        GridVisibility = Visibility.Visible;
                    }

                    else if (SelectedItem.Symbol == "Edit Record")
                    {      
                        EditButtonVisibility = Visibility.Visible;    
                        GridVisibility = Visibility.Visible;
                    }

                    else
                    {
                        EditButtonVisibility = Visibility.Hidden;
                        EditBoxVisibility = Visibility.Hidden;
                        AddButtonVisibility = Visibility.Hidden;
                        UpdateButtonVisibility = Visibility.Hidden;
                        GridVisibility = Visibility.Hidden;
                    }


                }
            }
        }

        public DateTime CalendarDate
        {
            get { return _calendarDate; }
            set
            {
                if (_calendarDate != value)
                {
                    _calendarDate = value;

                    if (SelectedItem.Symbol == "View Values")
                    {
                        GetUserValues();
                    }
                    else if (SelectedItem.Symbol == "Edit Record")
                    {
                        GetUserValues();
                    }

                    else if (SelectedItem.Symbol == "Add new record")
                    {
                        GetUserValues();
                    }


                    OnPropertyChanged("CalendarDate");

                }
            }
        }

        public HashSet<DateTime> Dates { get; } = new HashSet<DateTime>();

        #endregion

        public ProgressViewModel()
        {
            ComboBoxChoices = new ObservableCollection<ComboBoxHistory>();
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "-----" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Add new record" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Edit Record" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "View Values" });            
            SelectedItem = ComboBoxChoices[0];
            CurrentProgress = new ProgressModel();                    
        }

        #region methods

        public void AddRecord()
        {
            var progress = new ProgressModel();
            progress = CurrentProgress;
            progress.UserId = UserId;
            progress.DateAdded = CalendarDate;

            bool isAdded = Queries.AddNewRecord(progress);

            if (isAdded)
            {
                MessageBox.Show("Record Added!");

                TextBlockVisibility = Visibility.Visible;
                EditBoxVisibility = Visibility.Hidden;
                AddButtonVisibility = Visibility.Hidden;
            }

            else
            {
                MessageBox.Show("Record Not Added!");
            }
            
        }

        public void GetUserValues()
        {
            RecordExists = Queries.CheckIfRecordExists(UserId, CalendarDate);                       
            CurrentProgress = Queries.GetUserProgress(UserId, CalendarDate);
        }

        public void SetUserValues(int userId)
        {
            UserId = userId;
        }

        public void EditData()
        {
            IsChecked = !IsChecked;

            if (IsChecked && SelectedItem.Symbol == "Edit Record" )
            {
                TextBlockVisibility = Visibility.Hidden;
                EditBoxVisibility = Visibility.Visible;
                UpdateButtonVisibility = Visibility.Visible;
            
            }
            else if (IsChecked && SelectedItem.Symbol == "Add new record" && !RecordExists)
            {
                TextBlockVisibility = Visibility.Hidden;
                EditBoxVisibility = Visibility.Visible;             
                AddButtonVisibility = Visibility.Visible;
            }

            else if(IsChecked && SelectedItem.Symbol == "Add new record" && RecordExists)
            {
                TextBlockVisibility = Visibility.Hidden;
                EditBoxVisibility = Visibility.Visible;
                UpdateButtonVisibility = Visibility.Visible;

            }
            else
            {
                TextBlockVisibility = Visibility.Visible;
                UpdateButtonVisibility = Visibility.Hidden;
                EditBoxVisibility = Visibility.Hidden;
                AddButtonVisibility = Visibility.Hidden;
            }
        }
            
        public void UpdateRecord()
        {
            CurrentProgress.UserId = UserId;
          
            if (Queries.UpdateRecord(CurrentProgress,CalendarDate))
            {
                TextBlockVisibility = Visibility.Visible;
                EditBoxVisibility = Visibility.Hidden;
                UpdateButtonVisibility = Visibility.Hidden;
            }

            else
            {
                TextBlockVisibility = Visibility.Hidden;
                EditBoxVisibility = Visibility.Visible;
            }
            
        }

        #endregion methods

    }
}
