using GetPet.Data.Entities;
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
        private static List<string> _twoYear = new List<string>() { "שנתיים", "2 שנים", "2" };
        private static List<string> _threeYear = new List<string>() { "3 שנים", "שלוש", "3" };
        private static List<string> _fourYear = new List<string>() { "4 שנים", "ארבע", "4" };
        private static List<string> _fiveYear = new List<string>() { "5 שנים", "חמש", "5" };
        private static List<string> _sixYear = new List<string>() { "6 שנים", "שש", "6" };
        private static List<string> _sevenYear = new List<string>() { "7 שנים", "שבע", "7" };
        private static List<string> _eightYear = new List<string>() { "8 שנים", "שמונה", "8" };
        private static List<string> _nineYear = new List<string>() { "9 שנים", "תשע", "9" };
        private static List<string> _tenYear = new List<string>() { "10 שנים", "עשר", "10" };
        private static List<string> _elevenYear = new List<string>() { "11 שנים","אחת עשרה","אחד עשרה", "11" };
        private static List<string> _twelveYear = new List<string>() { "12 שנים", "שניים עשר", "שתיים עשרה", "12" };
        private static List<string> _thirteenYear = new List<string>() { "13 שנים", "שלוש עשרה", "13" };
        private static List<string> _fourteenYear = new List<string>() { "14 שנים",  "ארבע עשרה", "14" };

        
        public static int ConvertYear(string input)
        {           
            if (_fourteenYear.Any(x => input.Contains(x)))
                return 14;
            else if (_thirteenYear.Any(x => input.Contains(x)))
                return 13;
            else if (_twelveYear.Any(x => input.Contains(x)))
                return 12;
            else if (_elevenYear.Any(x => input.Contains(x)))
                return 11;
            else if (_tenYear.Any(x => input.Contains(x)))
                return 10;
            else if (_nineYear.Any(x => input.Contains(x)))
                return 9;
            else if (_eightYear.Any(x => input.Contains(x)))
                return 8;
            else if (_sevenYear.Any(x => input.Contains(x)))
                return 7;
            else if (_sixYear.Any(x => input.Contains(x)))
                return 6;
            else if (_fiveYear.Any(x => input.Contains(x)))
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

        private static List<string> _oneMonth = new List<string>() { "חודש", "1 חודש" };
        private static List<string> _twoMonth = new List<string>() { "חודשיים", "2 חודשים" };
        private static List<string> _threeMonth = new List<string>() { "3 חודשים", "שלושה חודשים" };
        private static List<string> _fourMonth = new List<string>() { "4 חודשים", "ארבעה חודשים" };
        private static List<string> _fiveMonth = new List<string>() { "5 חודשים", "חמישה חודשים" };
        private static List<string> _sixMonth = new List<string>() { "6 חודשים", "שישה חודשים", "חצי" };
        private static List<string> _sevenMonth = new List<string>() { "7 חודשים", "שבעה חודשים" };
        private static List<string> _eightMonth = new List<string>() { "8 חודשים", "שמונה חודשים" };
        private static List<string> _nineMonth = new List<string>() { "9 חודשים", "תשעה חודשים" };
        private static List<string> _tenMonth = new List<string>() { "10 חודשים", "עשרה חודשים" };
        private static List<string> _elevenMonth = new List<string>() { "11 חודשים", "אחד עשר חודשים", "אחד עשרה חודשים" };

        public static int ConvertMonth(string input)
        {
          
             if (_elevenMonth.Any(x => input.Contains(x)))
                return 11;
            else if (_tenMonth.Any(x => input.Contains(x)))
                return 10;
            else if (_nineMonth.Any(x => input.Contains(x)))
                return 9;
            else if (_eightMonth.Any(x => input.Contains(x)))
                return 8;
            else if (_sevenMonth.Any(x => input.Contains(x)))
                return 7;
            else if (_sixMonth.Any(x => input.Contains(x)))
                return 6;
            else if (_fiveMonth.Any(x => input.Contains(x)))
                return 5;
            else if (_fourMonth.Any(x => input.Contains(x)))
                return 4;
            else if (_threeMonth.Any(x => input.Contains(x)))
                return 3;
            else if (_twoMonth.Any(x => input.Contains(x)))
                return 2;
            else if (_oneMonth.Any(x => input.Contains(x)))
                return 1;
            return 0;
        }

        private static List<string> _dog = new List<string>() { "כלב", "dog", "dogs", "כלבים" };
        private static List<string> _cat = new List<string>() { "חתול", "cat", "cats", "חתולים" };

        public static AnimalType ConvertAnimalType(string input, List<AnimalType> animalTypes)
        {
            if (_dog.Any(x => input.Contains(x)))
            {
                return animalTypes.FirstOrDefault(x => _dog.Contains(x.Name));
            }
            else if (_cat.Any(x => input.Contains(x)))
            {
                return animalTypes.FirstOrDefault(x => _cat.Contains(x.Name));
            }
            else
            {
                return null;
            }
        }
    }
}