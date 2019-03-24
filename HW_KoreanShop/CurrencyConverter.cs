using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HW_KoreanShop
{
    public static class CurrencyConverter
    {
        public static decimal CurrencyConversion(decimal sum, string fromCurrency, string toCurrency="KZT")
        {
            string urlPattern = $"https://free.currencyconverterapi.com/api/v6/convert?q={fromCurrency}_{toCurrency}&compact=ultra&apiKey=43fb798ff52eef940fd5";
            string url = string.Format(urlPattern, fromCurrency, toCurrency);

            using (var wc = new WebClient())
            {
                var response = wc.DownloadString(url);

                var dicRate = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(response);

                decimal rate = dicRate[$"{fromCurrency}_{toCurrency}"];
                return rate;
            }
        }
    }
}
