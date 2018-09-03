using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HorseScrapping
{
    [TestClass]
    class AllTests
    {
        [TestMethod]
        public void TestBet365Scraper()
        {
            string testUrl = "";
            Bet365Scraper tester = new Bet365Scraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestBetEasyScraper()
        {
            string testUrl = "";
            BetEasyScraper tester = new BetEasyScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestBlueBetcraper()
        {
            string testUrl = "";
            BlueBetScraper tester = new BlueBetScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestBookMakerScraper()
        {
            string testUrl = "";
            BookMakerScraper tester = new BookMakerScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestClassicBetScraper()
        {
            string testUrl = "";
            ClassicBetScraper tester = new ClassicBetScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestMadbookieScraper()
        {
            string testUrl = "";
            MadbookieScraper tester = new MadbookieScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestNedsScraper()
        {
            string testUrl = "";
            NedsScraper tester = new NedsScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestPalmerBetScraper()
        {
            string testUrl = "";
            PalmerBetScraper tester = new PalmerBetScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestPointbetScraper()
        {
            string testUrl = "";
            PointbetScraper tester = new PointbetScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestSportsBetScraper()
        {
            string testUrl = "";
            SportsBetScraper tester = new SportsBetScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestUbetScraper()
        {
            string testUrl = "";
            UbetScraper tester = new UbetScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }

        [TestMethod]
        public void TestUniBetScraper()
        {
            string testUrl = "";
            UniBetScraper tester = new UniBetScraper();
            tester.GetRaceUrls(testUrl);
            var myArrayOfUrls = tester.ReadRunnersAndPrices(testUrl);
            Assert.IsTrue(myArrayOfUrls.GetType().IsArray);
            var isLoggedOut = !tester.IsLoggedIn();
            Assert.IsTrue(isLoggedOut == true);
            tester.Login();
            var isLoggedIn = tester.IsLoggedIn();
            Assert.IsTrue(isLoggedIn == true);
            var isValidated = tester.ValidateScrapeVersion(testUrl);
        }
    }
}
