using GetPet.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetPet.Crawler.Utils
{
    public static class ParserUtils
    {
        private static List<string> _male = new List<string>() { "זכר", "male", "Male", "בן" };
        private static List<string> _female = new List<string>() { "נקבה", "female", "Female", "בת" };

        public static Gender ConvertGender(string input)
        {
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

        private static List<string> _dog = new List<string>() { "כלב", "dog" };
        private static List<string> _cat = new List<string>() { "חתול", "cat" };

        public static AnimalType ConvertAnimalType(string input)
        {
            if (_dog.Any(x => input.Contains(x)))
            {
                return AnimalType.Dog;
            }
            else if (_cat.Any(x => input.Contains(x)))
            {
                return AnimalType.Cat;
            }
            else
            {
                return AnimalType.Unknown; // Handle missing case
            }
        }
    }
}