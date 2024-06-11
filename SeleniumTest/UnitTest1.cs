using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace SeleniumTestMyKidsreg
{
    [TestClass]
    public class SuperAdminTests
    {
        private static readonly string DriverDirectory = @"C:\Webdrivers\chromedriver-win32\chromedriver.exe";
        private static IWebDriver _driver;
        private static WebDriverWait _wait;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            try
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--headless"); // Kør i headless-tilstand for CI/CD miljøer uden GUI
                _driver = new ChromeDriver(DriverDirectory, chromeOptions);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved initialisering af WebDriver: {ex.Message}");
                throw;
            }
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            if (_driver != null)
            {
                //_driver.Quit();
                _driver.Dispose();
            }
        }

        [TestMethod]
        public void TestInstitutionSection()
        {
            _driver.Navigate().GoToUrl(@"file:///E:/HovedOpgave2024/MyKidsRegApp/MyKidsReg/Super_Admin/superadmin.html");
            var institutionLink = _wait.Until(drive => drive.FindElement(By.LinkText("Institution")));
            Assert.IsNotNull(institutionLink, "Institution link not found");
            institutionLink.Click();

            var institutionTable = _wait.Until(driver => driver.FindElement(By.Id("data-table")));
            Assert.IsTrue(institutionTable.Displayed, "institutiontable is not found");
        }
       


        [TestMethod]
        public void TestStudentSection()
        {
            _driver.Navigate().GoToUrl(@"file:///E:/HovedOpgave2024/MyKidsRegApp/MyKidsReg/Super_Admin/superadmin.html");

            
            var studentLink = _wait.Until(driver => driver.FindElement(By.LinkText("Studerende")));
            Assert.IsNotNull(studentLink, "Student link not found");
            studentLink.Click();

           
            var studentTable = _wait.Until(driver => driver.FindElement(By.Id("data-table")));
            Assert.IsTrue(studentTable.Displayed, "Institution table is not displayed");

        }
        [TestMethod]
        public void TestCreateAdminRelationForm()
           
        {
            _driver.Navigate().GoToUrl(@"file:///E:/HovedOpgave2024/MyKidsRegApp/MyKidsReg/AdminRelations/createRelation.html");

            // Verify that the page title is correct
            Assert.AreEqual("Create Admin Relation", _driver.Title);

            // Verify that the form is present
            var form = _wait.Until(driver => driver.FindElement(By.TagName("form")));
            Assert.IsNotNull(form);

            // Select a user from the dropdown
            var userSelect = _wait.Until(driver => driver.FindElement(By.Id("user")));
            Assert.IsNotNull(userSelect, "User dropdown not found");

            var userOptions = userSelect.FindElements(By.TagName("option"));
            Assert.IsFalse(userOptions.Any(), "No user options available");

            // Select the second option if available, otherwise select the first option
            if (userOptions.Count > 1)
            {
                userOptions[1].Click();
            }
            else if (userOptions.Count == 1)
            {
                userOptions[0].Click();
            }
            else
            {
                Assert.Fail("No user options available");
            }

            // Select an institution from the dropdown
            var institutionSelect = _wait.Until(driver => driver.FindElement(By.Id("Institution")));
            Assert.IsNotNull(institutionSelect, "Institution dropdown not found");

            var institutionOptions = institutionSelect.FindElements(By.TagName("option"));
            Assert.IsTrue(institutionOptions.Any(), "No institution options available");

            // Select the second option if available, otherwise select the first option
            if (institutionOptions.Count > 1)
            {
                institutionOptions[1].Click();
            }
            else if (institutionOptions.Count == 1)
            {
                institutionOptions[0].Click();
            }
            else
            {
                Assert.Fail("No institution options available");
            }

            // Click the Create button
            var createButton = _wait.Until(driver => driver.FindElement(By.CssSelector("button[type='submit']")));
            createButton.Click();

            // Verify that some confirmation or expected result occurs
            // This step depends on what happens after form submission. For example, you might expect a success message.
            var successMessage = _wait.Until(driver => driver.FindElement(By.CssSelector(".success-message")));
            Assert.IsTrue(successMessage.Displayed, "Success message not displayed");
        }

        [TestMethod]
        public void TestPaedagogLink()
        {
            // Navigate to the Admin page
            _driver.Navigate().GoToUrl(@"file:///E:\HovedOpgave2024\MyKidsRegApp\MyKidsReg\Admin\admin.html");
       
        // Click on the "Pædagog" link
        var paedagogLink = _wait.Until(driver => driver.FindElement(By.LinkText("Pædagog")));
            Assert.IsNotNull(paedagogLink, "Pædagog link not found");
            paedagogLink.Click();

            // Wait for the new page to load (this can be adjusted based on what you expect to be present on the new page)
            _wait.Until(driver => driver.Url.Contains("../Admin/Pædagog/pædagog.html"));

            // Verify that the Pædagog page is loaded by checking for a specific element that should be on that page
            var tableSection = _wait.Until(driver => driver.FindElement(By.Id("table-section")));
            Assert.IsTrue(tableSection.Displayed, "Table section is not displayed on the Pædagog page");
        }

    }
}
