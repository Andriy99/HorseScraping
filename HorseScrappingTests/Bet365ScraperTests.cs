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
            string testUrl = "https://www.bet365.com.au/?&cb=1032551672#/AC/B2/C103/D20180831/E20600100/F75976627/P12/";
            Bet365Scraper tester = new Bet365Scraper();
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
            string testUrl = "https://beteasy.com.au/racing-betting/horse-racing/saratoga/20180829/race-1-1039035-30161490";
            BetEasyScraper tester = new BetEasyScraper();
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
            string testUrl = "";
            BlueBetScraper tester = new BlueBetScraper();
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
            string testUrl = "";
            BookMakerScraper tester = new BookMakerScraper();
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
            string testUrl = "";
            ClassicBetScraper tester = new ClassicBetScraper();
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
            string testUrl = "";
            LadbrokesScraper tester = new LadbrokesScraper();
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
            string testUrl = "";
            MadbookieScraper tester = new MadbookieScraper();
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
            string testUrl = "";
            NedsScraper tester = new NedsScraper();
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
            string testUrl = "";
            PalmerBetScraper tester = new PalmerBetScraper();
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
            string testUrl = "";
            PointbetScraper tester = new PointbetScraper();
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
            string testUrl = "";
            SportsBetScraper tester = new SportsBetScraper();
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
            string testUrl = "";
            UbetScraper tester = new UbetScraper();
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

        public void UniBetTest()
        {
            string testUrl = "";
            UniBetScraper tester = new UniBetScraper();
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