using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PriceCompare
{
    class Program
    {
        public struct Data
        {
            public Data(string strTitle, int intPrice, string strSite)
            {
                StrTitle = strTitle;
                IntPrice = intPrice;
                StrSite = strSite;
            }
            public string StrTitle { get; private set; }
            public int IntPrice { get; private set; }
            public string StrSite { get; private set; }
        }
        static void Main(string[] args)
        {
            var productList = new List<Data>();
            Console.WriteLine("***What do you Want to Compare prices of?***\n\n");
            string nameOfTheProductToBeSearched = Console.ReadLine();

            //SastoDeal sastoDeal = new SastoDeal(nameOfTheProductToBeSearched);
            //sastoDeal.WebScrape();
            //Console.Clear();
            //Console.WriteLine("***We are searching for {0}***\n\n",nameOfTheProductToBeSearched);
            //for (int i = 0; i < sastoDeal.NoOfItemsInWebPage(); i++)
            //{
            //    var item = new Data(sastoDeal.productTitle(i), sastoDeal.productPrice(i), "sasto deal");
            //    productList.Add(item);
            //}

            Console.Clear();
            Console.WriteLine("***We are searching for {0}***\n\n", nameOfTheProductToBeSearched);
            Daraz daraz = new Daraz(nameOfTheProductToBeSearched);
            daraz.WebScrape();
            for (int i = 0; i < daraz.NoOfItemsInWebPage(); i++)
            {
                var item = new Data(daraz.productTitle(i), daraz.productPrice(i), "daraz");
                productList.Add(item);
            }
            var improved = productList.OrderBy(o => o.IntPrice).ToList();

            foreach (var item in improved)
            {
                Console.WriteLine(item.StrTitle +"\n"+ item.IntPrice + "\n"+ item.StrSite +"\n\n");
            }
            Console.SetWindowPosition(0, 0);
        }
    }
}