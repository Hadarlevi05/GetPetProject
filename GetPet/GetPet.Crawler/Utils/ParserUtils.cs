using GetPet.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetPet.Crawler.Utils
{
    public static class ParserUtils
    {
        private static List<string> _male = new List<string>() { "זכר", "male", "Male" };
        private static List<string> _female = new List<string>() { "נקבה", "female", "Female" };

        public static Gender ConvertGender(string input)
        {
            // TODO: enum
            if (_female.Any(x => input.Contains(x)))
            {
                return Gender.Female;
            }
            else if (_male.Any(x => input.Contains(x)))
            {
                return Gender.Male;
            }
            else
            {
                return Gender.Unknown; // Handle missing case
            }
        }

        private static List<string> _oneYear = new List<string>() { "שנה", "1 שנים" };
        private static List<string> _twoYear = new List<string>() { "שנתיים", "2 שנים" };
        private static List<string> _threeYear = new List<string>() { "3 שנים", "שלוש", "3 וחצי" };
        private static List<string> _fourYear = new List<string>() { "4 שנים", "ארבע", "4 וחצי" };
        private static List<string> _fiveYear = new List<string>() { "5 שנים", "חמש", "5 וחצי" };

        public static int ConvertYear(string input)
        {
            if (_fiveYear.Any(x => input.Contains(x)))
                return 5;
            else if(_fourYear.Any(x => input.Contains(x)))
                return 4;
            else if(_threeYear.Any(x => input.Contains(x)))
                return 3;
            else if (_twoYear.Any(x => input.Contains(x)))
                return 2;
            else if (_oneYear.Any(x => input.Contains(x)))
                return 1;
            return 0;
        }
    }
}