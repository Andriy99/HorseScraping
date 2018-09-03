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
    public class BetEasyScraper : Scraper
    {
        public BetEasyScraper()
        {
            m_strDomain = "https://beteasy.com.au";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("button[class='login-flyout-button__title']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement loginBtn = getElementBy(m_chrDriver,By.CssSelector("button[class='login-flyout-button__title'"));
                loginBtn.Click();
                WaitForTagVisible(m_chrDriver,By.CssSelector("button[class='cb-button cb-submit-button login-form__submit cb-button--normal cb-button--disabled']"));
                IWebElement loginSubmitBtn = getElementBy(m_chrDriver,By.CssSelector("button[class='cb-button cb-submit-button login-form__submit cb-button--normal cb-button--disabled']"));
                IWebElement userElement = getElementBy(m_chrDriver, By.CssSelector("input[name='username']"));
                IWebElement pwdElement = getElementBy(m_chrDriver, By.CssSelector("input[name='password']"));
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
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("button[class='login-flyout-button__title'"));
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("table[class='middle-section outcomes clearfix']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("table[class='middle-section outcomes clearfix']"));
                IEnumerable<IWebElement> raceRows = raceRowsContainer.FindElements(By.CssSelector("tr"));
                foreach (IWebElement raceItem in raceRows)
                {
                    if (!raceItem.GetAttribute("class").Contains("graph"))
                    {
                        try
                        {
                            RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                            IWebElement detailedInfo = raceItem.FindElement(By.CssSelector("td[class='column details  has-form ']"));
                            string[] detailedArray = detailedInfo.Text.Substring(0, detailedInfo.Text.IndexOf("(")).Split('.');
                            runnerNameTemp.m_strRunnerNumber = detailedArray[0];
                            runnerNameTemp.m_strRunnerName = detailedArray[1];
                            try
                            {
                                runnerNameTemp.m_strWinPrice = raceItem.FindElement(By.CssSelector("a[data-market-type-code='WIN']")).Text;
                                runnerNameTemp.m_strFixedPrice = raceItem.FindElement(By.CssSelector("a[data-market-type-code='PLC']")).Text;
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='racing-number']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='racing-number']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("li[class=' single-bet']"));
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
