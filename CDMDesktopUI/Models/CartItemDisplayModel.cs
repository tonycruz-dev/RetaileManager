using CDMHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMDesktopUI.Models
{
    public class CartItemDisplayModel: INotifyPropertyChanged
    {
        private int _quantity;

        public ProductDisplayModel Product { get; set; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                CallPropertyChange(nameof(Quantity));
                CallPropertyChange(nameof(DisplayValue));

            }
        }
      

        public string DisplayValue
        {
            get
            {
                return $"{ Product.ProductName }  Qty ({Quantity})";
            }
        }

        public string DisplayText
        {
            get
            {
                return $"{ Product.ProductName }  Qty ({Quantity})";

            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void CallPropertyChange(string proppertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(proppertyName)));
        }
    }
}
