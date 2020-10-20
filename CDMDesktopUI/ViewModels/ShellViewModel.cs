using Caliburn.Micro;
using CDMDesktopUI.EventModels;
using CDMDesktopUI.Library.API;
using CDMDesktopUI.Library.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CDMDesktopUI.ViewModels
{
    public class ShellViewModel: Conductor<Object>, IHandle<LogOnEvent>
    {
        //private LoginViewModel _loginVM;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggedInUserModel _user;
        private readonly IAPIHelper _apiHelper;

        public ShellViewModel(
            IEventAggregator eventAggregator,
            ILoggedInUserModel user,
            IAPIHelper apiHelper)
        {

            _user = user;
            _apiHelper = apiHelper;
            _eventAggregator = eventAggregator;

            _eventAggregator.SubscribeOnPublishedThread(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>());
        }
        public async void ExitApplication()
        {
            // ExitApplication();
           await TryCloseAsync();
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
        public async void UserManagement()
        {
           await ActivateItemAsync(IoC.Get<UserDisplayViewModel>());
        }
        public async void LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogoutUser();
           await ActivateItemAsync(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
           await ActivateItemAsync(IoC.Get<SalesViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
