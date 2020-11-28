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

        #endregion fields

        #region properties/commands

        public string Name
        {
            get
            {
                return "Profile Info";
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


        #region methods
        public void SetUserValues(int userId)
        {

            AccountModel currentAccount = new AccountModel();

            //TODO add method to queries class


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

                            
                            currentAccount.UserAge = reader.GetDateTime(1);
                            currentAccount.UserHeight = reader.GetInt32(2);
                            currentAccount.UserName = reader.GetString(3);
                            currentAccount.UserMail = reader.GetString(4);
                            currentAccount.UserGender = reader.GetString(5);
                            currentAccount.AccountCreated = reader.GetDateTime(6);

                            CurrentAccount = currentAccount;
                           
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



           
           
        }

        #endregion methods
    }
}
