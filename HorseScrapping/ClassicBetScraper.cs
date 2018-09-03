using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace HorseScrapping
{
    public class ClassicBetScraper : Scraper
    {
        public ClassicBetScraper()
        {
            m_strDomain = "https://www.classicbet.com.au/";
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
                IWebElement logInSubmit = getElementBy(logForm, By.CssSelector("a[class='button confirm']"));
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
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("form[id='LoginForm']"));
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("table[class='racing']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("table[class='racing']"));
                IEnumerable<IWebElement> raceRows = getElementsBy(raceRowsContainer,By.TagName("tr"));
                foreach (IWebElement raceItem in raceRows)
                {
                    if (!raceItem.Text.Contains("Scratched"))
                    {
                        try
                        {
                            RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                            IWebElement detailedInfo = raceItem.FindElement(By.CssSelector("td[class='competitorCell']"));
                            string[] detailedArray = detailedInfo.Text.Substring(0, detailedInfo.Text.IndexOf("(")).Replace(".", "#").Split('#');
                            runnerNameTemp.m_strRunnerNumber = detailedArray[0].Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
                            runnerNameTemp.m_strRunnerName = detailedArray[1].Replace("  ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
                            try
                            {
                                IEnumerable<IWebElement> tds = getElementsBy(raceItem, By.TagName("td"));
                                runnerNameTemp.m_strWinPrice = tds.ElementAt(5).Text;
                                runnerNameTemp.m_strFixedPrice = tds.ElementAt(7).Text;
                            }
                            catch (Exception e)
                            {

                            }
                            m_lstRunnerName.Add(runnerNameTemp);
                        }
                        catch (Exception e1)
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
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("a"));
                for (int i = 0; i < tabs.Count() - 1; i++)
                {
                    try
                    {
                        IWebElement tabItem = tabs.ElementAt(i);
                        m_lstEventUrls.Add(tabItem.GetAttribute("href"));
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
