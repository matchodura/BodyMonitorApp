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
        private bool _isCheckedCreateAccount = false;
        private bool _isCheckedForgotPassword = false;
        private LoginModel _currentLogin;
        private ICommand _loginUserCommand;
        private IPageViewModel _currentPageViewModel;
        private Visibility _visibility = Visibility.Hidden;

        #endregion           
    
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

        public CreateAccountViewModel CreateAccountVM { get; set; }

        public ForgotPasswordViewModel ForgotPasswordVM { get; set; }

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

        public bool IsCheckedCreateAccount
        {
            get { return _isCheckedCreateAccount; }

            set
            {
                _isCheckedCreateAccount = value;
                OnPropertyChanged("IsCheckedCreateAccount");

            }
        }

        public bool IsCheckedForgotPassword
        {
            get { return _isCheckedForgotPassword; }

            set
            {
                _isCheckedForgotPassword = value;
                OnPropertyChanged("IsCheckedForgotPassword");
                          
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        public LoginViewModel()
        {
            CreateAccountVM = new CreateAccountViewModel();
            ForgotPasswordVM = new ForgotPasswordViewModel();
            _name = "Login";
            LoginModel login = new LoginModel();
            login.LoggedIn = false;
            CurrentLogin = login;
        }

        #region methods

        public bool LoginUser(string password)
        {

            LoginModel login = new LoginModel
            {
                UserName = UserLogin,
                UserPassword = password,
                UserId = 0,
            
                LoggedIn = false
            };

            var userName = login.UserName;




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

                //Queries queries = new Queries();


                //queries.GetUserId(login.UserName);

                LoginModel user = Queries.GetUser(userName);


                bool isValidated = HashSalt.VerifyPassword(password, user.Hash, user.Salt);

                if (isValidated)
                {
                    MessageBox.Show("Login Sucessfull!");
                    return true;
                }
                else
                {
                    MessageBox.Show("Wrong Password!");
                    return false;
                }
               
            }

            //    try
            //    {

            //        SqlConnection conn = new SqlConnection(Connections.ConnectionString);


            //        string sql = "SELECT UserId FROM dbo.Users WHERE UserLogin=@UserLogin;



            //        conn.Open();

            //        SqlCommand cmd = new SqlCommand(sql, conn);
            //        cmd.Parameters.Add("@UserLogin", SqlDbType.VarChar).Value = login.UserName;
            //        cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = login.UserPassword;



            //        using (SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            // if the result set is not NULL
            //            if (reader.HasRows)
            //            {
            //                while (reader.Read())
            //                {

            //                    var UserId = reader.GetInt32(0);
            //                    login.UserId = UserId;
            //                    login.LoggedIn = true;
            //                    CurrentLogin = login;

            //                    MessageBox.Show($"Login Succesfull!");
                            
            //                    return true;
            //                }

                            
            //                // update the existing value + the value from the text file
            //            }



            //            else 
            //            {
            //                MessageBox.Show("Wrong Data!");
            //                return false;
            //            }
                                                
            //        }
                                                           
            //    }

            //    catch (SqlException ex)
            //    {
            //        string errorMessage = $"Error: {ex}";
            //        MessageBox.Show(errorMessage);
            //        return false;
            //    }


            //    return false;
                
            //}

            
        }
           
        public void CreateAccount()
        {
            IsCheckedCreateAccount = !IsCheckedCreateAccount;

            if (IsCheckedCreateAccount)
            {
                CurrentPageViewModel = CreateAccountVM;
            }
            else
            {
                CurrentPageViewModel = null;
            }
        }

        public void ForgotPassword()
        {
            IsCheckedForgotPassword = !IsCheckedForgotPassword;
         
            if (IsCheckedForgotPassword)
            {
                CurrentPageViewModel = ForgotPasswordVM;
            }
            else
            {
                CurrentPageViewModel = null;
            }
        }

        #endregion
    }
}
