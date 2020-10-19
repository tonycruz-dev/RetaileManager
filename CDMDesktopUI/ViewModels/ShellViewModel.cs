using Caliburn.Micro;
using CDMDesktopUI.EventModels;
using CDMDesktopUI.Library.API;
using CDMDesktopUI.Library.Models;
using System;

namespace CDMDesktopUI.ViewModels
{
    public class ShellViewModel: Conductor<Object>, IHandle<LogOnEvent>
    {
        //private LoginViewModel _loginVM;
        private readonly IEventAggregator _eventAggregator;
        private readonly SalesViewModel _salesVM;
        private readonly ILoggedInUserModel _user;
        private readonly IAPIHelper _apiHelper;

        public ShellViewModel(
            IEventAggregator eventAggregator, 
            SalesViewModel salesVM, 
            ILoggedInUserModel user,
            IAPIHelper apiHelper)
        {
           // _loginVM = loginVM;
            _salesVM = salesVM;
            _user = user;
            _apiHelper = apiHelper;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
           // _loginVM = _container.GetInstance<LoginViewModel>();
        }
        public void ExitApplication()
        {
            // ExitApplication();
            TryClose();
        }
        public bool IsLoggedIn
        {
            get
            {
                bool output = false;
                if (string.IsNullOrWhiteSpace(_user.Token) == false)
                {
                    output = true;
                }
                return output;
            }
        }
        public void UserManagement()
        {
            ActivateItem(IoC.Get<UserDisplayViewModel>());
        }
        public void LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogoutUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
        
    }
}
