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
    public class RunnerNamePrice
    {
        public string m_strRunnerNumber { get; set; }
        public string m_strRunnerName { get; set; }
        public string m_strWinPrice { get; set; }
        public string m_strFixedPrice { get; set; }

        public RunnerNamePrice()
        {
            m_strFixedPrice = string.Empty;
            m_strWinPrice = string.Empty;
            m_strRunnerName = string.Empty;
            m_strRunnerNumber = string.Empty;
        }
    }

    public class Scraper
    {
        public ChromeDriver m_chrDriver;
        public string m_strUser { get; set; }
        public string m_strPwd { get; set; }
        public string m_strDomain { get; set; }
        public string previousSource { get; set; }
        public List<string> m_lstEventUrls { get; set; }
        public List<RunnerNamePrice> m_lstRunnerName { get; set; }
        public bool isLogIn { get; set; }

        public Scraper()
        {
            m_strUser = string.Empty;
            m_strPwd = string.Empty;
            m_strDomain = string.Empty;
            previousSource = string.Empty;
            m_lstEventUrls = new List<string>();
            m_lstRunnerName = new List<RunnerNamePrice>();
            m_chrDriver = createDriver("");
            isLogIn = false;
        }

        protected ChromeDriver createDriver(string proxy)
        {
            ChromeDriver driver;
            try
            {
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeOptions option = new ChromeOptions();
                option.AddArgument("--start-maximized");
                Proxy pro = new Proxy();
                if (!proxy.Contains("no"))
                {
                    pro.HttpProxy = proxy;
                    pro.FtpProxy = proxy;
                    pro.SslProxy = proxy;
                }
                option.Proxy = pro;
                driver = new ChromeDriver(chromeDriverService, option, TimeSpan.FromSeconds(180));
                //     driver.Manage().Window.Position = new Point(-2000, 0);
                return driver;
            }
            catch (Exception e)
            {
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeOptions option = new ChromeOptions();
                option.AddArgument("--start-maximized");
                option.AddArgument("–lang= el");
                driver = new ChromeDriver(chromeDriverService, option, TimeSpan.FromSeconds(180));
                return driver;
            }
        }

        public virtual void Login()
        {

        }

        public virtual string GetBalance()
        {
            if (isLogIn)
                return string.Empty;
            else
                return "";
        }

        public virtual bool IsLoggedIn()
        {
            return true;
        }

        public virtual bool ValidateScrapeVersion(string url)
        {
            m_chrDriver.Navigate().GoToUrl(url);
            WaitForPageLoad(m_chrDriver, 10);
            if (m_chrDriver.PageSource.Contains(previousSource))
                return true;
            else
                return false;
        }

        public virtual RunnerNamePrice[] ReadRunnersAndPrices(string url)
        {
            try
            {
                m_chrDriver.Navigate().GoToUrl(url);
                return m_lstRunnerName.ToArray();
            }
            catch (Exception e)
            {
                return new List<RunnerNamePrice>().ToArray();
            }

        }

        public virtual string[] GetRaceUrls(string url)
        {
            return m_lstEventUrls.ToArray();
        }

        protected IWebElement getElementBy(IWebDriver driver, By by)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        protected IWebElement getElementBy(IWebElement Ele , By by)
        {
            try
            {
                IWebElement element = Ele.FindElement(by);
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        protected IList<IWebElement> getElementsBy(IWebDriver driver, By by)
        {
            try
            {
                IList<IWebElement> element = driver.FindElements(by);
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        protected IList<IWebElement> getElementsBy(IWebElement driver, By by)
        {
            try
            {
                IList<IWebElement> element = driver.FindElements(by);
                return element;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        protected void WaitForPageLoad(ChromeDriver driver, int maxWaitTimeInSeconds)
        {
            string state = string.Empty;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitTimeInSeconds));


                wait.Until(d =>
                {
                    try
                    {
                        state = ((IJavaScriptExecutor)driver).ExecuteScript(@"return document.readyState").ToString();
                    }
                    catch (InvalidOperationException)
                    {

                    }
                    catch (NoSuchWindowException)
                    {

                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                    }

                    return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase));
                });
            }
            catch (TimeoutException)
            {

            }
            catch (NullReferenceException)
            {

            }
            catch (WebDriverException)
            {
                if (driver.WindowHandles.Count == 1)
                {
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }

                state = ((IJavaScriptExecutor)driver).ExecuteScript(@"return document.readyState").ToString();
            }
        }

        protected bool WaitForTagVisible(ChromeDriver driver, By by, int waitTime = 20)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
                wait.PollingInterval = TimeSpan.FromSeconds(1);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NoSuchFrameException));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(StaleElementReferenceException));
                wait.IgnoreExceptionTypes(typeof(ArgumentOutOfRangeException));
                wait.Until(d =>
                {
                    try
                    {
                        IWebElement element = driver.FindElement(by);
                        if (element.Displayed)
                            return element;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(2000);
                    }

                    throw new NoSuchElementException();
                });

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected void waitForTagDisplayed(ChromeDriver driver, By by)
        {
            try
            {

                IWebElement element = getElementBy(driver, by);
                while (!element.Displayed)
                    Thread.Sleep(500);

                WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));

                wait.Until(ExpectedConditions.ElementToBeClickable(by));

            }
            catch (Exception e)
            {

            }
        }

        protected void releaseDriver(ChromeDriver driver)
        {
            try
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
