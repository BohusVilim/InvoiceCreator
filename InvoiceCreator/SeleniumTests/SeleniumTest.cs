using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvoiceCreator.InvoiceCreatorDbContext;
using Microsoft.EntityFrameworkCore;
using InvoiceCreator.Models.MainModels;

namespace InvoiceCreator.SeleniumTests
{
    [TestClass]
    public class SeleniumTest
    {
        [TestMethod]
        public void ChromeSession()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://localhost:7248/Suppliers/Create");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textBox = driver.FindElement(By.Id("SupplierName"));
            var a = driver.FindElement(By.Id("SupplierAddress"));
            var b = driver.FindElement(By.Id("SupplierCountry"));
            var c = driver.FindElement(By.Id("SupplierIco"));
            var d = driver.FindElement(By.Id("SupplierDic"));
            var submitButton = driver.FindElement(By.Id("button"));

            textBox.SendKeys("Bohuš");
            a.SendKeys("SupplierAddress");
            b.SendKeys("SupplierCountry");
            c.SendKeys("1");
            d.SendKeys("2");
            submitButton.Click();

            string supplierName = ???;

            Assert.AreEqual("Bohuš", supplierName);

            driver.Quit();
        }
    }
}

