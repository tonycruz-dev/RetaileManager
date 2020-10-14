using CDMHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.Models
{
    public class CartItemModel : BindableBase
    {
        private int _quantity;

        public ProductModel Product { get; set; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value);
                
            }
        }
        private string _displayValue;

        public string DisplayValue
        {
            get 
            {
                return $"{ Product.ProductName }  Qty ({Quantity})";
            }
            set 
            {
               // string message = $"{ Product.ProductName }  Qty ({Quantity})";
                _displayValue = value;
                SetProperty(ref _displayValue, value);
            }
        }

        public string DisplayText
        {
            get
            {
                return $"{ Product.ProductName }  Qty ({Quantity})";

            }

        }

    }
}
