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


        private string _userLogin;
        private string _userPassword;
        private string _userPasswordConfirmation;
        private DateTime _userAge;
        private int _userHeight;
        private string _userName;
        private string _userMail;
        private string _userGender;

       
        
        private ICommand _createAccountCommand;



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


        public void CreateAccount()
        {

            AccountModel account = new AccountModel
            {
                UserLogin = UserLogin,
                UserPassword = UserPassword,
                UserAge = UserAge,
                UserHeight = UserHeight,
                UserName = UserName,
                UserMail = UserMail,
                UserGender = UserGender
            };

            try
            {

                DateTime currentTime = DateTime.Now;

               
                // get connection string from Connections Helper Class
                SqlConnection conn = new SqlConnection(Connections.ConnectionString);

                string sql = "INSERT INTO dbo.Users (UserLogin,UserPassword) values (@UserLogin,@UserPassword);" +
                    "INSERT INTO dbo.UserData(id,UserHeight,UserName,UserAge,UserMail,UserGender,AccountCreated) values ((SELECT SCOPE_IDENTITY()),@UserHeight,@UserName,@UserAge,@UserMail,@UserGender,@AccountCreated)";


                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@UserLogin", SqlDbType.VarChar).Value = account.UserLogin;
                cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = account.UserPassword;
                cmd.Parameters.Add("@UserAge", SqlDbType.Date).Value = account.UserAge.Date;
                cmd.Parameters.Add("@UserHeight", SqlDbType.Int).Value = account.UserHeight;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = account.UserName;
                cmd.Parameters.Add("@UserMail", SqlDbType.VarChar).Value = account.UserMail;
                cmd.Parameters.Add("@UserGender", SqlDbType.Char).Value = account.UserGender;
                cmd.Parameters.Add("@AccountCreated", SqlDbType.DateTime).Value = currentTime;


                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Account Created!");
                }


            }

            catch (SqlException ex)
            {
                string errorMessage = $"Error: {ex}";
                MessageBox.Show(errorMessage);
            }

        }




    }
    
}
