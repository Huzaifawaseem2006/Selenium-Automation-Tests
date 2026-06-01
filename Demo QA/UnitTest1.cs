using System;
using System.Net.Http;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using static System.Net.Mime.MediaTypeNames;

namespace Demo_QA
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;



        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // This method can be used for class-level initialization if needed
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

        }
        [TestCleanup]
        public void Cleanup()
        {
            driver.Close();
        }

        //This test case shows how to test a dropdown  
        [TestMethod]
        public void TestMethod1()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/checkbox");
            driver.FindElement(By.XPath("//button[@title='Expand all']")).Click();
            var commandsCheckbox = driver.FindElement(By.XPath("//label[@for='tree-node-commands']"));
            if (!commandsCheckbox.Selected)
            {
                commandsCheckbox.Click();
            }
            var message = driver.FindElement(By.ClassName("text-success")).Text;
            Thread.Sleep(1000); // Wait for the message to appear
            Assert.AreEqual("commands", message);

            Thread.Sleep(5000); // Wait for the message to appear
        }
        //This test case shows how to test a Radio Button
        [TestMethod]
        public void TestMethod2()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/radio-button");
            driver.FindElement(By.XPath("//label[@for='yesRadio']")).Click();
            Thread.Sleep(5000);
            var message = driver.FindElement(By.ClassName("text-success")).Text;
            Assert.AreEqual("Yes", message);
            Thread.Sleep(5000);

        }
        //This method shows how to add a record in a web table
        [TestMethod]
        public void TestMethod3()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/webtables");
            driver.FindElement(By.Id("addNewRecordButton")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("userEmail")).SendKeys("huzaifa123456@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("30");
            driver.FindElement(By.Id("salary")).SendKeys("50000");
            driver.FindElement(By.Id("department")).SendKeys("IT");
            driver.FindElement(By.Id("submit")).Click();

            Thread.Sleep(2000); // Wait for the record to be added
            var table = driver.FindElement(By.ClassName("rt-tbody"));
            if (table.Text.Contains("John") && table.Text.Contains("Doe"))
            {
                Assert.IsTrue(true, "Record added successfully.");
            }
            else
            {
                Assert.Fail("Record not found in the table.");
            }


            Thread.Sleep(2000);

        }
        //This method shows how to delete a record from a web table
        [TestMethod]
        public void TestMethod4()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/webtables");
            driver.FindElement(By.Id("addNewRecordButton")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("userEmail")).SendKeys("huzaifa123456@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("30");
            driver.FindElement(By.Id("salary")).SendKeys("50000");
            driver.FindElement(By.Id("department")).SendKeys("IT");
            driver.FindElement(By.Id("submit")).Click();

            Thread.Sleep(2000); // Wait for the record to be added

            driver.FindElement(By.Id("delete-record-4")).Click();
            Thread.Sleep(2000); // Wait for the record to be deleted
            var table = driver.FindElement(By.ClassName("rt-tbody"));
            if (!table.Text.Contains("John") && !table.Text.Contains("Doe"))
            {
                Assert.IsTrue(true, "Record deleted successfully.");
            }
            else
            {
                Assert.Fail("Record still found in the table.");
            }

            Thread.Sleep(2000);
        }

        //This method shows how to update a record in a web table
        [TestMethod]
        public void TestMethod5()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/webtables");
            driver.FindElement(By.Id("addNewRecordButton")).Click();
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("userEmail")).SendKeys("huzaifa123456@gmail.com");
            driver.FindElement(By.Id("age")).SendKeys("30");
            driver.FindElement(By.Id("salary")).SendKeys("50000");
            driver.FindElement(By.Id("department")).SendKeys("IT");
            driver.FindElement(By.Id("submit")).Click();

            Thread.Sleep(2000); // Wait for the record to be added
            driver.FindElement(By.Id("edit-record-4")).Click();
            var firstNameField = driver.FindElement(By.Id("firstName"));
            firstNameField.Clear();
            firstNameField.SendKeys("kane");
            driver.FindElement(By.Id("submit")).Click();
            Thread.Sleep(2000); // Wait for the record to be updated
            var table = driver.FindElement(By.ClassName("rt-tbody"));
            if (table.Text.Contains("kane") && table.Text.Contains("Doe"))
            {
                Assert.IsTrue(true, "Record updated successfully.");
            }
            else
            {
                Assert.Fail("Record not found in the table.");
            }

            Thread.Sleep(2000);


        }
        // This method shows how to perform a double-click action
        [TestMethod]
        public void TestMethod6()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/buttons");
            var doubleClickButton = driver.FindElement(By.Id("doubleClickBtn"));

            Assert.IsTrue(doubleClickButton.Displayed, "Double Click button should be visible");
            Assert.IsTrue(doubleClickButton.Enabled, "Double Click button should be enabled");

            var actions = new Actions(driver);
            actions.DoubleClick(doubleClickButton).Perform();

            Thread.Sleep(2000);
            var message = driver.FindElement(By.Id("doubleClickMessage")).Text;
            Assert.AreEqual("You have done a double click", message);
            Thread.Sleep(2000);


        }
        // This method shows how to perform a right-click action
        [TestMethod]
        public void TestMethod7()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/buttons");
            var rightClickButton = driver.FindElement(By.Id("rightClickBtn"));

            Assert.IsTrue(rightClickButton.Displayed, "Right Click button should be visible");
            Assert.IsTrue(rightClickButton.Enabled, "Right Click button should be enabled");

            var actions = new Actions(driver);
            actions.ContextClick(rightClickButton).Perform();
            Thread.Sleep(2000);
            var message = driver.FindElement(By.Id("rightClickMessage")).Text;
            Assert.AreEqual("You have done a right click", message);
            Thread.Sleep(2000);
        }
        // This method shows how to perform a single-click action
        [TestMethod]
        public void TestMethod8()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/buttons");
            var clickMeButton = driver.FindElement(By.XPath("//button[text()='Click Me']"));

            Assert.IsTrue(clickMeButton.Displayed, "Click Me button should be visible");
            Assert.IsTrue(clickMeButton.Enabled, "Click Me button should be enabled");

            var actions = new Actions(driver);
            actions.Click(clickMeButton).Perform();
            Thread.Sleep(2000);
            var message = driver.FindElement(By.Id("dynamicClickMessage")).Text;
            Assert.AreEqual("You have done a dynamic click", message);
            Thread.Sleep(2000);

        }
        // This method shows how to test links and verify navigation
        [TestMethod]
        public void TestMethod9()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/links");
            driver.FindElement(By.Id("simpleLink")).Click();
            Thread.Sleep(2000);

            driver.Url = "https://demoqa.com/";
            var currentUrl = driver.Url;

            Assert.AreEqual("https://demoqa.com/", currentUrl);
            Thread.Sleep(2000);

            driver.Navigate().GoToUrl("https://demoqa.com/links");
            driver.FindElement(By.Id("dynamicLink")).Click();
            Thread.Sleep(2000);

            driver.Url = "https://demoqa.com/";
            var currentUrl2 = driver.Url;
            Assert.AreEqual("https://demoqa.com/", currentUrl2);

            Thread.Sleep(2000);
        }
        // This method shows how to test API links and verify the response message
        [TestMethod]
        public void TestMethod10()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/links");
            driver.FindElement(By.Id("created")).Click();
            Thread.Sleep(2000);
            var linkResponse = driver.FindElement(By.Id("linkResponse")).Text;
            Assert.AreEqual("Link has responded with staus 201 and status text Created", linkResponse);
            Thread.Sleep(2000);

        }
        //This method shows how to test broken image
        [TestMethod]
        public void TestMethod11()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/broken");
            var brokenImage = driver.FindElement(By.XPath("//img[@src='/images/Toolsqa_1.jpg']"));
            string imgSrc = brokenImage.GetAttribute("src");
            HttpClient client = new HttpClient();
            var response = client.GetAsync(imgSrc).Result;
            Thread.Sleep(2000);
            Assert.IsTrue(response.IsSuccessStatusCode, "Image is not broken.");

            Thread.Sleep(2000);

        }
        //This method shows how to test broken link
        [TestMethod]
        public void TestMethod12()
        {
            


        }
    }
}
