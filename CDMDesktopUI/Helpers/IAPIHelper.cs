using CDMDesktopUI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace CDMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; set; }

        Task<AuthenticatedUser> Authenticate(string userName, string password);
    }
}