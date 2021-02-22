using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyMonitorApp
{
    public class LoginModel : ObservableObject
    {
        #region Fields

        private string _hash;
        private string _salt;
        private string _userName;
        private string _userPassword;
        private bool _isValidated;
        private int _userId;
        private HashSalt _hashSalt;
       
        #endregion

        #region properties

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

        public string Hash
        {
            get { return _hash; }
            set
            {
                if (value != _hash)
                {
                    _hash = value;
                    OnPropertyChanged("Hash");
                }
            }

        }

        public string Salt
        {
            get { return _salt; }
            set
            {
                if (value != _salt)
                {
                    _salt = value;
                    OnPropertyChanged("Salt");
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

        public int UserId
        {
            get { return _userId; }
            set
            {
                if (value != _userId)
                {
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        public bool IsValidated
        {
            get { return _isValidated; }
            set
            {
                if (value != _isValidated)
                {
                    _isValidated = value;
                    OnPropertyChanged("IsValidated");
                }
            }
        }

        #endregion
    
    }
}