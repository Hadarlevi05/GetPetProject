using GetPet.Crawler;
using GetPet.Crawler.Parsers;
using GetPet.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GetPet.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MockTest()
        {
            // ctrl r+t
            var crawler = new TestCrawler<SpcaParser>();
            string file = Path.Combine(Environment.CurrentDirectory, "Files\\Spca.html");

            crawler.Load(file);

            var pets = crawler.Parse();

            Assert.AreEqual(pets.Count, 22);

            var firstPet = pets[0];
            Assert.AreEqual(firstPet.Name, "פרייה");
            Assert.AreEqual(firstPet.AgeInYears, "שנתיים וחודשיים");
            Assert.AreEqual(firstPet.AgeInMonths, "שנתיים וחודשיים");
            Assert.AreEqual(firstPet.Gender, "נקבה");

            var lastPet = pets[1];
            Assert.AreEqual(lastPet.Name, "סקאי");
            Assert.AreEqual(lastPet.AgeInYears, "5 שנים");
            Assert.AreEqual(lastPet.AgeInMonths, "5 שנים");
            Assert.AreEqual(lastPet.Gender, "זכר");
        }

        [Test]
        public void SpcaTest()
        {
            // ctrl r+t
            SpcaCrawler spca = new SpcaCrawler();
            spca.Load(@"https://spca.co.il/%d7%90%d7%99%d7%9e%d7%95%d7%a6%d7%99%d7%9d/");

            var pets = spca.Parse();

            Debugger.Break();
        }

        [Test]
        [Ignore("Yad2 are evil")]
        public void Yad2Test()
        {
            Yad2Crawler yad2 = new Yad2Crawler();
            yad2.Load(@"https://www.yad2.co.il/pets/all?species=1&page=2");

            var pets = yad2.Parse();

            Debugger.Break();
        }
    }
}