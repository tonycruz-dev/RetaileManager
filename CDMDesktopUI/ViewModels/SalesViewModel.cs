using AutoMapper;
using Caliburn.Micro;
using CDMDesktopUI.Library.API;
using CDMDesktopUI.Library.Models;
using CDMDesktopUI.Models;
using CDMHelper;
using CDMLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CDMDesktopUI.ViewModels
{
    public class SalesViewModel: Screen
    {
        private readonly IProductEndPoint _productEndPoint;
        private readonly IConfigHelper _configHelper;
        private readonly ISaleEndPont _saleEndPont;
        private readonly IMapper _mapper;
        private readonly StatusInfoViewModel _status;
        private readonly IWindowManager _windowManager;

        public SalesViewModel(
            IProductEndPoint productEndPoint, 
            IConfigHelper configHelper, 
            ISaleEndPont saleEndPont,
            IMapper mapper, StatusInfoViewModel status,
            IWindowManager windowManager)
        {
            _productEndPoint = productEndPoint;
            _configHelper = configHelper;
            _saleEndPont = saleEndPont;
            _mapper = mapper;
            _status = status;
            _windowManager = windowManager;
        }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadProducts();
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
        private async Task LoadProducts()
        {
            var productList = await _productEndPoint.GetAll();
            Products = new BindingList<ProductDisplayModel>(_mapper.Map<List<ProductDisplayModel>>(productList));
        }
        private async Task ResetViewModel()
        {
            Cart = new BindingList<CartItemDisplayModel>();
            await LoadProducts();

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanRemoveFromCart);
            Products.ResetBindings();
            Cart.ResetBindings();
        }
        private BindingList<ProductDisplayModel> _products;

        public BindingList<ProductDisplayModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        private ProductDisplayModel _selectedProduct;

        public ProductDisplayModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);

            }
        }

        private CartItemDisplayModel _selectedCartItem;

        public CartItemDisplayModel SelectedCartItem
        {
            get { return _selectedCartItem; }
            set
            {
                _selectedCartItem = value;
                NotifyOfPropertyChange(() => SelectedCartItem);
                NotifyOfPropertyChange(() => CanRemoveFromCart);

            }
        }

        private BindingList<CartItemDisplayModel> _cart = new BindingList<CartItemDisplayModel>();

        public BindingList<CartItemDisplayModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantity = 1;
        

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
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >=  ItemQuantity)
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
                //decimal subtotal = CalculateSubtotal();
                //foreach (var item in Cart)
                //{
                //    subtotal += (item.Product.RetailPrice * item.Quantity);
                //}
                return CalculateSubtotal().ToString("C");
            }

        }
        private decimal CalculateSubtotal()
        {
            decimal subtotal = 0;
            foreach (var item in Cart)
            {
                subtotal += (item.Product.RetailPrice * item.Quantity);
            }
            return subtotal;
        }
        public string Tax
        {
            get
            {
                //decimal taxAmount = 0;
                //decimal taxRate = _configHelper.GetTax();

                //foreach (var item in Cart)
                //{
                //    if(item.Product.IsTaxable)
                //    {
                //        taxAmount += (item.Product.RetailPrice * item.Quantity * taxRate);
                //    }
                    
                //}
                return CalculateTax().ToString("C");

            }

        }
        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTax()/100;
            taxAmount = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.Quantity * taxRate);

            return taxAmount;
        }
        public string Total
        {
            get
            {
                decimal total = CalculateSubtotal() + CalculateTax();
                return total.ToString("C");
            }

        }
        public void AddToCart()
        {
            CartItemDisplayModel existingItem = Cart.FirstOrDefault(c => c.Product == SelectedProduct);

            if (existingItem != null)
            {
                existingItem.Quantity += ItemQuantity;
                //Cart.Remove(existingItem);
                //Cart.Add(existingItem);
            }
            else 
            {
                CartItemDisplayModel item = new CartItemDisplayModel
                {
                    Product = SelectedProduct,
                    Quantity = ItemQuantity
                };
                Cart.Add(item);
                
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            //NotifyOfPropertyChange(() => Cart);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanRemoveFromCart);
            Products.ResetBindings();
            Cart.ResetBindings();
            //NotifyOfPropertyChange(() => SelectedProduct);
            //NotifyOfPropertyChange(() => Products);


        }
        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
                if (SelectedCartItem != null && SelectedCartItem?.Quantity > 0)
                {
                    output = true;
                }
                return output;
            }

        }
        public void RemoveFromCart()
        {
            if (SelectedCartItem.Quantity > 1)
            {
                SelectedCartItem.Quantity -= 1;
                SelectedCartItem.Product.QuantityInStock += 1;
            }
            else
            {
                SelectedCartItem.Product.QuantityInStock += 1;
                Cart.Remove(SelectedCartItem);
            }
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
            Products.ResetBindings();
            Cart.ResetBindings();
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                if (Cart.Count > 0)
                {
                    output = true;
                }
                return output;
            }

        }
        public async void CheckOut()
        {
            SalesModel sale =  new SalesModel();
            foreach (var item in Cart)
            {
                sale.SaleDetails.Add(new SaleDetailModel
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity
                });
            }
           await _saleEndPont.AddSales(sale);
            await ResetViewModel();
        }


    }
}
