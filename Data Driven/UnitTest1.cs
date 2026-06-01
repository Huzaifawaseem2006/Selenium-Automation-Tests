using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Data_Driven
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext Instance;
        public TestContext TestContext
        {
            get { return Instance; }
            set { Instance = value; }
        }

        [TestMethod]
        [TestCategory("Login")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "data.xml", "TestCase_001", DataAccessMethod.Sequential)]
        public void TestCase_001()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://adactinhotelapp.com/";

            string username = TestContext.DataRow["user"].ToString();
            string password = TestContext.DataRow["pass"].ToString();

            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("login")).Click();
            Thread.Sleep(1000);

            driver.Close();

        }
    }
}
