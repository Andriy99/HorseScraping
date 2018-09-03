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
    public class MadbookieScraper : Scraper
    {
        public MadbookieScraper()
        {
            m_strDomain = "https://www.madbookie.com.au/";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("form[id='LoginForm']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement logForm = getElementBy(m_chrDriver, By.CssSelector("form[id='LoginForm']"));
                IWebElement userElement = getElementBy(logForm, By.CssSelector("input[id='login_username']"));
                IWebElement pwdElement = getElementBy(logForm, By.CssSelector("input[id='login_password']"));
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
                previousSource = m_chrDriver.PageSource;
                IWebElement loginForm = getElementBy(m_chrDriver, By.CssSelector("form[id='LoginForm']"));
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("table[class='MarketTable race']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("table[class='MarketTable race']"));
                IEnumerable<IWebElement> raceRows = getElementsBy(raceRowsContainer, By.CssSelector("tr"));
                foreach (IWebElement raceItem in raceRows)
                {
                    if (!raceItem.Text.Contains("Scratched"))
                    {
                        try
                        {
                            RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                            runnerNameTemp.m_strRunnerNumber = getElementBy(raceItem, By.CssSelector("td[class='competitorNumColumn']")).Text;
                            runnerNameTemp.m_strRunnerName = getElementBy(raceItem, By.CssSelector("td[class='competitorCell']")).Text;
                            try
                            {
                                IEnumerable<IWebElement> tds = getElementsBy(raceItem, By.CssSelector("td[class='oddsColumn']"));
                                runnerNameTemp.m_strWinPrice = tds.ElementAt(0).Text;
                                runnerNameTemp.m_strFixedPrice = tds.ElementAt(1).Text;
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='raceNumberTabs']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='raceNumberTabs']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("li"));
                for (int i = 0; i < tabs.Count() - 1; i++)
                {
                    try
                    {
                        IWebElement tabItem = tabs.ElementAt(i);
                        m_lstEventUrls.Add(tabItem.FindElement(By.CssSelector("a")).GetAttribute("href"));
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
