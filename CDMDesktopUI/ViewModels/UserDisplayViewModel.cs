using Caliburn.Micro;
using CDMDesktopUI.Library.API;
using CDMDesktopUI.Library.Models;
using System;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CDMDesktopUI.ViewModels
{
    public class UserDisplayViewModel: Screen
    {
        private readonly IUserEndPoint _userEndPoint;
        private readonly StatusInfoViewModel _status;
        private readonly IWindowManager _windowManager;

        public UserDisplayViewModel(IUserEndPoint userEndPoint,
                StatusInfoViewModel status,
            IWindowManager windowManager)
        {
            _userEndPoint = userEndPoint;
            _status = status;
            _windowManager = windowManager;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Access", "You do not have permission to interact with sales Form ");
                    _windowManager.ShowDialog(_status, null, settings);
                }
                else
                {
                    _status.UpdateMessage("Error Exception", ex.Message);
                    _windowManager.ShowDialog(_status, null, settings);
                }

                TryClose();
            }

        }
        private async Task LoadUsers()
        {
            var productList = await _userEndPoint.GetUsers();
            Users = new BindingList<AppUserModel>(productList);
        }
        private BindingList<AppUserModel> _users;

        public BindingList<AppUserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }
        private AppUserModel _selectedUser;

        public AppUserModel SelectedUser
        {
            get { return _selectedUser; }
            set 
            { 
                _selectedUser = value;
                SelectedUserName = SelectedUser.UserName;
                UserRole = new BindingList<string>(value.UserRols.Select(x => x.RoleName).ToList());
                LoadRole();
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }
        private string _selectedUserName;

        public string SelectedUserName
        {
            get { return _selectedUserName; }
            set 
            { 
                _selectedUserName = value;
                
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        private BindingList<string> _userRole =  new BindingList<string>();

        public BindingList<string> UserRole
        {
            get { return _userRole; }
            set 
            {
                _userRole =  value;
                NotifyOfPropertyChange(() => UserRole);
            }
        }

        private BindingList<string> _avilableRole = new BindingList<string>();
        public BindingList<string> AvilableRole
        {
            get { return _avilableRole; }
            set 
            { 
                _avilableRole = value;
                NotifyOfPropertyChange(() => AvilableRole);
            }
        }
        private BindingList<string> _seletedRoleToRemove = new BindingList<string>();
        public BindingList<string> SeletedRoleToRemove
        {
            get { return _seletedRoleToRemove; }
            set
            {
                _seletedRoleToRemove = value;
                NotifyOfPropertyChange(() => SeletedRoleToRemove);
            }
        }
        private string _selectedAvilableRole;
        public string SelectedAvilableRole
        {
            get { return _selectedAvilableRole; }
            set
            {
                _selectedAvilableRole = value;
                NotifyOfPropertyChange(() => SelectedAvilableRole);
            }
        }
        private string _selectedUserRole;
        public string SelectedUserRole
        {
            get { return _selectedUserRole; }
            set
            {
                _selectedUserRole = value;
                NotifyOfPropertyChange(() => SelectedUserRole);
            }
        }

        private async void LoadRole()
        {
            var roles = await _userEndPoint.GetRoles();
            foreach (var role in roles)
            {
                if (UserRole.IndexOf(role.Value) < 0)
                {
                    AvilableRole.Add(role.Value);
                }
            }
        }
        public async void AddSelectedRole()
        {
            await  _userEndPoint.AddUserToRole(SelectedUser.Id, SelectedAvilableRole);
            UserRole.Add(SelectedAvilableRole);
            AvilableRole.Remove(SelectedAvilableRole);
        }
        public async void RemoveSelectedItem()
        {
            await _userEndPoint.RemoveUserFromRole(SelectedUser.Id, SelectedUserRole);
            AvilableRole.Add(SelectedUserRole);
            UserRole.Remove(SelectedUserRole);
            
        }


    }
}
