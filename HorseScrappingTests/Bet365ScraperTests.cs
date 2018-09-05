using Microsoft.VisualStudio.TestTools.UnitTesting;
using HorseScrapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseScrapping.Tests
{
    [TestClass()]
    public class Bet365ScraperTests
    {
        [TestMethod()]
        public void Bet365Test()
        {
            string testUrl = "https://www.bet365.com.au/?&cb=1032551672#/AC/B2/C103/D20180906/E20601012/F76071267/P12/";
            Bet365Scraper tester = new Bet365Scraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(),typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void BetEasyTest()
        {
            string testUrl = "https://beteasy.com.au/racing-betting/horse-racing/echuca/20180906/race-1-1045035-30320948";
            BetEasyScraper tester = new BetEasyScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void BlueBetTest()
        {
            string testUrl = "https://www.bluebet.com.au/Overseas-Racing/Lingfield/Race-6/3372728/win";
            BlueBetScraper tester = new BlueBetScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void BookMakerTest()
        {
            string testUrl = "https://www.bookmaker.com.au/racing/horses/strathalbyn/61881070-raine-horne-strathalbyn-bm60/";
            BookMakerScraper tester = new BookMakerScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void ClassicBetTest()
        {
            string testUrl = "https://www.classicbet.com.au/Racing/Thoroughbreds/Echuca/R3/3541528";
            ClassicBetScraper tester = new ClassicBetScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void LadbrokesTest()
        {
            string testUrl = "https://www.ladbrokes.com.au/racing/horses/ballina/61944281-xxxx-gold-mdn/";
            LadbrokesScraper tester = new LadbrokesScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void MadbookieTest()
        {
            string testUrl = "https://www.madbookie.com.au/Racing/Thoroughbreds/Bath/R1/3541979";
            MadbookieScraper tester = new MadbookieScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void NedsTest()
        {
            string testUrl = "https://www.neds.com.au/racing/ballarat/7ce8e89c-06c7-458d-9684-e79c891b20c7";
            NedsScraper tester = new NedsScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void PalmerBetTest()
        {
            string testUrl = "https://www.palmerbet.com/racing/greyhound-racing/albion-park/r3/2018-9-5";
            PalmerBetScraper tester = new PalmerBetScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void PointbetTest()
        {
            string testUrl = "https://pointsbet.com/racing/Greyhound/GBR/Monmore-Bags/race/845301";
            PointbetScraper tester = new PointbetScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void SportsBetTest()
        {
            string testUrl = "https://new.sportsbet.com.au/horse-racing/australia-nz/echuca/race-1-4236962";
            SportsBetScraper tester = new SportsBetScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod()]
        public void UbetTest()
        {
            string testUrl = "https://ubet.com/racing/horse-racing/Randwick-SR/Race-1/Win?Date=20180905";
            UbetScraper tester = new UbetScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }
        [TestMethod()]
        public void UniBetTest()
        {
            string testUrl = "https://www.unibet.com.au/racing#/event/1724379/win?_k=9wb5h9";
            UniBetScraper tester = new UniBetScraper();
            tester.m_strUser = "ddddd";
            tester.m_strPwd = "dfdfd";
            var myArrayOfUrls = tester.GetRaceUrls(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var myArrayRunnerDetails = tester.ReadRunnersAndPrices(testUrl);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            //var myBalance = tester.GetBalance();
            //Assert.AreEqual(myBalance.GetType(), typeof(decimal));
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }
    }
}