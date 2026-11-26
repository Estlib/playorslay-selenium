using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayOrSlay_Testing
{
    [TestCaseOrderer("PlayOrSlay_Testing.PriorityOrderer", "PlayOrSlay_Testing")]
    public class FrontendTests1
    {
        [Fact] //siin on test
        [Order(1)] //järjekorranumber
        public void Should_NavigateToCreate_AddGame_WithCorrectData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "http://localhost:5173";
            Thread.Sleep(200);

            IWebElement navigateToGames = driver.FindElement(By.Id("RL_Games"));
            navigateToGames.Click();
            Thread.Sleep(200);
            IWebElement navigateToCreateGame = driver.FindElement(By.Id("CreateGame"));
            navigateToCreateGame.Click();

            InputGameData(driver);

            IWebElement saveButton = driver.FindElement(By.Id("saveGame"));
            saveButton.Click();

            Thread.Sleep(250);

            IWebElement thisGameButton = driver.FindElement(By.XPath("//button[text()='SeleniumName']"));
            thisGameButton.Click();

            string gameNameDetail = driver.FindElement(By.Id("gameNameDetail")).Text;
            string gameDescriptionDetail = driver.FindElement(By.Id("gameDescriptionDetail")).Text;
            string gameCategoryDetail = driver.FindElement(By.Id("gameCategoryDetail")).Text;

            Assert.Equal("SeleniumName", gameNameDetail);
            Assert.Equal("SeleniumDescription", gameDescriptionDetail);
            Assert.Equal("01a1eefd-fc43-7002-9f0d-32a441940735", gameCategoryDetail);
        }

        private static void InputGameData(IWebDriver driver)
        {
            IWebElement inputName = driver.FindElement(By.Id("gameName"));
            inputName.SendKeys("SeleniumName");

            IWebElement inputDescription = driver.FindElement(By.Id("gameDescription"));
            inputDescription.SendKeys("SeleniumDescription");

            IWebElement releaseDate = driver.FindElement(By.Id("releaseDate"));
            releaseDate.Click();
            releaseDate.SendKeys("11111111");

            IWebElement categoryId = driver.FindElement(By.Id("categoryId"));
            categoryId.SendKeys("01a1eefd-fc43-7002-9f0d-32a441940735");

            IWebElement imageUrl = driver.FindElement(By.Id("imageUrl"));
            imageUrl.SendKeys("http://google.com");


        }
    }
}
