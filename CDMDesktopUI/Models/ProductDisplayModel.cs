using CDMHelper;
using System.ComponentModel;

namespace CDMDesktopUI.Models
{
    public class ProductDisplayModel : INotifyPropertyChanged
    {
        private int _quantityInStock;

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Discription { get; set; }
        public decimal RetailPrice { get; set; }
        public int QuantityInStock
        {
            get => _quantityInStock;
            set
            {
                _quantityInStock = value;
                CallPropertyChange(nameof(QuantityInStock));
            }
        }
        public bool IsTaxable { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void CallPropertyChange(string proppertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(proppertyName)));
        }
    }
}
