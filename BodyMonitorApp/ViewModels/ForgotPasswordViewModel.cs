using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BodyMonitorApp
{
    public class ForgotPasswordViewModel : ObservableObject, IPageViewModel
    {

        private ICommand _getSecretQuestionCommand;
        private ICommand _changePasswordCommand;
        private ICommand _updatePasswordCommand;
        private string _secretQuestion;
        private string _secretAnswer;
        private string _secretAnswerConfirmation;

        private string _userLogin;
        private string _newUserPassword;

        private Visibility _changePassword = Visibility.Hidden;
        private Visibility _secretQuestionBox = Visibility.Hidden;

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
                        param => UpdatePassword());

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

        public string NewUserPassword
        {
            get { return _newUserPassword; }
            set
            {
                if (value != _newUserPassword)
                {
                    _newUserPassword = value;
                    OnPropertyChanged("NewUserPassword");
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


        public string SecretAnswerConfirmation
        {
            get { return _secretAnswerConfirmation; }
            set
            {
                if (value != _secretAnswerConfirmation)
                {
                    _secretAnswerConfirmation = value;
                    OnPropertyChanged("SecretAnswerConfirmation");
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

            if(SecretAnswerConfirmation == SecretAnswer)
            {
                MessageBox.Show("Gites!");
                ChangePasswordOption = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("smuteczek");
            }

        }


        public void UpdatePassword()
        {

            var queries = new Queries();
                        
            queries.UpdatePassword(UserLogin, NewUserPassword);
                 
          
        }
    }
}
