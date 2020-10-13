using Caliburn.Micro;
using CDMDesktopUI.Library.API;
using CDMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.ViewModels
{
    public class SalesViewModel: Screen
    {
        private readonly IProductEndPoint _productEndPoint;

        public SalesViewModel(IProductEndPoint productEndPoint)
        {
            _productEndPoint = productEndPoint;
         }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }
        private async Task LoadProducts()
        {
            Products = new BindingList<ProductModel>(await _productEndPoint.GetAll());
        }
        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);

            }
        }

        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity;
        

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set 
            { 
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }
        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                if (SelectedProduct?.QuantityInStock >=  ItemQuantity && ItemQuantity > 0)
                {
                    output = true;
                }
                return output;
            }

        }

        public string SubTotal
        {
            get
            {
                decimal subtotal = 0;
                foreach (var item in Cart)
                {
                    subtotal += (item.Product.RetailPrice * item.Quantity);
                }
                return subtotal.ToString("C");
            }

        }
        public string Tax
        {
            get
            {

                return "£0.00";
            }

        }
        public string Total
        {
            get
            {

                return "£0.00";
            }

        }
        public void AddToCart()
        {
            CartItemModel item = new CartItemModel
            {
                Product = SelectedProduct,
                Quantity = ItemQuantity
            };
            Cart.Add(item);
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);

        }
        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
                return output;
            }

        }
        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                return output;
            }

        }
        public void CheckOut()
        {

        }


    }
}
