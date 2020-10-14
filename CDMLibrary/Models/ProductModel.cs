using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Discription { get; set; }
        public decimal RetailPrice { get; set; }
        public int QuantityInStock { get; set; }
        public string CreatedDate { get; set; }
        public string LastModified { get; set; }
        public bool IsTaxable { get; set; }
    }
}
