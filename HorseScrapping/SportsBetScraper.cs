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
    public class SportsBetScraper:Scraper
    {
        public SportsBetScraper()
        {
            m_strDomain = "https://new.sportsbet.com.au/";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("button[data-automation-id='header-login-touchable']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement logInBtn = getElementBy(m_chrDriver,By.CssSelector("button[data-automation-id='header-login-touchable']"));
                logInBtn.Click();
                WaitForTagVisible(m_chrDriver,By.CssSelector("div[class='bodyContainerFixedDesktop_fdefsah']"));
                IWebElement loginBtn = getElementBy(m_chrDriver,By.CssSelector("button[class='textButton_f153ctfb']"));
                IWebElement userElement = getElementBy(m_chrDriver, By.CssSelector("input[data-automation-id='login-username']"));
                IWebElement pwdElement = getElementBy(m_chrDriver, By.CssSelector("input[data-automation-id='login-password']"));
                userElement.SendKeys(m_strUser);
                Thread.Sleep(1000);
                pwdElement.SendKeys(m_strPwd);
                loginBtn.Click();
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
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("button[data-automation-id='header-login-touchable']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("button[data-automation-id='header-login-touchable']"));
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[data-automation-id='racecard-body']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("div[data-automation-id='racecard-body']"));
                IEnumerable<IWebElement> raceRows = raceRowsContainer.FindElements(By.CssSelector("div[class='outcomeCard_fhfy5kv']"));
                foreach (IWebElement raceItem in raceRows)
                {
                    if (!raceItem.Text.Contains("SCRATCHED"))
                    {
                        RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                        IWebElement detailedInfo = raceItem.FindElement(By.CssSelector("div[data-automation-id='racecard-outcome-name']"));
                        string[] detailedArray = detailedInfo.Text.Split('.');
                        runnerNameTemp.m_strRunnerNumber = detailedArray[0].Replace(" ", "");
                        runnerNameTemp.m_strRunnerName = detailedArray[1].Replace("  ", "");
                        try
                        {
                            runnerNameTemp.m_strWinPrice = raceItem.FindElement(By.CssSelector("div[data-automation-id='racecard-outcome-0-L-price']")).Text;
                            runnerNameTemp.m_strFixedPrice = raceItem.FindElement(By.CssSelector("div[data-automation-id='racecard-outcome-1-L-price']")).Text;
                        }
                        catch (Exception e)
                        {

                        }
                        m_lstRunnerName.Add(runnerNameTemp);
                    }

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
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[data-automation-id='racecard-event-selector-container']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("div[data-automation-id='racecard-event-selector-container']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("li"));
                foreach (IWebElement tabItem in tabs)
                {
                    try
                    {
                        IWebElement aTag = tabItem.FindElement(By.CssSelector("a"));
                        m_lstEventUrls.Add(aTag.GetAttribute("href"));
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
