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

namespace HorseScrapping
{
    public class Bet365Scraper : Scraper
    {

        public Bet365Scraper()
        {
            m_strDomain = "https://www.bet365.com.au/";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("input[class='hm-Login_InputField ']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement userElement = getElementBy(m_chrDriver,By.CssSelector("div[class='hm-Login_UserNameWrapper ']")).FindElement(By.CssSelector("input[class='hm-Login_InputField ']"));
                IWebElement _pwdElement = getElementBy(m_chrDriver, By.CssSelector("div[class='hm-Login_PasswordWrapper ']")).FindElement(By.CssSelector("input[class='hm-Login_InputField ']"));
                _pwdElement.SendKeys("t");
                IWebElement pwdElement = getElementBy(m_chrDriver,By.CssSelector("div[class='hm-Login_PasswordWrapper ']")).FindElement(By.CssSelector("input[class='hm-Login_InputField ']"));
                IWebElement goElement = getElementBy(m_chrDriver,By.CssSelector("button[class='hm-Login_LoginBtn ']"));
                pwdElement.SendKeys(m_strPwd);
                userElement.SendKeys(m_strUser);
                goElement.Click();
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
                IWebElement userElement = getElementBy(m_chrDriver, By.CssSelector("input[class='hm-Login_InputField ']"));
                IWebElement pwdElement = getElementBy(m_chrDriver, By.CssSelector("input[class='hm-Login_InputField ']"));
                previousSource = m_chrDriver.PageSource;
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
                m_chrDriver.Navigate().GoToUrl(url);
                WaitForTagVisible(m_chrDriver,By.CssSelector("div[class='rl-RacingCouponParticipantAus ']"));
                previousSource = m_chrDriver.PageSource;
                IEnumerable<IWebElement> raceRows = getElementsBy(m_chrDriver,By.CssSelector("div[class='rl-RacingCouponParticipantAus ']"));
                foreach (IWebElement raceItem in raceRows)
                {
                    if (!raceItem.Text.Contains("Scratched"))
                    {
                        try
                        {
                            RunnerNamePrice runnerNameTemp = new RunnerNamePrice();
                            runnerNameTemp.m_strRunnerNumber = raceItem.FindElement(By.CssSelector("span[class='rl-CardBarrierNumber_Card']")).Text;
                            runnerNameTemp.m_strRunnerName = raceItem.FindElement(By.CssSelector("span[class='rl-HorseTrainerJockey_Horse']")).Text;
                            runnerNameTemp.m_strWinPrice = raceItem.FindElements(By.CssSelector("span[class='rl-RacingCouponParticipantAusOdds_Odds']"))[2].Text;
                            runnerNameTemp.m_strFixedPrice = raceItem.FindElements(By.CssSelector("span[class='rl-RacingCouponParticipantAusOdds_Odds']"))[3].Text;
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
                m_chrDriver.Navigate().GoToUrl(url);
                WaitForTagVisible(m_chrDriver,By.CssSelector("div[class='rl-RacingNavBar_ButtonContainer rl-RacingNavScoller_ScrollContent ']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='rl-RacingNavBar_ButtonContainer rl-RacingNavScoller_ScrollContent ']"));
                IEnumerable<IWebElement> tabs = marcketTabContainer.FindElements(By.CssSelector("div"));
                int k = 0;
                foreach (IWebElement tabItem in tabs)
                {
                    string number = tabItem.Text;
                    int tempOut = 0;
                    var isNumeric = int.TryParse(number.Replace(" ",""), out tempOut);
                    if (isNumeric)
                        k++;
                }
                for (int i = 0;i< k;i++)
                {
                    IWebElement tabItem = tabs.ElementAt(i);
                    tabItem.Click();
                    Thread.Sleep(1000);
                    m_lstEventUrls.Add(m_chrDriver.Url);
                    WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='rl-RacingNavBar_ButtonContainer rl-RacingNavScoller_ScrollContent ']"));
                    marcketTabContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='rl-RacingNavBar_ButtonContainer rl-RacingNavScoller_ScrollContent ']"));
                    tabs = marcketTabContainer.FindElements(By.CssSelector("div"));
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
