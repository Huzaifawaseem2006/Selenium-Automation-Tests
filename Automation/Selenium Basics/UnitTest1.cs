using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FirstLecture
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;

        [TestInitialize]
        public void initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://adactinhotelapp.com/";
        }
        [TestCleanup]
        public void cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void TestMethod1(int num)
        {
            Console.WriteLine(num);
        }

        //THIS IS A POSITIVE TEST CASE
        [TestMethod]
        [DataRow("evinevin7277@gmail.com", "B6988W")]
        public void TestMethod2(string user, string pass)
        {

            driver.FindElement(By.Id("username")).SendKeys(user);
            driver.FindElement(By.Id("password")).SendKeys(pass);
            driver.FindElement(By.Id("login")).Click();
            string message = driver.FindElement(By.ClassName("welcome_menu")).Text;
            Assert.AreEqual("Welcome to Adactin Group of Hotels", message);

            Thread.Sleep(3000);
        }

        //THIS IS A NEGATIVE TEST CASE
        [TestMethod]
        [DataRow("evinevin72773@gmail.com", "B6988W")]
        public void TestMethod3(string user, string pass)
        {

            driver.FindElement(By.Id("username")).SendKeys(user);
            driver.FindElement(By.Id("password")).SendKeys(pass);
            driver.FindElement(By.Id("login")).Click();
            string message = driver.FindElement(By.ClassName("auth_error")).Text;
            Assert.AreEqual("Invalid Login details or Your Password might have expired. Click here to reset your password", message);

            Thread.Sleep(3000);

        }

        //this test method checks if the title of the page is correct
        [TestMethod]
        public void TestMethod4()
        {

            var title = driver.Title;
            Assert.AreEqual("Adactin.com - Hotel Reservation System", title);

            Thread.Sleep(3000);


        }

        //this test method checks if the login page has all the required elements
        [TestMethod]
        public void TestMethod5()
        {

            Assert.IsTrue(driver.FindElement(By.Id("username")).Displayed, "Username field is not displayed");
            Assert.IsTrue(driver.FindElement(By.Id("password")).Displayed, "Password field is not displayed");
            Assert.IsTrue(driver.FindElement(By.Id("login")).Displayed, "Login button is not displayed");
            Assert.IsTrue(driver.FindElement(By.Id("login")).Enabled, "Login button is not enabled");

            Thread.Sleep(3000);

        }

        //this test method checks if the user can successfully search for a hotel
        [TestMethod]
        [DataRow("testuser123m", "123456")]
        public void TestMethod6(string user, string pass)
        {

            driver.FindElement(By.Id("username")).SendKeys(user);
            driver.FindElement(By.Id("password")).SendKeys(pass);
            driver.FindElement(By.Id("login")).Click();

            driver.FindElement(By.Id("location")).SendKeys("Sydney");
            driver.FindElement(By.Id("hotels")).SendKeys("Hotel Creek");
            driver.FindElement(By.Id("room_type")).SendKeys("Standard");
            driver.FindElement(By.Id("datepick_in")).SendKeys("01/01/2024");
            driver.FindElement(By.Id("datepick_out")).SendKeys("02/01/2024");
            driver.FindElement(By.Id("adult_room")).SendKeys("1");
            driver.FindElement(By.Id("child_room")).SendKeys("0");

            driver.FindElement(By.Id("Submit")).Click();
            string heading = driver.FindElement(By.ClassName("login_title")).Text;
            Assert.AreEqual("Select Hotel", heading);

            Thread.Sleep(5000);





        }
        //this test method checks if the user can login with an empty username
        [TestMethod]
        public void TestMethod7()
        {

            driver.FindElement(By.Id("username")).SendKeys("");
            driver.FindElement(By.Id("password")).SendKeys("123456");
            driver.FindElement(By.Id("login")).Click();

            string errorMessage = driver.FindElement(By.Id("username_span")).Text;
            Assert.AreEqual("Enter Username", errorMessage);

            Thread.Sleep(5000);


        }
        //this test method checks if the user can logout successfully
        [TestMethod]
        public void TestMethod8()
        {

            driver.FindElement(By.Id("username")).SendKeys("testuser123m");
            driver.FindElement(By.Id("password")).SendKeys("123456");
            driver.FindElement(By.Id("login")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();
            string message = driver.FindElement(By.LinkText("Click here to login again")).Text;
            Assert.AreEqual("Click here to login again", message);

            Thread.Sleep(5000);

        }
        //this test method checks if the user can select an option from a dropdown menu
        [TestMethod]
        public void TestMethod9()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://demoqa.com/select-menu";

            var selectElement = driver.FindElement(By.Id("oldSelectMenu"));
            var select = new SelectElement(selectElement);
            select.SelectByText("Red");
            Thread.Sleep(2000);
            select.SelectByValue("2");
        }

        [TestMethod]
        public void TestMethod10()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://demoqa.com/checkbox";
            var element = driver.FindElement(By.Id("rct-title"));
            if(element.)

        }
    }
}