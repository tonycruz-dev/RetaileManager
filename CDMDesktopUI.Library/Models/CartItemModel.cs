using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.Library.Models
{
    public class CartItemModel
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }


        public string  DisplayText
        {
            get
            { 
                return $"{ Product.ProductName }  Qty ({Quantity})"; 
            
            }

        }

    }
}
