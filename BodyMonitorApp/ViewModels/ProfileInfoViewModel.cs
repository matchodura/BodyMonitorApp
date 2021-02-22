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
        private int _userId;
        private bool _isChecked = false;
        private Visibility _visibility = Visibility.Hidden;
        private Visibility _textBlockVisibility = Visibility.Visible;
        private Visibility _editBoxVisibility = Visibility.Hidden;
     
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
        /// Displaying user profile data based on id
        /// </summary>
        /// <param name="userId"></param>
        public void SetUserValues(int userId)
        {
            UserId = userId;
            CurrentAccount = Queries.GetProfileData(UserId);   

        }

        /// <summary>
        /// display text editing of boxes
        /// </summary>
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
                    
        /// <summary>
        /// updates data in db with new values
        /// </summary>
        public void UpdateData()
        {
            var account = CurrentAccount;
            Queries.UpdateProfileData(account, UserId);

            TextBlockVisibility = Visibility.Visible;
            EditBoxVisibility = Visibility.Hidden;
            
        }

        #endregion methods
    }
}

