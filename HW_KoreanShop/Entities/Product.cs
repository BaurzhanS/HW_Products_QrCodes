using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_KoreanShop.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public string Currency { get; set; }
        private decimal CostInTenge_;
        public decimal CostInTenge
        {
            set
            {
                CostInTenge_ = (CurrencyConverter.CurrencyConversion(Cost, Currency))*Cost;
            }
            get
            {
                return CostInTenge_ = CurrencyConverter.CurrencyConversion(Cost, Currency)*Cost;
            }

        }
    }
    
}
