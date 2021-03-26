using GetPet.Crawler;
using NUnit.Framework;
using System.Diagnostics;

namespace GetPet.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Yad2Crawler yad2 = new Yad2Crawler();
            yad2.Load(@"https://www.yad2.co.il/pets/all?species=1");

            var pets= yad2.Parse();

            Debugger.Break();
        }
    }
}