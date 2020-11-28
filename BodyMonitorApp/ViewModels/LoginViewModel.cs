using BodyMonitorApp;
using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BodyMonitorApp
{
    public class LoginViewModel : ObservableObject, IPageViewModel
    {

        #region fields
        private string _userLogin;
        private string _userPassword;
        private string _name;

        private LoginModel _currentLogin;

        private ICommand _loginUserCommand;


        #endregion

        public LoginViewModel()
        {

            _name = "Login";
            LoginModel login = new LoginModel();

            login.LoggedIn = false;

            CurrentLogin = login;


        }




        #region properties

        public string Name
        {
            get {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }

        }

        public string UserLogin
        {
            get { return _userLogin; }
            set
            {
                if (value != _userLogin)
                {
                    _userLogin = value;
                    OnPropertyChanged("UserLogin");
                }
            }
        }

        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                if (value != _userPassword)
                {
                    _userPassword = value;
                    OnPropertyChanged("UserPassword");
                }
            }
        }


        public SecureString SecurePassword { private get; set; }

        public LoginModel CurrentLogin
        {
            get { return _currentLogin; }

            set
            {
                if (value != _currentLogin)
                {
                    _currentLogin = value;
                    OnPropertyChanged("CurrentLogin");
                }
            }

        }

                
        public ICommand LoginUserCommand
        {
            get
            {
                if (_loginUserCommand == null)
                {
                    _loginUserCommand = new RelayCommand(
                        param => LoginUser());

                }
                return _loginUserCommand;
            }
        }
        #endregion

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


       

        public bool LoginUser()
        {

            LoginModel login = new LoginModel
            {
                UserName = UserLogin,
                UserPassword = UserPassword,
                UserId = 0,
                LoggedIn = false
            };

                        
            if (string.IsNullOrWhiteSpace(login.UserName))
            {

                MessageBox.Show("Missing Login!");
                return false;

            }

            else if (string.IsNullOrEmpty(login.UserPassword))
                {

                MessageBox.Show("Missing Password!");
                return false;

            }
            else
            {

                try
                {

                    SqlConnection conn = new SqlConnection(Connections.ConnectionString);


                    string sql = "SELECT UserId FROM dbo.Users WHERE UserLogin=@UserLogin AND UserPassword=@UserPassword";



                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@UserLogin", SqlDbType.VarChar).Value = login.UserName;
                    cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = login.UserPassword;



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // if the result set is not NULL
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                var UserId = reader.GetInt32(0);
                                login.UserId = UserId;
                                login.LoggedIn = true;
                                CurrentLogin = login;
                                
                                MessageBox.Show($"Login Succesfull!+userId:{login.UserId}");
                                return true;
                            }

                            
                            // update the existing value + the value from the text file
                        }



                        else 
                        {
                            MessageBox.Show("Wrong Data!");
                            return false;
                        }
                                                
                    }
                                                           
                }

                catch (SqlException ex)
                {
                    string errorMessage = $"Error: {ex}";
                    MessageBox.Show(errorMessage);
                    return false;
                }


                return false;
                
            }

            
        }
           
    }


}
