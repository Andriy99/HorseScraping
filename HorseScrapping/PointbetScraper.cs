using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace HorseScrapping
{
    public class PointbetScraper:Scraper
    {
        public PointbetScraper()
        {
            m_strDomain = "https://pointsbet.com";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("button[class='btn btn-quartenary']"));
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("button[class='btn btn-quartenary'"));
                loginBtn.Click();
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='modal-content']"));
                IWebElement loginSubmitBtn = getElementBy(m_chrDriver, By.CssSelector("button[class='btn btn-primary btn-block']"));
                IWebElement userElement = getElementBy(m_chrDriver, By.CssSelector("input[id='login-username']"));
                IWebElement pwdElement = getElementBy(m_chrDriver, By.CssSelector("input[id='Password']"));
                userElement.SendKeys(m_strUser);
                Thread.Sleep(1000);
                pwdElement.SendKeys(m_strPwd);
                loginSubmitBtn.Click();
                Thread.Sleep(2000);
                
            }
            catch (Exception e)
            {

            }
        }

        public override bool IsLoggedIn()
        {
            try
            {
                previousSource = m_chrDriver.PageSource;
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("button[class='btn btn-quartenary'"));
                return false;
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public override bool ValidateScrapeVersion(string url)
        {
            if (base.ValidateScrapeVersion(url))
                return true;
            else
                return false;
        }

        public override RunnerNamePrice[] ReadRunnersAndPrices(string url)
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(url);
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='racing-bets bet-win-place bet-fixed-price']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='racing-bets bet-win-place bet-fixed-price']"));
                IEnumerable<IWebElement> raceRows = raceRowsContainer.FindElements(By.CssSelector("li[class='ng-scope']"));
                foreach (IWebElement raceItem in raceRows)
                {
                    RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                    IWebElement detailedInfo = raceItem.FindElement(By.CssSelector("div[class='runner']"));
                    string[] detailedArray = detailedInfo.Text.Split('.');
                    runnerNameTemp.m_strRunnerNumber = detailedArray[0].Replace(" ","");
                    runnerNameTemp.m_strRunnerName = detailedArray[1].Replace("  ","");
                    try
                    {
                        runnerNameTemp.m_strWinPrice = raceItem.FindElement(By.CssSelector("button[class='btn-outline price-indicator left']")).Text;
                        runnerNameTemp.m_strFixedPrice = raceItem.FindElement(By.CssSelector("button[class='btn-outline price-indicator right ng-scope']")).Text;
                    }
                    catch (Exception e)
                    {

                    }
                    m_lstRunnerName.Add(runnerNameTemp);

                }
                return m_lstRunnerName.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override string[] GetRaceUrls(string url)
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(url);
                WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='race-list']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("ul[class='race-list']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("li"));
                foreach (IWebElement tabItem in tabs)
                {
                    try
                    {
                        tabItem.Click();
                        WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='race-list']"));
                        m_lstEventUrls.Add(m_chrDriver.Url);
                    }
                    catch (Exception e)
                    {

                    }
                }
                return m_lstEventUrls.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
