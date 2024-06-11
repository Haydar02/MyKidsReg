using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace SeleniumTestMyKidsreg
{
    public class SuperAdminTestes : IDisposable
   {
        private static readonly string DriverDirectory = "C:\\Webdrivers";
        private readonly IWebDriver _drive;
        
        
        public void Dispose()
        {
            _drive.Quit();
            _drive.Dispose();
        }

        [SetUp]
        public void Setup()
        {
         
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}