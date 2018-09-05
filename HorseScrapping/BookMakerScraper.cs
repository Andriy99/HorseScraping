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
    public class BookMakerScraper : Scraper
    {
        public BookMakerScraper()
        {
            m_strDomain = "https://www.bookmaker.com.au";
            m_strPwd = "test";
            m_strUser = "test";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("td[class='member-login-button']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement userElement = getElementBy(m_chrDriver,By.CssSelector("input[id='userauth_username']"));
                IWebElement pwdElement = getElementBy(m_chrDriver,By.CssSelector("input[id='userauth_password']"));
                IWebElement logInSubmit = getElementBy(m_chrDriver,By.CssSelector("td[class='member-login-button']")).FindElement(By.CssSelector("input[value='Login']"));
                userElement.SendKeys(m_strUser);
                Thread.Sleep(1000);
                pwdElement.SendKeys(m_strPwd);
                logInSubmit.Click();
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
                IWebElement logInContainer = getElementBy(m_chrDriver, By.CssSelector("td[class='member-login-button']"));
                isLogIn = false;
                return false;
            }
            catch (Exception e)
            {
                isLogIn = true;
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("table[class='odds horses narrow-odds']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("table[class='odds horses narrow-odds']"));
                IEnumerable<IWebElement> raceRows = raceRowsContainer.FindElements(By.CssSelector("tr"));
                foreach (IWebElement raceItem in raceRows)
                {
                    if (!raceItem.Text.Contains("Scratched") && !raceItem.GetAttribute("style").Contains("display:none;"))
                    {
                        RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                        runnerNameTemp.m_strRunnerNumber = getElementBy(m_chrDriver, By.CssSelector("span[class='saddle-number']")).Text;
                        runnerNameTemp.m_strRunnerName = getElementBy(m_chrDriver, By.CssSelector("span[class='competitor-name']")).Text;
                        try
                        {
                            runnerNameTemp.m_strWinPrice = getElementBy(raceItem,By.CssSelector("td[class='win odds odds odds-FixedWin subcontent subcontent-default subcontent-results subcontent-fullresults subcontent-winplace']")).Text;
                            runnerNameTemp.m_strFixedPrice = getElementBy(raceItem,By.CssSelector("td[class='place odds odds odds-FixedPlace subcontent subcontent-default subcontent-results subcontent-fullresults subcontent-winplace']")).Text;
                            m_lstRunnerName.Add(runnerNameTemp);
                        }
                        catch (Exception e)
                        {

                        }
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='races']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("ul[class='races']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.TagName("li"));
                foreach (IWebElement tabItem in tabs)
                {
                    try
                    {
                        IWebElement aTab = getElementBy(tabItem,By.TagName("a"));
                        m_lstEventUrls.Add(m_strDomain + aTab.GetAttribute("href"));
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
