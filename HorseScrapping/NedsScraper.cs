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
using System.Windows.Forms;
using HorseScrapping;


namespace HorseScrapping
{
    public class NedsScraper : Scraper
    {
        public NedsScraper()
        {
            m_strDomain = "https://www.neds.com.au";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("button[class='button-login']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("button[class='button-login'"));
                loginBtn.Click();
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='inner-content-modal-content']"));
                IWebElement loginSubmitBtn = getElementBy(m_chrDriver, By.CssSelector("button[class='btn btn-rounded btn-primary btn-block form-button']"));
                IWebElement userElement = getElementBy(m_chrDriver, By.CssSelector("input[name='un']"));
                IWebElement pwdElement = getElementBy(m_chrDriver, By.CssSelector("input[name='pw']"));
                Thread.Sleep(1000);
                userElement.SendKeys(m_strUser);
                pwdElement.SendKeys(m_strPwd);
                loginSubmitBtn.Click();
                Thread.Sleep(3000);
            }
            catch (Exception e)
            {

            }
        }

        public override bool IsLoggedIn()
        {
            try
            {
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("button[class='btn login'"));
                previousSource = m_chrDriver.PageSource;
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("table[class='table table-hover race-table race-field']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("table[class='table table-hover race-table race-field']"));
                IEnumerable<IWebElement> raceRows = raceRowsContainer.FindElements(By.CssSelector("tr[class='race-table-row']"));
                foreach (IWebElement raceItem in raceRows)
                {
                    RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                    IWebElement detailedInfo = raceItem.FindElement(By.CssSelector("div[class='runner']"));
                    string[] detailedArray = detailedInfo.Text.Substring(0, detailedInfo.Text.IndexOf("(")).Split('.');
                    runnerNameTemp.m_strRunnerNumber = detailedArray[0];
                    runnerNameTemp.m_strRunnerName = detailedArray[1];
                    try
                    {
                        IWebElement fixedOddsContainer = raceItem.FindElement(By.CssSelector("td[class='odds-column runner-fixed-odds']")).FindElement(By.CssSelector("tr[class='odds-type-wrapper']"));
                        IEnumerable<IWebElement> values = fixedOddsContainer.FindElements(By.CssSelector("td"));
                        runnerNameTemp.m_strWinPrice = values.ElementAt(0).Text;
                        runnerNameTemp.m_strFixedPrice = values.ElementAt(1).Text;
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='race-numbers-list']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("ul[class='race-numbers-list']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("li"));
                foreach (IWebElement tabItem in tabs)
                {
                    try
                    {
                        m_lstEventUrls.Add(m_strDomain + tabItem.FindElement(By.CssSelector("a")).GetAttribute("href"));
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
