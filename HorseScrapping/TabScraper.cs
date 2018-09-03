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
    public class TabScraper : Scraper
    {
        public TabScraper()
        {
            m_strDomain = "https://www.tab.com.au/";
            m_strPwd = "";
            m_strUser = "";
        }

        public override void Login()
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(m_strDomain);
                WaitForTagVisible(m_chrDriver, By.CssSelector("button[class='functional login-link']"));
                previousSource = m_chrDriver.PageSource;
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("button[class='functional login-link']"));
                loginBtn.Click();
                WaitForTagVisible(m_chrDriver, By.CssSelector("div[class='modal-window']"));
                IWebElement loginContainer = getElementBy(m_chrDriver, By.CssSelector("div[class='modal-window']"));
                IWebElement loginSubmitBtn = getElementBy(loginContainer, By.CssSelector("button[class='common-button modal-ok-button negative']"));
                IWebElement userElement = getElementBy(loginContainer, By.CssSelector("input[name='account_id']"));
                IWebElement pwdElement = getElementBy(loginContainer, By.CssSelector("input[name='account_password']"));
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
                IWebElement loginBtn = getElementBy(m_chrDriver, By.CssSelector("button[class='functional login-link']"));
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

    }
}
