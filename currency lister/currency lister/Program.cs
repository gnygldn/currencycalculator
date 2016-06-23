using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;

namespace CurrencyCalculator
{
    public class JsnList
    {
        public Currencypair[] currencyPairs { get; set; }
    }

    public class Currencypair
    {
        public string baseCurrency { get; set; }
        public string counterCurrency { get; set; }
        public float rate { get; set; }
    }

    class Calculator
    {
        
        private static string before;
        private static string after;
        private static string amount;
        private static double amount1;
        private static double currencyRate;
        private static string myInfo;
        private static JsnList myInfo1;
        private static string myUrl;

        static void Main(string[] args)
        {
            Calculator calculate = new Calculator();
            
            calculate.GetCurrentCurreny("enter your currency ");
            calculate.GetNextCurreny("enter the currency you want to get");
            calculate.GetAmount("enter the amount");
            calculate.GetUrl();
            calculate.GetInfo();
            calculate.TranslateInfo();
            calculate.ChangeAmount();
            currencyRate = calculate.GetRate();
            calculate.Result();
            Console.ReadKey();
            
            
        }

        private void GetAmount(string p)
        {
            Console.WriteLine(p);
            amount = Console.ReadLine();
        }

        private void GetNextCurreny(string p)
        {
            Console.WriteLine(p);
            after = Console.ReadLine();
        }

        private void GetCurrentCurreny(string p)
        {
            Console.WriteLine(p);
            before = Console.ReadLine();
        }

        private void TranslateInfo()
        {
            myInfo1 = (JsnList)JsonConvert.DeserializeObject(myInfo, typeof(JsnList));
        }

        private void Result()
        {

            Console.WriteLine("your currency is {0}", amount1 * currencyRate);
            Console.ReadKey();
        }

        public void ChangeAmount()
        {
            amount1 = Convert.ToDouble(amount);
        }


        public double GetRate()
        {
            foreach(var pairs in myInfo1.currencyPairs)
            {
                if (after == pairs.counterCurrency)
                {
                    return pairs.rate;
                }
            }
            return 0;
        }

        public void GetUrl()
        {
           myUrl = ConfigurationManager.AppSettings["url"] + "/v1/currency/GetExchangeRates?basecurrency=" + before + "&access_token=" + ConfigurationManager.AppSettings["access_token"];
        }
        public void GetInfo()
        {
            WebClient client = new WebClient();
            myInfo = client.DownloadString(myUrl);
        }
    }
}
