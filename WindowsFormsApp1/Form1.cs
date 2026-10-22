using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private IWebDriver driver;

        public Form1()
        {
            InitializeComponent();

            Shown += StartBrowser;
            btnSearch.Click += SearchProducts;
            btnBack.Click += GoBack;
            btnCloseBrowser.Click += CloseBrowser;
            FormClosed += CloseBrowser;
        }

        private void StartBrowser(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.ebay.com/");
        }

        private void SearchProducts(object sender, EventArgs e)
        {
            if (driver == null || string.IsNullOrWhiteSpace(txtSearch.Text))
                return;

            IWebElement search = driver.FindElement(By.Id("gh-ac"));
            search.Clear();
            search.SendKeys(txtSearch.Text);

            string oldUrl = driver.Url;
            driver.FindElement(By.Id("gh-search-btn")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(d => d.Url != oldUrl && d.Url.Contains("/sch/"));

            txtResult.Text = driver.Url;
            txtHistory.AppendText(driver.Url + Environment.NewLine);
        }

        private void GoBack(object sender, EventArgs e)
        {
            if (driver == null)
                return;

            driver.Navigate().Back();

            txtSearch.Clear();
            txtResult.Clear();
        }

        private void CloseBrowser(object sender, EventArgs e)
        {
            if (driver == null)
                return;

            driver.Quit();
            driver = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
