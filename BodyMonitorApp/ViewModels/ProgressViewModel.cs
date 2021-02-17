using BodyMonitorApp.Models;
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

        private Visibility _visibility = Visibility.Hidden;
        private Visibility _addButtonVisiblity = Visibility.Hidden;
        private Visibility _editButtonVisiblity = Visibility.Hidden;
        private Visibility _updateButtonVisiblity = Visibility.Hidden;
        private Visibility _gridVisibility = Visibility.Hidden;      
        private Visibility _editBoxVisibility = Visibility.Hidden;
        private Visibility _textBlockVisibility = Visibility.Visible;
        private ComboBoxHistory _selectedItem;
        private ProgressModel _currentProgress;
        private DateTime _calendarDate = DateTime.Now;
        private ICommand _addItemCommand;
        private bool _isChecked = false;
        private bool _recordExists = false;

        #endregion
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
        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(
                        param => AddItems());

                }
                return _addItemCommand;
            }
        }

        #region properties/commands             

        public string Name
        {
            get
            {
                return "Progress";
            }
            set {; }
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

               
        #endregion
        public HashSet<DateTime> Dates { get; } = new HashSet<DateTime>();


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

        public void AddItems()
        {
            var progress = new ProgressModel();
            progress = CurrentProgress;
      


            try
            {

                // DateTime currentTime = DateTime.Now.Time;


                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "INSERT INTO dbo.UserBodyValues (UserId,DateAdded,Weight,Neck,Chest,Stomach,Waist,Hips,Thigh,Calf,Biceps) values (@UserId,@DateAdded,@Weight,@Neck,@Chest,@Stomach,@Waist,@Hips,@Thigh,@Calf,@Biceps)";


                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                cmd.Parameters.Add("@Weight", SqlDbType.Decimal).Value = progress.Weight;
                cmd.Parameters.Add("@Neck", SqlDbType.Decimal).Value = progress.Neck;
                cmd.Parameters.Add("@Chest", SqlDbType.Decimal).Value = progress.Chest;
                cmd.Parameters.Add("@Stomach", SqlDbType.Decimal).Value = progress.Stomach;
                cmd.Parameters.Add("@Waist", SqlDbType.Decimal).Value = progress.Waist;
                cmd.Parameters.Add("@Hips", SqlDbType.Decimal).Value = progress.Hips;
                cmd.Parameters.Add("@Thigh", SqlDbType.Decimal).Value = progress.Thigh;
                cmd.Parameters.Add("@Calf", SqlDbType.Decimal).Value = progress.Calf;
                cmd.Parameters.Add("@Biceps", SqlDbType.Decimal).Value = progress.Biceps;
                cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = CalendarDate;



                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Record Added!");
                }


            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);
            }
        }


        public void GetUserValues()
        {
          
            //TODO add method to queries class
            //sql

            try
            {

                SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                string sql = "SELECT * FROM dbo.UserBodyValues WHERE UserId=@UserId AND DateAdded=@DateAdded";



                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = CalendarDate;




                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // if the result set is not NULL
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            CurrentProgress.Weight = reader.GetDecimal(4);
                            CurrentProgress.Neck = reader.GetDecimal(5);
                            CurrentProgress.Chest = reader.GetDecimal(6);
                            CurrentProgress.Stomach = reader.GetDecimal(7);
                            CurrentProgress.Waist = reader.GetDecimal(8);
                            CurrentProgress.Hips = reader.GetDecimal(9);
                            CurrentProgress.Thigh = reader.GetDecimal(10);
                            CurrentProgress.Calf = reader.GetDecimal(11);
                            CurrentProgress.Biceps = reader.GetDecimal(12);                                               

                        }

                        RecordExists = true;
                    }
                    else
                    {
                        RecordExists = false;
                        MessageBox.Show("Record doesn't exist!");
                    }

                }
                conn.Close();

            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);

            }
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

        //TO:DO
        public void DeleteRecord()
        {

        }

        public void UpdateRecord()
        {

            CurrentProgress.UserId = UserId;
            var query = new Queries();


            if (query.UpdateRecord(CurrentProgress,CalendarDate))
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
