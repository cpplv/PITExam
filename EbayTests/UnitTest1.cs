using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EbayTests
{
    [TestClass]
    public class EbaySearchTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ebay.com/");
        }

        [TestMethod]
        public void Test1_field()
        {
            var wait = new WebDriverWait(
                driver, TimeSpan.FromSeconds(15));

            bool exists = wait.Until(d =>
                d.FindElements(By.Id("gh-ac")).Count > 0);

            Assert.IsTrue(exists, "Not found");
        }

        [TestMethod]
        public void Test2_search()
        {
            var wait = new WebDriverWait(
                driver, TimeSpan.FromSeconds(15));

            bool exists = wait.Until(d =>
                d.FindElements(
                    By.Id("gh-search-btn")).Count > 0);

            Assert.IsTrue(exists, "Not found");
        }

        [TestCleanup]
        public void CloseBrowser()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}