﻿using Helpers;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BodyMonitorApp
{
    public class ApplicationViewModel : ObservableObject
    {
        #region Fields

        private ICommand _changePageCommand;

        private ICommand _logoutUserCommand;

        private IPageViewModel _currentPageViewModel;

        //lista ze stronami przed zalogowaniem
        private List<IPageViewModel> _pageViewModels;

        //lista ze stronami po zalogowaniu
        private List<IPageViewModel> _userPageViewModels;

        private Visibility _buttonVisiblity = Visibility.Hidden;



        #endregion


        public ApplicationViewModel()
        {

            HomeVM = new HomeViewModel();
            LoginVM = new LoginViewModel();
            AboutVM = new AboutViewModel();


            LoggedInVM = new LoggedInViewModel();
            ForgotPasswordVM = new ForgotPasswordViewModel();
            CreateAccountVM = new CreateAccountViewModel();

            ProfileInfoVM = new ProfileInfoViewModel();
            ProgressVM = new ProgressViewModel();
            WorkoutVM = new WorkoutViewModel();
            ChartsVM = new ChartsViewModel();

            // Add available pages
            PageViewModels.Add(HomeVM);
            PageViewModels.Add(LoginVM);
            PageViewModels.Add(AboutVM);

            // PageViewModels.Add(new ProductsViewModel());

            UserPageViewModels.Add(ProfileInfoVM);
            UserPageViewModels.Add(ProgressVM);
            UserPageViewModels.Add(WorkoutVM);
            UserPageViewModels.Add(ChartsVM);

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];


            LoginUserCommand = new RelayCommand((p) => LoginUserTool());
            ForgotPasswordSwitchPageCommand = new RelayCommand((p) => ForgotPasswordSwitchPage());
            CreateAccountSwitchPageCommand = new RelayCommand((p) => AccountCreationSwitchPage());


            
            HomeVM.Visibility = Visibility.Visible;
            LoginVM.Visibility = Visibility.Visible;
            AboutVM.Visibility = Visibility.Visible;

        }

        #region Properties / Commands

        
        public HomeViewModel HomeVM { get; set; }

        public LoginViewModel LoginVM { get; set; }


        public AboutViewModel AboutVM { get; set; }

        public LoggedInViewModel LoggedInVM { get; set; }
        public CreateAccountViewModel CreateAccountVM { get; set; }

        public ForgotPasswordViewModel ForgotPasswordVM { get; set; }

        public ProfileInfoViewModel ProfileInfoVM { get; set; }


        public ProgressViewModel ProgressVM { get; set; }
        public WorkoutViewModel WorkoutVM { get; set; }

        public ChartsViewModel ChartsVM { get; set; }

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        public ICommand LogoutUserCommand
        {
            get
            {
                if (_logoutUserCommand == null)
                {
                    _logoutUserCommand = new RelayCommand(
                        p => LogoutUser());
                       
                }

                return _logoutUserCommand;
            }
        }


        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public List<IPageViewModel> UserPageViewModels
        {
            get
            {
                if (_userPageViewModels == null)
                    _userPageViewModels = new List<IPageViewModel>();

                return _userPageViewModels;
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


        public ICommand LoginUserCommand { get; set; }

        public ICommand ForgotPasswordSwitchPageCommand { get; set; }
        public ICommand CreateAccountSwitchPageCommand { get; set; }


        public Visibility ButtonVisibility
        {
            get
            {
                return _buttonVisiblity;
            }
            set
            {
                _buttonVisiblity = value;

                OnPropertyChanged("ButtonVisibility");
            }
        }

        


        #endregion

        #region Methods

        //zmiana strony
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }


        //zmiana strony - robienie konta
        public void AccountCreationSwitchPage()
        {

            CurrentPageViewModel = CreateAccountVM;

        }


        //zmiana strony - zapomniane haslo
        public void ForgotPasswordSwitchPage()
        {


            CurrentPageViewModel = ForgotPasswordVM;
        }

        //logowanie użytkownika    
        public void LoginUserTool()
        {
            //sprawdzenie czy login się zgadza
            bool isValidated = LoginVM.LoginUser();
            int userId = LoginVM.CurrentLogin.UserId;
            

            if (isValidated)
            {

                CurrentPageViewModel = LoggedInVM;
                               
                ButtonVisibility = Visibility.Visible;
                SetUserPageVisibility(isValidated);

                //wyślij id do Profile info vm, aby wybrać z bazy odpowiednie wartości
                SetAccountInfo(userId);
            }
                       
        }
        
        public void SetUserPageVisibility(bool isTrue)
        {
            if(isTrue)
            {
                ProfileInfoVM.Visibility = Visibility.Visible;
                ProgressVM.Visibility = Visibility.Visible;
                WorkoutVM.Visibility = Visibility.Visible;
                ChartsVM.Visibility = Visibility.Visible;
                LoginVM.Visibility = Visibility.Hidden;

            }
            else
            {
                ProfileInfoVM.Visibility = Visibility.Hidden;
                ProgressVM.Visibility = Visibility.Hidden;
                WorkoutVM.Visibility = Visibility.Hidden;
                ChartsVM.Visibility = Visibility.Hidden;
                LoginVM.Visibility = Visibility.Visible;
            }
        }
        public void SetAccountInfo(int val)
        {
            //user id to profile info
            ProfileInfoVM.SetUserValues(val);

            //user id to progress page
            ProgressVM.SetUserValues(val);

            //user id to charts page
            ChartsVM.SetUserValues(val);
        }

        public void LogoutUser()
        {


            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure to logout?", "Logout", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                SetUserPageVisibility(false);

                LoginVM.UserPassword = null;
                LoginVM.UserLogin = null;

                CurrentPageViewModel = PageViewModels[0];
                ButtonVisibility = Visibility.Hidden;

            }



        }

        #endregion



    }
}
