using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BodyMonitorApp
{
    public class ProfileInfoViewModel : ObservableObject, IPageViewModel
    {

        #region fields

        private AccountModel _currentAccount;
        private Visibility _visibility = Visibility.Hidden;

        private Visibility _textBlockVisibility = Visibility.Visible;

        private Visibility _editBoxVisibility = Visibility.Hidden;

        private int _userId;

        private bool _isChecked = false;



        #endregion fields

        #region properties/commands


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

        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;

                OnPropertyChanged("UserId");
            }
        }


        public string Name
        {
            get{return "Profile Info";} set {;}
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
        public AccountModel CurrentAccount
        {
            get { return _currentAccount; }

            set
            {
                if (value != _currentAccount)
                {
                    _currentAccount = value;
                    OnPropertyChanged("CurrentAccount");
                }
            }

        }
        #endregion


        public ProfileInfoViewModel()
        {
            var account = new AccountModel();
            CurrentAccount = account;
        }




        #region methods

        /// <summary>
        /// Displaying user profile data
        /// </summary>
        /// <param name="userId"></param>

        public void SetUserValues(int userId)
        {

            AccountModel currentAccount = new AccountModel();

            UserId = userId;
            //TODO: add method to queries class


            try
            {

                SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                string sql = "SELECT * FROM dbo.UserData WHERE Id=@UserId";
                              
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
               



                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // if the result set is not NULL
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var test = DateTime.Now;

                            var tempDate = DateTime.Now;

                            //tempDate = reader.GetDateTime(1);
                            currentAccount.UserBirthday = reader.GetDateTime(1);
                            currentAccount.UserHeight = reader.GetInt32(2);
                            currentAccount.UserName = reader.GetString(3);
                            currentAccount.UserMail = reader.GetString(4);
                            currentAccount.UserGender = reader.GetString(5);
                            currentAccount.AccountCreated = reader.GetDateTime(6);


                            var result = test.Year - tempDate.Year;

                            //MessageBox.Show(result.ToString());

                            //// var result = (test - tempDate).TotalDays/365;

                            //// var resultint = Convert.ToInt32(Math.Floor(result));

                            //DateTime dt = DateTime.Parse(result.ToString());
                            ////var yourDateString = yourDateTime.ToString("dd/MM/yyyy");
                            ////MessageBox.Show(yourDateString);
                            //currentAccount.UserAge = dt;
                            //CurrentAccount = currentAccount;
                           
                        }


                        // update the existing value + the value from the text file
                    }
                    else
                    {
                        MessageBox.Show("Wrong Data!");
                       
                    }

                }

            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);
              
            }


            CurrentAccount = currentAccount;



        }


        //todo: logika
        public void EditData()
        {
                     
            IsChecked = !IsChecked;

            if (IsChecked)
            {
                TextBlockVisibility = Visibility.Hidden;
                EditBoxVisibility = Visibility.Visible;
            }
            else
            {
                TextBlockVisibility = Visibility.Visible;
                EditBoxVisibility = Visibility.Hidden;
            }


        }

            
        public void UpdateData()
        {

            var account = CurrentAccount;

            try
            {


                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "UPDATE dbo.UserData " +
                   "SET UserName=@UserName,UserAge=@UserAge,UserHeight=@UserHeight, UserMail=@UserMail, UserGender=@UserGender" +
                   " WHERE Id=@UserId";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                cmd.Parameters.Add("@UserBirthday", SqlDbType.DateTime).Value = account.UserBirthday;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = account.UserName;
                cmd.Parameters.Add("@UserHeight", SqlDbType.Int).Value = account.UserHeight;
                cmd.Parameters.Add("@UserMail", SqlDbType.VarChar).Value = account.UserMail;
                cmd.Parameters.Add("@UserGender", SqlDbType.Char).Value = account.UserGender;


                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data Updated!");
                }

            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);
            }


            TextBlockVisibility = Visibility.Visible;
            EditBoxVisibility = Visibility.Hidden;



        }


    }
        #endregion methods
}

