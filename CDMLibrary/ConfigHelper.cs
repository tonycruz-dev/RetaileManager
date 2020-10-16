using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMLibrary
{
    public static class ConfigHelper
    {
        public static decimal GetTax()
        {

            string taxRate = ConfigurationManager.AppSettings["taxRate"];
            bool IsvalidTaxRate = decimal.TryParse(taxRate, out decimal output);
            if (IsvalidTaxRate == false)
            {
                throw new ConfigurationErrorsException("the tax rate is not set properly");
            }
            return output;

        }
    }
}
