using Caliburn.Micro;
using CDMDesktopUI.Library.API;
using CDMDesktopUI.Library.Models;
using CDMDesktopUI.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDMDesktopUI.EventModels;

namespace CDMDesktopUI.ViewModels
{
    public class LoginViewModel: Screen
    {
        private readonly IAPIHelper _apiHelper;
        private readonly IEventAggregator _eventAggregator;
        private string _userName;
        private string _password ;
        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator eventAggregator)
        {
            _apiHelper = apiHelper;
            _eventAggregator = eventAggregator;
        }
        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }
        
        public string Password
        {
            get { return _password; }
            set 
            {
                _password = value;
                NotifyOfPropertyChange(() => Password); 
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }
        // private bool _isErrorVisible;

        public bool IsErrorVisible
        {
            get 
            {
                bool output = false;
                if(ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output; 
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
                
            }
        }


        public bool CanLogIn
        {
            get
            {
                bool output = false;
                if(UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
           
          //  return output;
        }

        public async void  LogIn()
        {
            try
            {
                ErrorMessage = "";
                AuthenticatedUser user = await _apiHelper.Authenticate(UserName, Password);

                await _apiHelper.GetLoggedInUserInfo(user.Access_token);
                _eventAggregator.PublishOnUIThread(new LogOnEvent());
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                //throw;
                ErrorMessage = ex.Message;
            }
            
        }

    }
}
