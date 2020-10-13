using Caliburn.Micro;
using CDMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.ViewModels
{
    public class ShellViewModel: Conductor<Object>, IHandle<LogOnEvent>
    {
        //private LoginViewModel _loginVM;
        private readonly IEventAggregator _eventAggregator;
        private readonly SalesViewModel _salesVM;
        private readonly SimpleContainer _container;

        public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesVM, SimpleContainer container)
        {
           // _loginVM = loginVM;
            _salesVM = salesVM;
            _eventAggregator = eventAggregator;
            _container = container;
            _eventAggregator.Subscribe(this);
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
           // _loginVM = _container.GetInstance<LoginViewModel>();
        }
    }
}
