using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMHelper
{
    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTax()
        {
            //double output = 0;
            string taxRate = ConfigurationManager.AppSettings["taxRate"];
            bool IsvalidTaxRate = Decimal.TryParse(taxRate, out decimal output);
            if (IsvalidTaxRate == false)
            {
                throw new ConfigurationErrorsException("the tax rate is not set properly");
            }
            return output;

        }
    }
}
