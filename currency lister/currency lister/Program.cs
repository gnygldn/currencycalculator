using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace gny.CurrencyLister
{
    class Program
    {
        private static string changethis;
        private static string changeinto;
        private static JsnList mylist1;
        private static double mny;
        private static double changerate;
        private string money;

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

        public double findchangerate(string changethis, string changeinto) 
        {
            foreach (var pair in mylist1.currencyPairs)
            {
                if (pair.counterCurrency == changeinto)
                {
                    return pair.rate;
                }
            }
            return 0;
        }

        public double changecurrency(double money, double changerate)
        {
            return money * changerate;
        }

        static void Main(string[] args)
        {
            Program mycurrency = new Program();

            mycurrency.getparameters();

            mycurrency.getlist();

            changerate = mycurrency.findchangerate(changethis, changeinto);
            
            Console.WriteLine("yourchange : {0}", mycurrency.changecurrency(mny, changerate));

            Console.ReadKey();
        }

        public void getlist()
        {
            WebClient client = new WebClient();
            var mylist = client.DownloadString("http://api.dev.paximum.com/v1/currency/GetExchangeRates?basecurrency=" + changethis + "&access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL2F1dGgucGF4aW11bS5jb20iLCJhdWQiOiJodHRwczovL2FwaS5wYXhpbXVtLmNvbSIsIm5iZiI6MTQ2NjU5NTI5NSwiZXhwIjoxNDY2NjgxNjk1LCJzdWIiOiI2NmE1MDZhNC1jZjI0LTQ1NjAtODdmZS1lYjQ1ZWJjOTUzOTkiLCJyb2xlIjoicGF4OmRldmVsb3BlciJ9.OePUvylIcVnEQ1E5aeBiB8fuvuVHWXvTUW0u7cG3_QM");
            mylist1 = (JsnList)JsonConvert.DeserializeObject(mylist, typeof(JsnList));
        }

        public void getparameters()
        {
            Console.WriteLine("Please enter the curreny you want to translate");
            changethis = Console.ReadLine();
            Console.WriteLine("Please enter the curreny you want to get");
            changeinto = Console.ReadLine();
            Console.WriteLine("Please enter how much you want to change");
            money = Console.ReadLine();
            mny = Convert.ToDouble(money);
        }    
    }
}
