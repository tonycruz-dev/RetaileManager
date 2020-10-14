using Caliburn.Micro;
using CDMDesktopUI.EventModels;
using System;

namespace CDMDesktopUI.ViewModels
{
    public class ShellViewModel: Conductor<Object>, IHandle<LogOnEvent>
    {
        //private LoginViewModel _loginVM;
        private readonly IEventAggregator _eventAggregator;
        private readonly SalesViewModel _salesVM;

        public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesVM)
        {
           // _loginVM = loginVM;
            _salesVM = salesVM;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
           // _loginVM = _container.GetInstance<LoginViewModel>();
        }
    }
}
