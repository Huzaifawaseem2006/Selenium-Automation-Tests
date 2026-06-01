using System;
using System.IO;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V137.Debugger;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Status = AventStack.ExtentReports.Status;

namespace Page_Object_model
{
    

    [TestClass]
    public class UnitTest1
    {

        

        private IWebDriver driver;
        public TestContext instance;
        public TestContext TestContext
        {
            get { return instance; }
            set { instance = value; }
        }
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            string ResultFilePath = @"C:\ExtentReports\TestExecLog_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";

            BasePage.CreateReport(ResultFilePath);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            BasePage.extentReports.Flush();
        }

        [TestInitialize]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            BasePage.Test = BasePage.extentReports.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void Cleanup() 
        {
            if(driver != null)
            driver.Close();
        }

        //[TestMethod]

        //public void TestMethod1()
        //{
            
        //    driver.Url = "https://adactinhotelapp.com/";

            

        //    LoginPage loginPage = new LoginPage(driver);
        //    loginPage.Username = "testuser123m";
        //    loginPage.Password = "52090K";
        //    loginPage.Login();
        //    string message = driver.FindElement(By.ClassName("welcome_menu")).Text;
        //    Assert.AreEqual("Welcome to Adactin Group of Hotels", message);

        //    Thread.Sleep(5000);
            

        //}

        [TestMethod]
        public void TestMethod2() 
        {
            driver.Url = "https://adactinhotelapp.com/";
            LoginPage loginPage = new LoginPage(driver);

            loginPage.Username = "testuser123m";
            loginPage.Password = "52090K";

            BasePage.Step = BasePage.Test.CreateNode("Enter Login Details");
            loginPage.Login();
            loginPage.TakeScreenshot(Status.Pass, "Login Successful");



            var searchPage = new SearchPage(driver);

            BasePage.Step = BasePage.Test.CreateNode("Select City, Hotel Name, Room type");
            searchPage.Search("Sydney", "Hotel Creek", "Standard");
            BasePage.Step.Pass("Selected City, Hotel Name, Room type");

            string title = driver.Title;
            Assert.AreEqual("Adactin.com - Select Hotel", title);

            Thread.Sleep(5000);


        }
    }

    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }
        
        public string Username { get; set; }
        public string Password { get; set; }

        By usernameField = By.Id("username");
        By passwordField = By.Id("password");
        By loginField = By.Id("login");
        
        public void Login()
        {
            SubStep = Step.CreateNode("Enter Username");
            Type(usernameField, Username);
            SubStep.Pass("Username Entered");
            Thread.Sleep(2000);
            SubStep = Step.CreateNode("Enter Password");
            Type(passwordField, Password);
            SubStep.Pass("Password Entered");
            Click(loginField);


        }
    }

    public class SearchPage:BasePage
    {
        public SearchPage(IWebDriver driver) : base(driver) { }

        By locationDropdown = By.Id("location");
        By hotelDropdown = By.Id("hotels");
        By hotelTypeDropdown = By.Id("room_type");
        By numberOfRoomsDropdown = By.Id("room_nos");
        By checkIn = By.Id("datepick_in");
        By checkOut = By.Id("datepick_out");
        By adultsDropdown = By.Id("adult_room");
        By childrenDropdowwn = By.Id("child_room");
        By submitButton = By.Id("Submit");


        public void SelectLocation(string location) => Find(locationDropdown).SendKeys(location);
        public void SelectHotel(string hotel) => Find(hotelDropdown).SendKeys(hotel);
        public void SelectRoomType(string room_type) => Find(hotelTypeDropdown).SendKeys(room_type);
        public void Submit() => Find(submitButton).Click();

        public void Search(string location,string hotel,string room_type)
        {
            SelectLocation(location);
            SelectHotel(hotel);
            SelectRoomType(room_type);
            Submit();
        }
    }

    public class BasePage
    {
        public static ExtentReports extentReports;
        public static ExtentTest Test;
        public static ExtentTest Step;
        public static ExtentTest SubStep;

        protected IWebDriver driver;
        public BasePage(IWebDriver driver) => this.driver = driver;
        protected IWebElement Find(By locator) => driver.FindElement(locator);
        protected void Click(By locator) => Find(locator).Click();
        protected void Type(By locator, string text)
        {
            var element = Find(locator);
            element.Clear();
            element.SendKeys(text);
        }

        public static void CreateReport(string dirpath)
        {
            extentReports = new ExtentReports();
            var sparkReporter = new ExtentSparkReporter(@dirpath);
            extentReports.AttachReporter(sparkReporter);
        }

        // Capture Screenshot
        public void TakeScreenshot(Status status, string stepDetail)
        {
            string path = @"C:\ExtentReports\Screenshots"
            + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            File.WriteAllBytes(path, screenshot.AsByteArray);
            Step.Log(status, stepDetail, MediaEntityBuilder
                .CreateScreenCaptureFromPath(path).Build());

        }
    }
}
