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
        private string _name = "Login";
        private bool _isCheckedCreateAccount = false;
        private bool _isCheckedForgotPassword = false;
        private LoginModel _currentLogin;        
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
            CurrentLogin = new LoginModel();
        }

        #region methods

        public LoginModel LoginUser(string password)
        {
            LoginModel login = new LoginModel
            {
                UserName = UserLogin,
                UserPassword = password,
                IsValidated = false
            };
                    
            if (string.IsNullOrWhiteSpace(login.UserName))
            {
                MessageBox.Show("Missing Login!");            
            }

            else if (string.IsNullOrEmpty(login.UserPassword))
            {
                MessageBox.Show("Missing Password!");              
            }

            else
            {             
                LoginModel user = Queries.GetUser(login.UserName);

                bool isValidated = HashSalt.VerifyPassword(password, user.Hash, user.Salt);

                if (isValidated)
                {
                    login.UserId = user.UserId;
                    CurrentLogin.UserId = login.UserId;
                    login.IsValidated = true;
                    MessageBox.Show("Login Sucessfull!");                  
                }

                else
                {
                    login.IsValidated = false;
                    MessageBox.Show("Wrong Password!");                  
                }
               
            }

            return login;
        }
           
        public void CreateAccount()
        {
            IsCheckedCreateAccount = !IsCheckedCreateAccount;

            if (IsCheckedCreateAccount)
            {
                CreateAccountVM = new CreateAccountViewModel();
                CurrentPageViewModel = CreateAccountVM;
            }
            else
            {
                CreateAccountVM = null;
                CurrentPageViewModel = null;
            }
        }

        public void ForgotPassword()
        {
            IsCheckedForgotPassword = !IsCheckedForgotPassword;
         
            if (IsCheckedForgotPassword)
            {
                ForgotPasswordVM = new ForgotPasswordViewModel();
                CurrentPageViewModel = ForgotPasswordVM;
            }

            else
            {
                ForgotPasswordVM = null;
                CurrentPageViewModel = null;
            }
        }

        #endregion
    }
}
