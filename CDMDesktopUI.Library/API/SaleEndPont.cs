using CDMLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.API
{
    public class SaleEndPont : ISaleEndPont
    {
        private readonly IAPIHelper _apiHelper;

        public SaleEndPont(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }


        public async Task<SalesModel> AddSales(SalesModel sales)
        {

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale/AddSale", sales))
            {

                if (response.IsSuccessStatusCode)
                {

                    //var result = await response.Content.ReadAsAsync<List<ProductModel>>();
                    return sales;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
