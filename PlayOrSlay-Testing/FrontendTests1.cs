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
        public string[] DataOfGame1 { get; set; } = { Guid.NewGuid().ToString(), "SeleniumDescription", "11111111", "01a1eefd-fc43-7002-9f0d-32a441940735", "http://google.com" };
        public string[] DataOfGame2 { get; set; } = { "", "NewDescription", "22222222", "01a1eefd-fc43-7002-9f0d-32a441940735", "http://neti.ee" };

        [Fact] //siin on test
        [Order(1)] //järjekorranumber
        public void Should_NavigateToCreate_AddGame_WithCorrectData()
        {
            //Ülesseade
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "http://localhost:5173";
            Thread.Sleep(200);

            IWebElement navigateToGames = driver.FindElement(By.Id("RL_Games"));
            navigateToGames.Click();
            Thread.Sleep(200);
            IWebElement navigateToCreateGame = driver.FindElement(By.Id("CreateGame"));
            navigateToCreateGame.Click();
            //tegevus
            InputGameData(driver, DataOfGame1);

            IWebElement saveButton = driver.FindElement(By.Id("saveGame"));
            saveButton.Click();

            Thread.Sleep(250);

            //kontroll
            IWebElement thisGameButton = driver.FindElement(By.XPath($"//button[text()='{DataOfGame1[0]}']"));
            thisGameButton.Click();

            string gameNameDetail = driver.FindElement(By.Id("gameNameDetail")).Text;
            string gameDescriptionDetail = driver.FindElement(By.Id("gameDescriptionDetail")).Text;
            string gameCategoryDetail = driver.FindElement(By.Id("gameCategoryDetail")).Text;

            driver.Quit();

            Assert.Equal(DataOfGame1[0], gameNameDetail);
            Assert.Equal(DataOfGame1[1], gameDescriptionDetail);
            Assert.Equal(DataOfGame1[3], gameCategoryDetail);
        }
        [Fact]
        [Order(2)]
        public void Should_NavigateToDetailsOfGame_WithPreviouslyCorrectData()
        {
            //Ülesseade
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "http://localhost:5173";
            Thread.Sleep(200);

            IWebElement navigateToGames = driver.FindElement(By.Id("RL_Games"));
            navigateToGames.Click();
            Thread.Sleep(200);
            //tegevus
            IWebElement thisGame = driver.FindElement(By.XPath($"//button[text()='{DataOfGame1[0]}']"));
            thisGame.Click();
            //kontroll
            string gameNameDetail = driver.FindElement(By.Id("gameNameDetail")).Text;
            string gameDescriptionDetail = driver.FindElement(By.Id("gameDescriptionDetail")).Text;
            string gameCategoryDetail = driver.FindElement(By.Id("gameCategoryDetail")).Text;

            driver.Quit();

            Assert.Equal(DataOfGame1[0], gameNameDetail);
            Assert.Equal(DataOfGame1[1], gameDescriptionDetail);
            Assert.Equal(DataOfGame1[3], gameCategoryDetail);
        }

        private static void InputGameData(IWebDriver driver, string[] dataOfGame)
        {
            IWebElement inputName = driver.FindElement(By.Id("gameName"));
            inputName.SendKeys(dataOfGame[0]);

            IWebElement inputDescription = driver.FindElement(By.Id("gameDescription"));
            inputDescription.SendKeys(dataOfGame[1]);

            IWebElement releaseDate = driver.FindElement(By.Id("releaseDate"));
            releaseDate.Click();
            releaseDate.SendKeys(dataOfGame[2]);

            IWebElement categoryId = driver.FindElement(By.Id("categoryId"));
            categoryId.SendKeys(dataOfGame[3]);

            IWebElement imageUrl = driver.FindElement(By.Id("imageUrl"));
            imageUrl.SendKeys(dataOfGame[4]);
        }
    }
}
