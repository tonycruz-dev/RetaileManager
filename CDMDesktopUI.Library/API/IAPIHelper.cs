using CDMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.API
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
        Task GetLoggedInUserInfo(string token);
        HttpClient ApiClient { get; }
    }
}
