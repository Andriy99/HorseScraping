using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibUsbDotNet.Main;
using LibUsbDotNet;
using MonoLibUsb;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HorseScrapping
{
    public partial class fmain : Form
    {
        public fmain()
        {
            AllTests test = new AllTests();
            test.TestBet365Scraper();
            test.TestBetEasyScraper();
            test.TestBlueBetcraper();
            test.TestBookMakerScraper();
            test.TestClassicBetScraper();
            test.TestMadbookieScraper();
            test.TestNedsScraper();
            test.TestPalmerBetScraper();
            test.TestPointbetScraper();
            test.TestSportsBetScraper();
            test.TestUbetScraper();
            test.TestUniBetScraper();
        }
    }
}
