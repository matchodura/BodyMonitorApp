using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;


namespace BodyMonitorApp
{
    public class AccountModel : ObservableObject
    {

        #region Fields

        private string _userLogin;
        private string _userPassword;
        private DateTime _userAge;
        private int _userHeight;
        private string _userName;
        private string _userMail;
        private string _userGender;
        private DateTime _accountCreated;

        #endregion

        #region properties


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

        public DateTime AccountCreated
        {
            get { return _accountCreated; }
            set
            {
                if (value != _accountCreated)
                {
                    _accountCreated = value;
                    OnPropertyChanged("AccountCreated");
                }
            }
        }
        #endregion
    }
}
