using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyMonitorApp
{
    public class ForgotPasswordViewModel : ObservableObject, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Forgot password";
            }
            set {;}
        }


    
    }
}
