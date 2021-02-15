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
        private Visibility _buttonVisiblity = Visibility.Hidden;
        private ComboBoxHistory _selectedItem;
        private ProgressModel _currentProgress;
        private DateTime _calendarDate;
        private ICommand _addItemCommand;
      



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
        public Visibility ButtonVisibility
        {
            get
            {
                return _buttonVisiblity;
            }
            set
            {
                _buttonVisiblity = value;

                OnPropertyChanged("ButtonVisibility");
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

                    if (SelectedItem.Symbol == "Add new item!")
                    {

                        ButtonVisibility = Visibility.Visible;

                        ProgressModel test = new ProgressModel();

                        test.UserId = UserId;
                        CurrentProgress = test;


                    }

                    else if (SelectedItem.Symbol == "View Values")
                    {
                        CurrentProgress = null;
                        ButtonVisibility = Visibility.Hidden;



                    }
                    else
                    {
                        ButtonVisibility = Visibility.Hidden;
                        CurrentProgress = null;
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
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "Add new item!" });
            ComboBoxChoices.Add(new ComboBoxHistory() { Symbol = "View Values" });

            SelectedItem = ComboBoxChoices[0];

            ProgressModel test = new ProgressModel();

            CurrentProgress = test;
                    

        }

        #region methods

        public void AddItems()
        {


            ProgressModel progress = new ProgressModel
            {
                UserId = CurrentProgress.UserId,
                Weight = CurrentProgress.Weight,
                Biceps = CurrentProgress.Biceps,
                Chest = CurrentProgress.Chest,
                DateAdded = CalendarDate
            };



            try
            {

                // DateTime currentTime = DateTime.Now.Time;


                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "INSERT INTO dbo.UserBodyValues (UserId,DateAdded,Weight,Biceps,Chest) values (@UserId,@DateAdded,@Weight,@Biceps,@Chest)";


                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = progress.UserId;
                cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = progress.DateAdded;
                cmd.Parameters.Add("@Weight", SqlDbType.Decimal).Value = progress.Weight;
                cmd.Parameters.Add("@Biceps", SqlDbType.Decimal).Value = progress.Biceps;
                cmd.Parameters.Add("@Chest", SqlDbType.Decimal).Value = progress.Chest;



                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Query Added!");
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
            ProgressModel progress = new ProgressModel
            {
                UserId = UserId,

            };

            //TODO add method to queries class
            //sql

            try
            {

                SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                string sql = "SELECT * FROM dbo.UserBodyValues WHERE UserId=@UserId AND DateAdded=@DateAdded";



                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = progress.UserId;
                cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = CalendarDate;




                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // if the result set is not NULL
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            progress.Weight = reader.GetDecimal(4);
                            progress.Chest = reader.GetDecimal(6);
                            progress.Biceps = reader.GetDecimal(12);


                            //currentAccount.UserAge = reader.GetDateTime(1);
                            //currentAccount.UserHeight = reader.GetInt32(2);
                            //currentAccount.UserName = reader.GetString(3);
                            //currentAccount.UserMail = reader.GetString(4);
                            //currentAccount.UserGender = reader.GetString(5);
                            //currentAccount.AccountCreated = reader.GetDateTime(6);

                        
                            CurrentProgress = progress;

                        }


                        // update the existing value + the value from the text file
                    }
                    else
                    {
                        MessageBox.Show("Record doesn't exist!");

                    }

                }

            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);

            }




            //CurrentProgress = progress;
        }






        public void SetUserValues(int userId)
        {
            UserId = userId;
        }

        #endregion methods

      

       

       

     
       

      

      
    }
}
