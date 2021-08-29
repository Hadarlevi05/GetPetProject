using GetPet.Crawler.Utils;
using NUnit.Framework;
using System.Diagnostics;

namespace GetPet.Tests
{
    public class ParserUtilsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConvertYear()
        {
            string input = "שנתיים";
            int result = ParserUtils.ConvertYear(input);

            Assert.IsTrue(result == 2);

            Debugger.Break();
        }
    }
}