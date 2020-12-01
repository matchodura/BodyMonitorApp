using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace BodyMonitorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ApplicationView app = new ApplicationView();
            ApplicationViewModel context = new ApplicationViewModel();
            app.DataContext = context;
            app.Show();
        }

        //private void ApplicationStart(object sender, StartupEventArgs e)
        //{

        //    LoginView app = new LoginView();
        //    LoginViewModel context = new LoginViewModel();
        //    app.DataContext = context;
        //    app.Show();



        //    // Determine if login was successful
        //    if (app.DataContext is LoginViewModel loginVM)
        //    {
        //        MessageBox.Show("test");
        //        if(loginVM.CurrentLogin.LoggedIn)
        //        {
        //            MessageBox.Show("Essa");
        //        }

        //        //if (!loginVM.LoginSuccessful)
        //        //{
        //        //    // handle any cleanup and close/shutdown app
        //        //}
        //    }

        //    //show your MainWindow

        //    ApplicationView main = new ApplicationView();
        //    ApplicationViewModel contextMain = new ApplicationViewModel();
        //    main.DataContext = contextMain;
        //    main.Show();
        //}


    }
}
