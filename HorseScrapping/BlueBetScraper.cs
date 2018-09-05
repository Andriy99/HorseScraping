using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Threading.Tasks;

namespace HorseScrapping
{
    public class BlueBetScraper : Scraper
    {
        public BlueBetScraper()
        {
            m_strDomain = "https://www.bluebet.com.au/";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("a[class='btn  btn--small  btn--brand-primary']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("a[class='btn  btn--small  btn--brand-primary'"));
                loginBtn.Click();
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='login-wrapper']"));
                IWebElement loginSubmitBtn = getElementBy(m_chrDriver, By.CssSelector("a[class='btn  btn--brand-primary  btn--full  push-half--top  js-close-modal']"));
                IEnumerable<IWebElement> loginContainer = getElementsBy(m_chrDriver,By.CssSelector("div[class='inner-addon  left-addon  select-box']"));
                IWebElement userElement = getElementBy(loginContainer.ElementAt(0), By.CssSelector("input[type='text']"));
                IWebElement pwdElement = getElementBy(loginContainer.ElementAt(1), By.CssSelector("input[type='password']"));
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
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("a[class='btn  btn--small  btn--brand-primary']"));
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='block-list  border--top  border--bottom  border--left ng-scope']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement raceRowsContainer = getElementBy(m_chrDriver, By.CssSelector("ul[class='block-list  border--top  border--bottom  border--left ng-scope']"));
                IEnumerable<IWebElement> raceRows = raceRowsContainer.FindElements(By.CssSelector("li[class='ctr--epsilon  card  hard   ng-scope']"));
                foreach (IWebElement raceItem in raceRows)
                {
                    if (!raceItem.Text.Contains("Scratched"))
                    {
                        try
                        {
                            RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                            runnerNameTemp.m_strRunnerNumber = getElementBy(raceItem,By.CssSelector("div[class='dt40']")).Text;
                            runnerNameTemp.m_strRunnerName = getElementBy(raceItem,By.CssSelector("div[class='zeta  headline-wrap  push-half--bottom']")).Text.Split('(')[0];
                            try
                            {
                                runnerNameTemp.m_strWinPrice = raceItem.FindElements(By.CssSelector("div[class='fixed-odds  text--center  place-bet__odds ng-scope']")).ElementAt(0).Text;
                                runnerNameTemp.m_strFixedPrice = raceItem.FindElements(By.CssSelector("div[class='fixed-odds  text--center  place-bet__odds ng-scope']")).ElementAt(1).Text;
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
                WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='race-pagination']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("ul[class='race-pagination']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("li[class='race-pagination__item  flyout  flyout--right  flyout--tote ng-scope']"));
                for (int i = 0;i<tabs.Count();i++)
                {
                    try
                    {
                        IWebElement tabItem = tabs.ElementAt(i);
                        tabItem.Click();
                        Thread.Sleep(2000);
                        WaitForTagVisible(m_chrDriver, By.CssSelector("ul[class='race-pagination']"));
                        previousSource = m_chrDriver.PageSource;
                        marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("ul[class='race-pagination']"));
                        tabs = marcketTabContainer.FindElements(By.CssSelector("li[class='race-pagination__item  flyout  flyout--right  flyout--tote ng-scope']"));
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
