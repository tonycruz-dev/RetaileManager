using CDMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.API
{
    public class UserEndPoint : IUserEndPoint
    {
        private readonly IAPIHelper _apiHelper;

        public UserEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public async Task<List<AppUserModel>> GetUsers()
        {

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/User/GetAllUsers"))
            {

                if (response.IsSuccessStatusCode)
                {

                    var result = await response.Content.ReadAsAsync<List<AppUserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<Dictionary<string, string>> GetRoles()
        {

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/User/GetRoles"))
            {

                if (response.IsSuccessStatusCode)
                {

                    var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task AddUserToRole(string userId, string role)
        {
            var data = new { userId, role };
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/AddUserToRole", data))
            {

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task RemoveUserFromRole(string userId, string role)
        {
            var data = new { userId, role };
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/RemoveRole", data))
            {

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


    }
}
