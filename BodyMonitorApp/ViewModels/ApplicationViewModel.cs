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
        #region fields

        private ICommand _changePageCommand;
        private ICommand _logoutUserCommand;
        private IPageViewModel _currentPageViewModel;                
        private IPageViewModel _overlayViewModel;                
        private List<IPageViewModel> _pageViewModels;             
        private List<IPageViewModel> _userPageViewModels;
        private Visibility _buttonVisiblity = Visibility.Hidden;      

        #endregion     

        #region Properties / Commands
        
        public HomeViewModel HomeVM { get; set; }

        public LoginViewModel LoginVM { get; set; }

        public AboutViewModel AboutVM { get; set; }

        public LoggedInViewModel LoggedInVM { get; set; } 

        public ProfileInfoViewModel ProfileInfoVM { get; set; }

        public ProgressViewModel ProgressVM { get; set; }

        public WorkoutViewModel WorkoutVM { get; set; }

        public ChartsViewModel ChartsVM { get; set; }      

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

        public IPageViewModel OverlayViewModel
        {
            get
            {
                return _overlayViewModel;
            }
            set
            {
                if (_overlayViewModel != value)
                {
                    _overlayViewModel = value;
                    OnPropertyChanged("OverlayViewModel");
                }
            }
        }

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

        public ICommand LoginUserCommand { get; set; }
              
        public ICommand CreateAccountViewCommand { get; set; }

        public ICommand ForgotPasswordViewCommand { get; set; }
                     
        public ICommand EditDataCommand { get; set; }

        public ICommand UpdateDataCommand { get; set; }

        public ICommand AddRecordCommand { get; set; }
  
        public ICommand EditRecordCommand { get; set; }

        public ICommand UpdateRecordCommand { get; set; }

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

        public ApplicationViewModel()
        {
            HomeVM = new HomeViewModel();
            LoginVM = new LoginViewModel();
            AboutVM = new AboutViewModel();
            LoggedInVM = new LoggedInViewModel();
            WorkoutVM = new WorkoutViewModel();           

            // Add available pages
            PageViewModels.Add(HomeVM);     
            PageViewModels.Add(AboutVM);

            // Add user pages - displayed when logged on
            UserPageViewModels.Add(WorkoutVM);

            ProfileInfoVM = new ProfileInfoViewModel();
            ProgressVM = new ProgressViewModel();
            ChartsVM = new ChartsViewModel();

            UserPageViewModels.Add(ProfileInfoVM);
            UserPageViewModels.Add(ProgressVM);
            UserPageViewModels.Add(ChartsVM);

            // Set starting page
            OverlayViewModel = LoginVM;
            CurrentPageViewModel = PageViewModels[0];

            LoginUserCommand = new RelayCommand((p) => LoginUserTool(p));       
            CreateAccountViewCommand = new RelayCommand((p) => CreateAccountView());
            ForgotPasswordViewCommand = new RelayCommand((p) => ForgotPasswordView());                     
            EditDataCommand = new RelayCommand((p) => EditProfileData());           
            UpdateDataCommand = new RelayCommand((p) => UpdateProfileData());
            EditRecordCommand = new RelayCommand((p) => EditRecord());
            UpdateRecordCommand = new RelayCommand((p) => UpdateRecord());
            AddRecordCommand = new RelayCommand((p) => AddRecord());

            HomeVM.Visibility = Visibility.Visible;
            LoginVM.Visibility = Visibility.Visible;
            AboutVM.Visibility = Visibility.Visible;
        }

        #region Methods

        //changing the page
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }
                 
        public void ForgotPasswordView()
        {
            LoginVM.ForgotPassword();
        }
           
        public void CreateAccountView()
        {      
            LoginVM.CreateAccount();
        }

        public void EditProfileData()
        {
            ProfileInfoVM.EditData();
        }
                    
        public void UpdateProfileData()
        {
            ProfileInfoVM.UpdateData();
            UpdateDashboard();
        }

        public void EditRecord()
        {
            ProgressVM.EditData();
            UpdateChart();
        }

        public void AddRecord()
        {
            ProgressVM.AddRecord();
            UpdateChart();
        }

        public void UpdateRecord()
        {
            ProgressVM.UpdateRecord();
            UpdateDashboard();
            UpdateChart();
        }

        public void UpdateDashboard()
        {
            HomeVM.PopulateDashboard();
        }

        public void UpdateChart()
        {
            ChartsVM.SetUserValues(LoginVM.CurrentLogin.UserId);
        }
         
        public void LoginUserTool(object obj)
        {
            PasswordBox pwdBox = obj as PasswordBox;
            var password = pwdBox.Password;

            //validation of user credentials


            var loginModel = LoginVM.LoginUser(password);

            string userName = loginModel.UserName;
           
            if (loginModel.IsValidated)
            {
                int userId = loginModel.UserId;
                OverlayViewModel = null;
                             
                //sets current logged user as name on dashboard page
                HomeVM.UserName = userName;               
                UpdateDashboard();

                CurrentPageViewModel = HomeVM;

                ButtonVisibility = Visibility.Visible;
                SetUserPageVisibility(loginModel.IsValidated);                                             
                SendUserId(userId);
            }                       
        }
        
        /// <summary>
        /// Sets visibility of pages that can be displayed when user is logged in or out
        /// </summary>
        /// <param name="isTrue"></param>
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

        /// <summary>
        /// Sends current userId to other pages - profile info, progress and charts
        /// </summary>
        /// <param name="val"></param>
        public void SendUserId(int userId)
        {
            //user id to profile info
            ProfileInfoVM.SetUserValues(userId);

            //user id to progress page
            ProgressVM.SetUserValues(userId);

            //user id to charts page
            ChartsVM.SetUserValues(userId);
        }

        /// <summary>
        /// loging out of user, resets values of login/password and hides other pages that are displayed when user is logged in
        /// </summary>
        public void LogoutUser()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure to logout?", "Logout", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                SetUserPageVisibility(false);
                              
                LoginVM.UserLogin = null;            
                OverlayViewModel = LoginVM;             
                CurrentPageViewModel = PageViewModels[0];
                ButtonVisibility = Visibility.Hidden;
            }

        }
        #endregion
    }
}
