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
    public class UniBetScraper :Scraper
    {
        public UniBetScraper()
        {
            m_strDomain = "https://www.unibet.com.au/racing";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("form[data-test-name='container-login-header']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement logInContainer = getElementBy(m_chrDriver, By.CssSelector("form[data-test-name='container-login-header']"));
                logInContainer.Click();
                IWebElement userElement = logInContainer.FindElement(By.CssSelector("input[name='username']"));
                IWebElement pwdElement = logInContainer.FindElement(By.CssSelector("input[name='password']"));
                IWebElement logInSubmit = logInContainer.FindElement(By.CssSelector("button[data-test-name='btn-login']"));
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
                IWebElement logInContainer = getElementBy(m_chrDriver, By.CssSelector("form[data-test-name='container-login-header']"));
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
                WaitForTagVisible(m_chrDriver,By.CssSelector("iframe=[id='uarc']"));
                IWebElement ifrmaeEle = getElementBy(m_chrDriver, By.CssSelector("iframe[id='uarc']"));
                m_chrDriver.SwitchTo().Frame(ifrmaeEle);
                string pageSoure = m_chrDriver.PageSource;
                WaitForTagVisible(m_chrDriver, By.CssSelector("table[class='event-competitor-table pure-table pure-table-bordered']"));
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("table[class='event-competitor-table pure-table pure-table-bordered']"));
                IEnumerable<IWebElement> raceRows = raceRowsContainer.FindElements(By.CssSelector("tr[class='event-competitor-table__row']"));
                foreach (IWebElement raceItem in raceRows)
                {
                    RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                    runnerNameTemp.m_strRunnerNumber = raceItem.FindElement(By.CssSelector("span[class='event-competitors__row__no-border-left__no']")).Text;
                    runnerNameTemp.m_strRunnerName = raceItem.FindElement(By.CssSelector("span[class='event-runner__name']")).Text;
                    try
                    {
                        runnerNameTemp.m_strWinPrice = raceItem.FindElements(By.CssSelector("div[class='bet-price-button']")).ElementAt(0).Text;
                        runnerNameTemp.m_strFixedPrice = raceItem.FindElements(By.CssSelector("div[class='bet-price-button']")).ElementAt(2).Text;
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("iframe=[id='uarc']"));
                IWebElement ifrmaeEle = getElementBy(m_chrDriver, By.CssSelector("iframe[id='uarc']"));
                m_chrDriver.SwitchTo().Frame(ifrmaeEle);
                string pageSoure = m_chrDriver.PageSource;
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='pure-g event-header__races']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='pure-g event-header__races']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("a[data-km-feature='view_event']"));
                for (int i = 0; i < tabs.Count() ; i++)
                {
                    try
                    {
                        IWebElement tabItem = tabs.ElementAt(i);
                        tabItem.Click();
                        WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='pure-g event-header__races']"));
                        previousSource = m_chrDriver.PageSource;
                        marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='pure-g event-header__races']"));
                        tabs = marcketTabContainer.FindElements(By.CssSelector("a[data-km-feature='view_event']"));
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
