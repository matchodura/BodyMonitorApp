using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BodyMonitorApp
{
    public class ForgotPasswordViewModel : ObservableObject, IPageViewModel
    {
        #region fields

        private ICommand _getSecretQuestionCommand;
        private ICommand _changePasswordCommand;
        private ICommand _updatePasswordCommand;
        private string _secretQuestion;
        private string _secretAnswer;
        private string _secretAnswerCheck;
        private string _userLogin;
        private string _newPassword;
        private string _newPasswordConfirmation;
        private Visibility _changePassword = Visibility.Hidden;
        private Visibility _secretQuestionBox = Visibility.Hidden;

        #endregion

        #region properties

        public ICommand GetSecretQuestionCommand
        {
            get
            {
                if (_getSecretQuestionCommand == null)
                {
                    _getSecretQuestionCommand = new RelayCommand(
                        param => GetSecretQuestion());

                }
                return _getSecretQuestionCommand;
            }
        }

        public ICommand ChangePasswordCommand
        {
            get
            {
                if (_changePasswordCommand == null)
                {
                    _changePasswordCommand = new RelayCommand(
                        param => ChangePassword());

                }
                return _changePasswordCommand;
            }
        }

        public ICommand UpdatePasswordCommand
        {
            get
            {
                if (_updatePasswordCommand == null)
                {
                    _updatePasswordCommand = new RelayCommand(
                        param => UpdatePassword(param));

                }
                return _updatePasswordCommand;
            }
        }

        public string Name
        {
            get{ return "Forgot password";} set {;}
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

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (value != _newPassword)
                {
                    _newPassword = value;
                    OnPropertyChanged("NewPassword");
                }
            }
        }

        public string NewPasswordConfirmation
        {
            get { return _newPasswordConfirmation; }
            set
            {
                if (value != _newPasswordConfirmation)
                {
                    _newPasswordConfirmation = value;
                    OnPropertyChanged("NewPasswordConfirmation");
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

        public string SecretAnswerCheck
        {
            get { return _secretAnswerCheck; }
            set
            {
                if (value != _secretAnswerCheck)
                {
                    _secretAnswerCheck = value;
                    OnPropertyChanged("SecretAnswerCheck");
                }
            }
        }
        public Visibility ChangePasswordOption
        {
            get
            {
                return _changePassword;
            }
            set
            {
                _changePassword = value;

                OnPropertyChanged("ChangePasswordOption");
            }
        }
        public Visibility SecretQuestionBox
        {
            get
            {
                return _secretQuestionBox;
            }
            set
            {
                _secretQuestionBox = value;

                OnPropertyChanged("SecretQuestionBox");
            }
        }

        #endregion

        #region methods

        public void GetSecretQuestion()
        {
            var queries = new Queries();
            var results = queries.GetSecretQuestion(UserLogin);
            SecretQuestion = results.Question;
            SecretAnswer = results.Answer;

            if(SecretQuestion != null)
            {
                SecretQuestionBox = Visibility.Visible;
            }         
        }
    
        public void ChangePassword()
        {
            if(SecretAnswerCheck== SecretAnswer)
            {
                MessageBox.Show("Gites!");
                ChangePasswordOption = Visibility.Visible;
            }

            else
            {
                MessageBox.Show("smuteczek");
            }
        }

        public void UpdatePassword(object obj)
        {
            //gets password box values from command parameters on view
            var pswBoxes = obj as List<object>;
            PasswordBox pwdBox = pswBoxes[0] as PasswordBox;
            PasswordBox pwdBoxRepeat = pswBoxes[1] as PasswordBox;
            var password = pwdBox.Password;
            var passwordRepeat = pwdBoxRepeat.Password;

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Missing password!");
            }

            else if (password != passwordRepeat)
            {
                MessageBox.Show("Passwords don't match!");
            }

            else
            {
                //hashing data
                var hashSalt = HashSalt.GenerateSaltedHash(64, password);
                var queries = new Queries();
                queries.UpdatePassword(UserLogin, password, hashSalt);
            }                 
                          
        }

        #endregion 
    }
}
