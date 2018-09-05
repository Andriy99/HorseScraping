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
    public class PalmerBetScraper : Scraper
    {
        public PalmerBetScraper()
        {
            m_strDomain = "https://www.palmerbet.com/";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("form[id='login_form']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement loginContainer = getElementBy(m_chrDriver, By.CssSelector("form[id='login_form']"));
                IWebElement userElement = loginContainer.FindElement(By.CssSelector("input[id='user_name_textbox']"));
                IWebElement pwdElement = loginContainer.FindElement(By.CssSelector("input[id='user_pswd_textbox']"));
                IWebElement loginSubmitBtn = loginContainer.FindElement(By.CssSelector("button[id='login_button']"));
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
                IWebElement loginContainer = getElementBy(m_chrDriver, By.CssSelector("form[id='login_forms']"));
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("table[id='race']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("table[id='race']"));
                IEnumerable<IWebElement> raceRows = raceRowsContainer.FindElements(By.CssSelector("tr[class='race-wp-table']"));
                foreach (IWebElement raceItem in raceRows)
                {
                    try
                    {
                        RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                        runnerNameTemp.m_strRunnerNumber = raceItem.FindElement(By.CssSelector("td[class='race-wp-table no_cell']")).Text;
                        runnerNameTemp.m_strRunnerName = raceItem.FindElement(By.CssSelector("span[class='race-wp-table runner_name']")).Text;
                        try
                        {
                            runnerNameTemp.m_strWinPrice = raceItem.FindElements(By.CssSelector("td[class='race-wp-table data_cell']")).ElementAt(0).Text;
                            runnerNameTemp.m_strFixedPrice = raceItem.FindElements(By.CssSelector("td[class='race-wp-table data_cell']")).ElementAt(2).Text;
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='style-scope race-sel-tabs']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("ul[class='style-scope race-sel-tabs']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("li[style='display: inline-block;']"));
                foreach (IWebElement tabItem in tabs)
                {
                    try
                    {
                        tabItem.Click();
                        WaitForTagVisible(m_chrDriver, By.CssSelector("table[id='race']"));
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
