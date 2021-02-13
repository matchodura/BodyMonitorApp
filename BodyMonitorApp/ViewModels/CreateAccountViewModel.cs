using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace BodyMonitorApp
{

    public class CreateAccountViewModel : ObservableObject, IPageViewModel
    {

        #region fields

        private string _userLogin;
        private string _userPassword;
        private string _userPasswordConfirmation;
        private DateTime _userAge;
        private int _userHeight;
        private string _userName;
        private string _userMail;
        private string _userGender;             
        private ICommand _createAccountCommand;

        //new things

        private string _secretQuestion;
        private string _secretAnswer;

        #endregion fields

        #region properties/commands
        public string Name
        {
            get
            {
                return "robienie konta";
            }
            set {; }
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

        public string UserPasswordConfirmation
        {
            get { return _userPasswordConfirmation; }
            set
            {
                if (value != _userPasswordConfirmation)
                {
                    _userPasswordConfirmation = value;
                    OnPropertyChanged("UserPasswordConfirmation");
                }
            }
        }

        public DateTime UserAge
        {
            get { return _userAge; }
            set
            {
                if (value != _userAge)
                {
                    _userAge = value;
                    OnPropertyChanged("UserAge");
                }
            }
        }

        public int UserHeight
        {
            get { return _userHeight; }
            set
            {
                if (value != _userHeight)
                {
                    _userHeight = value;
                    OnPropertyChanged("UserHeight");
                }
            }
        }

        public string UserName
        {
            get { return _userName; }

            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");

                }
            }
        }

        public string UserMail
        {
            get { return _userMail; }
            set
            {
                if (value != _userMail)
                {
                    _userMail = value;
                    OnPropertyChanged("UserMail");
                }
            }
        }

        public string UserGender
        {
            get { return _userGender; }
            set
            {
                if (value != _userGender)
                {
                    _userGender = value;
                    OnPropertyChanged("UserGender");
                }
            }
        }

        //new things
        public string SecretQuestion
        {
            get { return _secretQuestion; }
            set
            {
                if (value != _secretQuestion)
                {
                    _secretQuestion = value;
                    OnPropertyChanged("SecretQuestion");
                }
            }
        }


        public string SecretAnswer
        {
            get { return _secretAnswer; }
            set
            {
                if (value != _secretAnswer)
                {
                    _secretAnswer = value;
                    OnPropertyChanged("SecretAnswer");
                }
            }
        }
        public ICommand CreateAccountCommand
        {
            get
            {
                if (_createAccountCommand == null)
                {
                    _createAccountCommand = new RelayCommand(
                        param => CreateAccount());

                }
                return _createAccountCommand;
            }
        }

        #endregion properties

        #region methods

        /// <summary>
        /// creates user account bassed on credentials provided in create account view
        /// </summary>
        public void CreateAccount()
        {
            Queries query = new Queries();

         
            AccountModel account = new AccountModel
            {
                UserLogin = UserLogin,
                UserPassword = UserPassword,
                UserAge = UserAge,
                UserHeight = UserHeight,
                UserName = UserName,
                UserMail = UserMail,
                UserGender = UserGender,
                SecretQuestion = SecretQuestion,
                SecretAnswer = SecretAnswer
            };

            query.CreateUserAccount(account);
                       
        }

        #endregion methods

    }

}
