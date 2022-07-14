using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCompare
{
    class Daraz
    {
        int NoOfItemsInPage { get; set; }
        List<List<string>> ProductInfo { get; set; }
        string NameOfProductToBeSearched { get; set; }
        private static IWebDriver driver;
        public Daraz(string nameOfProductToBeSearched)
        {
            this.NameOfProductToBeSearched = nameOfProductToBeSearched;
        }
        public void WebScrape()
        {
            int vcount = 1;
            string url = "https://www.daraz.com.np/";
            IWebDriver driver = new ChromeDriver("C:/Users/abina/source/repos/PriceCompare");
            driver.Navigate().GoToUrl(url);
            Console.Clear();
            var timeout = 10000;
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            driver.FindElement(By.XPath("//*[@id=\"q\"]")).SendKeys(NameOfProductToBeSearched);

            var buttonClickSearch = driver.FindElement(By.XPath("//*[@id=\"topActionHeader\"]/div/div[2]/div/div[2]/form/div/div[2]"));
            buttonClickSearch.Submit();

            wait.Equals(1000);
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            Int64 last_height = (Int64)(((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.scrollHeight"));
            while (true)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight);");
                Thread.Sleep(2000);
                Int64 new_height = (Int64)((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.scrollHeight");
                if (new_height == last_height)
                    break;
                last_height = new_height;
            }

            By productLink = By.ClassName("gridItem--Yd0sa");
            ReadOnlyCollection<IWebElement> products = driver.FindElements(productLink);
            NoOfItemsInPage = products.Count;
            List<List<string>> productInfo = new List<List<string>>();

            foreach (IWebElement product in products)
            {
                string productName, productPrice;
                IWebElement elemProductName = product.FindElement(By.ClassName("title--wFj93"));
                productName = elemProductName.Text;

                IWebElement elemProductPrice = product.FindElement(By.ClassName("currency--GVKjl"));
                productPrice = elemProductPrice.Text;

                List<string> productIndividual = new List<string>();
                productIndividual.Add(productName);
                productIndividual.Add(productPrice);
                productInfo.Add(productIndividual);
                //Console.WriteLine("a");
                vcount++;
            }
            ProductInfo = productInfo;

            driver.Close();
            Console.Clear();
        }
        public string productInfoReturn(int a, int b)
        {
            return ProductInfo[a][b];
        }
        public string productTitle(int a)
        {
            return ProductInfo[a][0];
        }
        public int productPrice(int c)
        {
            string a = ProductInfo[c][1];
            string b = string.Empty;
            int val = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }

            if (b.Length > 0)
            {
                val = Convert.ToInt32(b);
            }
            return val;
            //return Int32.Parse(ProductInfo[b][1]);
        }
        public int NoOfItemsInWebPage()
        {
            return NoOfItemsInPage;
        }
    }

}
