using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium_Allure
{
    [AllureNUnit] // Move the attribute to the class declaration
    public class Tests
    {
        private IWebDriver driver;
        [SetUp]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        [Test]
        [AllureStep]
        [AllureDescription("This is a sample test description.")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureTag("SampleTag")]
        [AllureOwner("Huzaifa")]
        [AllureSuite("Sample Suite")]
        [AllureFeature("Sample Feature")]
        [AllureStory("Sample Story")]
        public void Test1()
        {
            driver.Url = "https://adactinhotelapp.com/";
            Assert.AreEqual("Adactin.com - Hotel Reservation System", driver.Title);
            driver.Close();
        }
    }
}