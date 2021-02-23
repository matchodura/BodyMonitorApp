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
        private DateTime _userBirthday;
        private int _userHeight;
        private string _userName;
        private string _userMail;
        private string _userGender;
        private DateTime _accountCreated;
        private HashSalt _hashSalt;              
        private string _secretQuestion;
        private string _secretAnswer;

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

        public DateTime UserBirthday
        {
            get { return _userBirthday; }
            set
            {
                if (value != _userBirthday)
                {
                    _userBirthday = value;
                    OnPropertyChanged("UserBirthday");
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

        public HashSalt HashSalt
        {
            get { return _hashSalt; }
            set
            {
                if (value != _hashSalt)
                {
                    _hashSalt = value;
                    OnPropertyChanged("HashSalt");
                }
            }
        }

        #endregion
    }
}
