using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumTest
{
    [TestClass]
    public class SuperAdminPageTests
    {
        private static readonly string DriverDirectory = "C:\\Webdrivers";
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void VerifyBrugerTableDisplayed()
        {
            string url = "C:\\Users\\yusaf\\source\\repos\\MyKidsReg-1\\Super_Admin\\superadmin.html"; 
            _driver.Navigate().GoToUrl(url);

            // Click on the Bruger link
            var brugerLink = _driver.FindElement(By.CssSelector("a.nav-link.active"));
            brugerLink.Click();

            // Wait for the table to be displayed
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("table-section")).Displayed);

            // Verify that the Bruger table is displayed
            var table = _driver.FindElement(By.Id("table-section"));
            Assert.IsTrue(table.Displayed);
        }

        [TestMethod]
        public void VerifyInstitutionTableDisplayed()
        {
            string url = "C:\\Users\\yusaf\\source\\repos\\MyKidsReg-1\\Super_Admin\\superadmin.html"; 
            _driver.Navigate().GoToUrl(url);

            // Click on the Institution link
            var institutionLink = _driver.FindElement(By.CssSelector("a[href='../Institution/institution.html']"));
            institutionLink.Click();

            // Wait for the table to be displayed
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("institution-section")).Displayed);

            // Verify that the Institution table is displayed
            var table = _driver.FindElement(By.Id("institution-section"));
            Assert.IsTrue(table.Displayed);
        }

        [TestMethod]
        public void VerifyCreateButtonsDisplayed()
        {
            string url = "C:\\Users\\yusaf\\source\\repos\\MyKidsReg-1\\Super_Admin\\superadmin.html"; 
            _driver.Navigate().GoToUrl(url);

            // Click on the Bruger link
            var brugerLink = _driver.FindElement(By.CssSelector("a.nav-link.active"));
            brugerLink.Click();

            // Wait for the Bruger section to be displayed
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("table-section")).Displayed);

            // Verify that the "Opret Bruger" button is displayed
            var createUserButton = _driver.FindElement(By.CssSelector("button.btn-create-user"));
            Assert.IsTrue(createUserButton.Displayed);

            // Click on the Institution link
            var institutionLink = _driver.FindElement(By.CssSelector("a[href='../Institution/institution.html']"));
            institutionLink.Click();

            // Wait for the Institution section to be displayed
            wait.Until(d => d.FindElement(By.Id("institution-section")).Displayed);

            // Verify that the "Opret Institution" button is displayed
            var createInstitutionButton = _driver.FindElement(By.CssSelector("button.btn-success"));
            Assert.IsTrue(createInstitutionButton.Displayed);
        }
    }
}
