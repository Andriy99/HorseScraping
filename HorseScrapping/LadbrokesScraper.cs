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
    public class LadbrokesScraper : Scraper
    {
        public LadbrokesScraper()
        {
            m_strDomain = "https://www.ladbrokes.com.au/racing/horses/";
            m_strPwd = "test";
            m_strUser = "test";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("form[id='form_users_login']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement logForm = getElementBy(m_chrDriver, By.CssSelector("form[id='form_users_login']"));
                IWebElement userElement = getElementBy(logForm, By.CssSelector("input[id='userauth_username']"));
                IWebElement pwdElement = getElementBy(logForm, By.CssSelector("input[id='userauth_password']"));
                IWebElement logInSubmit = getElementBy(logForm, By.CssSelector("input[type='submit']"));
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
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("form[id='form_users_login']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("form[id='form_users_login']"));
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='competitor-view ']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='competitor-view ']"));
                IWebElement raceTableContainer = getElementBy(raceRowsContainer, By.CssSelector("table"));
                IEnumerable<IWebElement> raceRows = raceTableContainer.FindElements(By.CssSelector("tr"));
                foreach (IWebElement raceItem in raceRows)
                {                    
                    if (raceItem.GetAttribute("class").Contains("competitor row not-scratched saddle-") && !raceItem.Text.Contains("Scratched"))
                    {
                        try
                        {
                            RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                            runnerNameTemp.m_strRunnerNumber = getElementBy(raceItem, By.CssSelector("span[class='saddle-number']")).Text;
                            runnerNameTemp.m_strRunnerName = getElementBy(raceItem, By.CssSelector("span[class='competitor-name']")).Text;
                            try
                            {
                                runnerNameTemp.m_strWinPrice = raceItem.FindElement(By.CssSelector("td[class='win odds odds odds-FixedWin subcontent subcontent-default subcontent-results subcontent-fullresults subcontent-winplace']")).Text;
                                runnerNameTemp.m_strFixedPrice = raceItem.FindElement(By.CssSelector("td[class='place odds odds odds-FixedPlace subcontent subcontent-default subcontent-results subcontent-fullresults subcontent-winplace']")).Text;
                            }
                            catch (Exception e)
                            {

                            }
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='races-row']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='races-row']"));
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
