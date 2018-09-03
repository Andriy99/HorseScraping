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
    public class UbetScraper : Scraper
    {
        public UbetScraper()
        {
            m_strDomain = "https://ubet.com/";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='login-container']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement logInBtn = getElementBy(m_chrDriver, By.CssSelector("div[class='login-container']"));
                logInBtn.Click();
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='modal-content']"));
                IWebElement userElement = getElementBy(m_chrDriver, By.CssSelector("input[id='tatts_username']"));
                IWebElement pwdElement = getElementBy(m_chrDriver, By.CssSelector("input[id='tatts_password']"));
                IWebElement modalContent = getElementBy(m_chrDriver,By.CssSelector("div[class='modal-content']"));
                IWebElement logInSubmit = modalContent.FindElement(By.CssSelector("button[data-automationid='signInButton']"));
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
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("div[class='login-container']"));
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='refresh-panel-container']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='refresh-panel-container']"));
                IEnumerable<IWebElement> raceRows = raceRowsContainer.FindElements(By.CssSelector("li[class='runner-list']"));
                foreach (IWebElement raceItem in raceRows)
                {
                    if (!raceItem.Text.Contains("Deduction:"))
                    {
                        RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                        IWebElement detailedInfo = raceItem.FindElement(By.CssSelector("div[class='runner-name-container']"));
                        string[] detailedArray = detailedInfo.Text.Substring(0, detailedInfo.Text.IndexOf("(")).Replace("\r\n","#").Split('#');
                        runnerNameTemp.m_strRunnerNumber = detailedArray[0].Replace(" ", "");
                        runnerNameTemp.m_strRunnerName = detailedArray[1].Replace("  ", "");
                        try
                        {
                            runnerNameTemp.m_strWinPrice = raceItem.FindElements(By.CssSelector("button[class='ubet-race-offer-button vnext btn bet']")).ElementAt(0).Text;
                            runnerNameTemp.m_strFixedPrice = raceItem.FindElements(By.CssSelector("button[class='ubet-race-offer-button vnext btn bet']")).ElementAt(1).Text;
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='races pagenation']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("ul[class='races pagenation']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("li"));
                for(int i = 0;i<tabs.Count()-1;i++)
                {
                    try
                    {
                        IWebElement tabItem = tabs.ElementAt(i);
                        tabItem.FindElement(By.CssSelector("div")).Click();
                        WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='races pagenation']"));
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
