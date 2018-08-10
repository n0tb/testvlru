using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace TestVlru
{
    [TestClass]
    public class TestCompanySearch
    {
        private static ChromeDriver driver;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            driver = new ChromeDriver();
        }

        [TestInitialize]
        public void TestInit()
        {
            driver.Navigate().GoToUrl(@"https://www.vl.ru");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("vl_input")));
        }

        [TestMethod]
        public void Successful_Search_With_Correct_Value()
        {
            var input = driver.FindElementByClassName("vl_input");
            input.SendKeys("днс");
            input.Submit();

            var companies = driver.FindElementsByClassName("company__header");
            var oneCompany = companies.First();

            Assert.AreEqual("DNS", oneCompany.Text);
            Console.WriteLine(oneCompany.Text);
        }

        [TestMethod]
        public void Successful_Search_With_Space_In_Value()
        {
            var input = driver.FindElementByClassName("vl_input");
            input.SendKeys(" днс ");
            input.Submit();

            var companies = driver.FindElementsByClassName("company__header");
            var oneCompany = companies.First();

            Assert.AreEqual("DNS", oneCompany.Text);
            Console.WriteLine(oneCompany.Text);
        }

        [TestMethod]
        public void Successful_Search_With_Value_In_Uppercase()
        {
            var input = driver.FindElementByClassName("vl_input");
            input.SendKeys("ДНС");
            input.Submit();

            var companies = driver.FindElementsByClassName("company__header");
            var oneCompany = companies.First();

            Assert.AreEqual("DNS", oneCompany.Text);
            Console.WriteLine(oneCompany.Text);
        }

        [TestMethod]
        public void Successful_Search_With_Value_End_Dot()
        {
            var input = driver.FindElementByClassName("vl_input");
            input.SendKeys("днс.");
            input.Submit();

            var companies = driver.FindElementsByClassName("company__header");
            var oneCompany = companies.First();

            Assert.AreEqual("DNS", oneCompany.Text);
            Console.WriteLine(oneCompany.Text);
        }

        [TestMethod]
        public void Successful_Search_With_Value_ContainsOf_Multiple_Word()
        {
            var input = driver.FindElementByClassName("vl_input");
            input.SendKeys("Центр профессиональной стоматологии");
            input.Submit();

            var companies = driver.FindElementsByClassName("company__header");
            var oneCompany = companies.First();

            Assert.AreEqual("Центр профессиональной стоматологии", oneCompany.Text);
            Console.WriteLine(oneCompany.Text);
        }

        [TestMethod]
        public void Unsuccessful_Search_With_Uncorrect_Value()
        {
            var input = driver.FindElementByClassName("vl_input");
            input.SendKeys("УсыЛапыИХвост");
            input.Submit();

            var result = driver.FindElementByCssSelector("h1.search").Text;
          
            Assert.AreEqual("Ничего не найдено", result);
            Console.WriteLine(result);
        }

        [ClassCleanup]
        public static void Close()
        {
            driver.Close();
        }
    }
}
