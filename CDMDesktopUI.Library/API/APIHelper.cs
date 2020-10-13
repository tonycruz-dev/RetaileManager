﻿using CDMDesktopUI.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.API
{
    public class APIHelper: IAPIHelper
    {
        private ILoggedInUserModel _loggedInUserModel;

        private HttpClient ApiClient { get; set; }
        public APIHelper(ILoggedInUserModel loggedInUserModel)
        {
            InitializeClient();
            _loggedInUserModel = loggedInUserModel;
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];
            ApiClient = new HttpClient
            {
                BaseAddress = new Uri(api)
            };
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<AuthenticatedUser> Authenticate(string userName, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
              new KeyValuePair<string, string>("grant_type", "password"),
              new KeyValuePair<string, string>("username", userName),
              new KeyValuePair<string, string>("password", password),
            });
            using (HttpResponseMessage response = await ApiClient.PostAsync("/token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
        public async Task GetLoggedInUserInfo(string token)
        {
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using (HttpResponseMessage response = await ApiClient.GetAsync("api/User/UserById"))
            {

                if (response.IsSuccessStatusCode)
                {
                  
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedInUserModel.CreatedDate = result.CreatedDate;
                    _loggedInUserModel.Email = result.Email;
                    _loggedInUserModel.LastName = result.LastName;
                    _loggedInUserModel.FirstName = result.FirstName;
                    _loggedInUserModel.Token = token;
                    _loggedInUserModel.Id = result.Id;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
