using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace TestVlru
{
    [TestClass]
    public class UnitTest1
    {
        private static ChromeDriver driver;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext) {
            driver = new ChromeDriver();
        }

        [TestInitialize]
        public void TestInit()
        {
            driver.Navigate().GoToUrl(@"https://www.vl.ru");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("header__search-form-toggle")));
        }

        [TestMethod]
        public void Successful_Search_With_Correct_Value()
        {
            var searchLink = driver.FindElementByClassName("header__search-form-toggle");
            searchLink.Click();

            var textArea = driver.FindElementByClassName("header__search-query");
            textArea.SendKeys("вакансии");
            textArea.Submit();

            var searchResult = driver.FindElementByClassName("item-title").Text;
            Assert.AreEqual("Агентства по трудоустройству", searchResult);
            Console.WriteLine(searchResult);
        }

        [TestMethod]
        public void Successful_Search_With_Space_In_Value()
        {
            var searchLink = driver.FindElementByClassName("header__search-form-toggle");
            searchLink.Click();

            var textArea = driver.FindElementByClassName("header__search-query");
            textArea.SendKeys(" вакансии ");
            textArea.Submit();

            var searchResult = driver.FindElementByClassName("item-title").Text;
            Assert.AreEqual("Агентства по трудоустройству", searchResult);
            Console.WriteLine(searchResult);
        }

        [TestMethod]
        public void Successful_Search_With_Value_In_Mix_Register()
        {
            var searchLink = driver.FindElementByClassName("header__search-form-toggle");
            searchLink.Click();

            var textArea = driver.FindElementByClassName("header__search-query");
            textArea.SendKeys("ВаКаНсИи");
            textArea.Submit();

            var searchResult = driver.FindElementByClassName("item-title").Text;
            Assert.AreEqual("Агентства по трудоустройству", searchResult);
            Console.WriteLine(searchResult);
        }

        [TestMethod]
        public void Successful_Search_With_Value_In_Uppercase()
        {
            var searchLink = driver.FindElementByClassName("header__search-form-toggle");
            searchLink.Click();

            var textArea = driver.FindElementByClassName("header__search-query");
            textArea.SendKeys("ВАКАНСИИ");
            textArea.Submit();

            var searchResult = driver.FindElementByClassName("item-title").Text;
            Assert.AreEqual("Агентства по трудоустройству", searchResult);
            Console.WriteLine(searchResult);
        }

        [TestMethod]
        public void Successful_Search_With_Value_End_Dot()
        {
            var searchLink = driver.FindElementByClassName("header__search-form-toggle");
            searchLink.Click();

            var textArea = driver.FindElementByClassName("header__search-query");
            textArea.SendKeys("вакансии.");
            textArea.Submit();

            var searchResult = driver.FindElementByClassName("item-title").Text;
            Assert.AreEqual("Агентства по трудоустройству", searchResult);
            Console.WriteLine(searchResult);
        }

        [TestMethod]
        public void Unsuccessful_Search_With_Uncorrect_Value()
        {
            var searchLink = driver.FindElementByClassName("header__search-form-toggle");
            searchLink.Click();

            var textArea = driver.FindElementByClassName("header__search-query");
            textArea.SendKeys("Так щедро август звёзды расточал");
            textArea.Submit();

            var searchResult = driver.FindElementByCssSelector("h1.search").Text;
            Assert.AreEqual("Ничего не найдено", searchResult);
            Console.WriteLine(searchResult);
        }

        [ClassCleanup]
        public static void Close()
        {
            driver.Close();
        }
    }
}
